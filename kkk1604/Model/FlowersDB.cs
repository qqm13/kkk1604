using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model
{
    class FlowersDB
    { 
    DbConnection connection;

    private FlowersDB(DbConnection db)
    {
        this.connection = db;
    }

    public bool Insert(Flower flower)
    {
        bool result = false;
        if (connection == null)
            return result;

        if (connection.OpenConnection())
        {
            MySqlCommand cmd = connection.CreateCommand("insert into `Flowers` Values (0, @title, @count, @price);select LAST_INSERT_ID();");

            cmd.Parameters.Add(new MySqlParameter("title", flower.Title));
            cmd.Parameters.Add(new MySqlParameter("count", flower.Count));
            cmd.Parameters.Add(new MySqlParameter("price", flower.Price));

                try
            {
                int id = (int)(ulong)cmd.ExecuteScalar();
                if (id > 0)
                {
                    MessageBox.Show(id.ToString());
                    flower.id = id;
                    result = true;
                }
                else
                {
                    MessageBox.Show("Запись не добавлена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        connection.CloseConnection();
        return result;
    }

    public List<Flower> SelectAll()
    {
        List<Flower> flowers = new List<Flower>();
        if (connection == null)
            return flowers;


        if (connection.OpenConnection())
        {
            var command = connection.CreateCommand("select `id`, `Title`, `Count`, `Price` from `Flowers` ");

            try
            {
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string title = string.Empty;
                    if (!dr.IsDBNull(1))
                        title = dr.GetString("Title");
                    int count = dr.GetInt32(2);
                    int price = dr.GetInt32(3);
                    flowers.Add(new Flower
                    {
                        id = id,
                        Title = title,
                        Count = count,
                        Price = price
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        connection.CloseConnection();
        return flowers;
    }

    public bool Update(Flower edit)
    {
        bool result = false;
        if (connection == null)
            return result; if (connection.OpenConnection())
        {
            var mc = connection.CreateCommand($"update `Flowers` set `Title`=@title, `Count`=@count, `Price`=@price where `id` = {edit.id}");
            mc.Parameters.Add(new MySqlParameter("title", edit.Title));
            mc.Parameters.Add(new MySqlParameter("count", edit.Count));
            mc.Parameters.Add(new MySqlParameter("price", edit.Price));


                try
                {
                mc.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        connection.CloseConnection();
        return result;
    }


    public bool Remove(Flower remove)
    {
        bool result = false;
        if (connection == null)
            return result;

        if (connection.OpenConnection())
        {
            var mc = connection.CreateCommand($"delete from `Flowers` where `id` = {remove.id}");
            try
            {
                mc.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        connection.CloseConnection();
        return result;
    }




    static FlowersDB db;
    public static FlowersDB GetDb()
    {
        if (db == null)
            db = new FlowersDB(DbConnection.GetDbConnection());
        return db;
    }
}
}


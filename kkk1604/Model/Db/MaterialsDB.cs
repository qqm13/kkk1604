using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class MaterialsDB
    {
        DbConnection connection;

        private MaterialsDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Material material)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Materials` Values (0, @title, @Price);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", material.Title));
                cmd.Parameters.Add(new MySqlParameter("PricePerSquareMeter", material.Price));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        material.Id = id;
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

        public List<Material> SelectAll()
        {
            List<Material> materials = new List<Material>();
            if (connection == null)
                return materials;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `Title`, `Price` from `Materials` ");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString(1);
                        int price = dr.GetInt32(2);
                        materials.Add(new Material
                        {
                            Id = id,
                            Title = title,
                            Price = price,

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return materials;
        }

        public bool Update(Material edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Materials` set `Title`=@title, `Price`=@Price where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("PricePerSquareMeter", edit.Price));

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


        public bool Remove(Material remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Materials` where `id` = {remove.Id}");
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




        static MaterialsDB db;
        public static MaterialsDB GetDb()
        {
            if (db == null)
                db = new MaterialsDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


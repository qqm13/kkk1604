using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model
{
    public class FormsDB
    {
        DbConnection connection;

        private FormsDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Form form)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Forms` Values (0, @title, @priceModiffier);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", form.Title));
                cmd.Parameters.Add(new MySqlParameter("priceModiffier", form.PriceModiffier));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        form.id = id;
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

        public List<Form> SelectAll()
        {
            List<Form> forms = new List<Form>();
            if (connection == null)
                return forms;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `Title`, `PriceModiffier` from `Forms` ");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");
                        int priceModiffier = dr.GetInt32(2);
                        forms.Add(new Form
                        {
                            id = id,
                            Title = title,
                            PriceModiffier = priceModiffier
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return forms;
        }

        public bool Update(Form edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Forms` set `Title`=@title, `PriceModiffier`=@priceModiffier where `id` = {edit.id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("priceModiffier", edit.PriceModiffier));

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


        public bool Remove(Form remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Forms` where `id` = {remove.id}");
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




        static FormsDB db;
        public static FormsDB GetDb()
        {
            if (db == null)
                db = new FormsDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


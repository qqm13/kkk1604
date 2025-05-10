using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class FormsDB
    {
        DbConnection connection;

        private FormsDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Form form)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Forms` Values (0, @title, @height, @width, @MaterialUsage );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", form.Title));
                cmd.Parameters.Add(new MySqlParameter("height", form.Height));
                cmd.Parameters.Add(new MySqlParameter("width", form.Width));
                cmd.Parameters.Add(new MySqlParameter("MaterialUsage", form.MaterialUsage));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        form.Id = id;
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
                var command = connection.CreateCommand("select `id`, `Title`, `Height`, `Width`, `MaterialUsage` from `Forms` ");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString(1);
                        int height = dr.GetInt32(2);
                        int width = dr.GetInt32(3);
                        int materialUsage = dr.GetInt32(4);
                        forms.Add(new Form
                        {
                            Id = id,
                            Title = title,
                            Height = height,
                            Width = width,
                            MaterialUsage = materialUsage,

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
                var mc = connection.CreateCommand($"update `Forms` set `Title`=@title, `Height`=@Height, `Width`=@Width, `MaterialUsage`=@MaterialUsage where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("Height", edit.Height));
                mc.Parameters.Add(new MySqlParameter("Width", edit.Width));
                mc.Parameters.Add(new MySqlParameter("MaterialUsage", edit.MaterialUsage));

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
                var mc = connection.CreateCommand($"delete from `Forms` where `id` = {remove.Id}");
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


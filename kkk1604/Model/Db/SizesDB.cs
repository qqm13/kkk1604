using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class SizesDB
    {
        DbConnection connection;

        private SizesDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Size size)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Sizes` Values (0, @title, @Height, @Length, @Width);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", size.Title));
                cmd.Parameters.Add(new MySqlParameter("Height", size.Height));
                cmd.Parameters.Add(new MySqlParameter("Length", size.Length));
                cmd.Parameters.Add(new MySqlParameter("Width", size.Width));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        size.Id = id;
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

        public List<Size> SelectAll()
        {
            List<Size> sizes = new List<Size>();
            if (connection == null)
                return sizes;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `Title`, `Height`, `Length`, `Width` from `Sizes` ");

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
                        int length = dr.GetInt32(3);
                        int width = dr.GetInt32(4);
                        sizes.Add(new Size
                        {
                            Id = id,
                            Title = title,
                            Length = length,
                            Width = width,
                            Height = height
                            
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return sizes;
        }

        public bool Update(Size edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Sizes` set `Title`=@title, `Height`=@Height,`Length`=@Length,`Width`=@Width where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("Height", edit.Height));
                mc.Parameters.Add(new MySqlParameter("Length", edit.Length));
                mc.Parameters.Add(new MySqlParameter("Width", edit.Width));

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


        public bool Remove(Size remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Sizes` where `id` = {remove.Id}");
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




        static SizesDB db;
        public static SizesDB GetDb()
        {
            if (db == null)
                db = new SizesDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


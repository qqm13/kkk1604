using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class GravesDB
    {
        DbConnection connection;

        private GravesDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Grave grave)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `GraveType` Values (0, @title, @formId, @materialId, @price);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", grave.Title));
                cmd.Parameters.Add(new MySqlParameter("formId", grave.Form.Id));
                cmd.Parameters.Add(new MySqlParameter("materialId", grave.Material.Id));
                cmd.Parameters.Add(new MySqlParameter("price", grave.Price));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        grave.Id = id;
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

        public List<Grave> SelectAll()
        {
            List<Grave> graves = new List<Grave>();
            if (connection == null)
                return graves;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("SELECT gt.id, gt.Title, gt.FormId, gt.MaterialId, gt.Price, f.Title, f.Height, f.MaterialUsage, f.Width,m.Title, m.Price FROM GraveType gt JOIN Forms f ON gt.FormId = f.id JOIN Materials m ON gt.MaterialId = m.id");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString(1);
                        int formId = dr.GetInt32(2);
                        int materialId = dr.GetInt32(3);
                        int price = dr.GetInt32(4);

                        string titleForm = string.Empty;
                        if (!dr.IsDBNull(5))
                            titleForm = dr.GetString(5);
                        int height = dr.GetInt32(6);
                        int materialUsage = dr.GetInt32(7);
                        int width = dr.GetInt32(8);

                        string titleMaterial = string.Empty;
                        if (!dr.IsDBNull(7))
                            titleMaterial = dr.GetString(9);
                        int priceMaterial = dr.GetInt32(10);

                        Form GraveForm = new Form
                        {
                          Id = formId,
                          Title = titleForm,
                          Height = height,
                          Width = width,
                            MaterialUsage = materialUsage
                        };

                        Material GraveMaterial = new Material
                        {
                          Id=materialId,
                          Title = titleMaterial,
                          Price = priceMaterial
                        };

                        graves.Add(new Grave
                        {
                            Id = id,
                            Title = title,
                            FormId = formId,
                            MaterialId = materialId,
                            Price = price,

                            Form = GraveForm,
                            Material = GraveMaterial
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return graves;
        }

        public bool Update(Grave edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `GraveType` set `Title`=@title, `FormId`=@formId, `MaterialId`=@materialId, `Price`=@price where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("formId", edit.Form.Id));
                mc.Parameters.Add(new MySqlParameter("materialId", edit.Material.Id));
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


        public bool Remove(Grave remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `GraveType` where `id` = {remove.Id}");
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




        static GravesDB db;
        public static GravesDB GetDb()
        {
            if (db == null)
                db = new GravesDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


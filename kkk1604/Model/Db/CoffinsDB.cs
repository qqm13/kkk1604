using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class CoffinsDB
    {
        DbConnection connection;

        private CoffinsDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Coffin coffin)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `CoffinType` Values (0, @title, @materialId, @sizeId, @price);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", coffin.Title));
                cmd.Parameters.Add(new MySqlParameter("materialId", coffin.Material.Id));
                cmd.Parameters.Add(new MySqlParameter("sizeId", coffin.Size.Id));
                cmd.Parameters.Add(new MySqlParameter("price", coffin.Price));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        coffin.Id = id;
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

        public List<Coffin> SelectAll()
        {
            List<Coffin> coffins = new List<Coffin>();
            if (connection == null)
                return coffins;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("SELECT c.id, c.Title, c.MaterialId, c.SizeId, c.Price,  m.Title, m.Price,s.Title,s.PriceModiffier from `CoffinType` c JOIN Materials m ON c.MaterialId = m.id JOIN Sizes s ON c.SizeId = s.id");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");
                        int materialId = dr.GetInt32(2);
                        int sizeId = dr.GetInt32(3);
                        int price = dr.GetInt32(4);

                        string titleMaterial = string.Empty;
                        if (!dr.IsDBNull(1))
                            titleMaterial = dr.GetString(5);
                        int priceMaterial = dr.GetInt32(6);
                        
                        string titleSize = string.Empty;
                        if (!dr.IsDBNull(1))
                            titleSize = dr.GetString(7);
                        int priceModiffier = dr.GetInt32(8);

                        Material CoffinMaterial = new Material
                        {
                            Id = materialId,
                            Title = titleMaterial,  
                            Price = priceMaterial
                        };

                        Size CoffinSize = new Size
                        {
                            Id = sizeId,
                            Title = titleSize,
                            PriceModiffier = priceModiffier
                        };

                        coffins.Add(new Coffin
                        {
                            Id = id,
                            Title = title,
                            MaterialId = materialId,
                            SizeId = sizeId,
                            Price = price,

                            Material = CoffinMaterial,
                            Size = CoffinSize
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return coffins;
        }

        public bool Update(Coffin edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `CoffinType` set `Title`=@title, `MaterialId`=@materialId, `SizeId`=@sizeId, `Price`=@price where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("materialId", edit.Material.Id));
                mc.Parameters.Add(new MySqlParameter("sizeId", edit.Size.Id));
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


        public bool Remove(Coffin remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `CoffinType` where `id` = {remove.Id}");
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




        static CoffinsDB db;
        public static CoffinsDB GetDb()
        {
            if (db == null)
                db = new CoffinsDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


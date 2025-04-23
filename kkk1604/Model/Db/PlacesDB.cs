using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class PlacesDB
    {
        DbConnection connection;

        private PlacesDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Place place)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Place` Values (0, @cemetaryAdress, @cemeterySectorNumber, @cemeteryPlotNumber, @price );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("cemetaryAdress", place.CemetaryAdress));
                cmd.Parameters.Add(new MySqlParameter("cemeterySectorNumber", place.CemeterySectorNumber));
                cmd.Parameters.Add(new MySqlParameter("cemeteryPlotNumber", place.CemeteryPlotNumber));
                cmd.Parameters.Add(new MySqlParameter("price", place.Price));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        place.Id = id;
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

        public List<Place> SelectAll()
        {
            List<Place> places = new List<Place>();
            if (connection == null)
                return places;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `CemetaryAdress`, `CemeterySectorNumber`, `CemeteryPlotNumber`, `Price`   from `Place` ");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string cemetaryAdress = string.Empty;
                        if (!dr.IsDBNull(1))
                            cemetaryAdress = dr.GetString("CemetaryAdress");
                        int cemeterySectorNumber = dr.GetInt32(2);
                        int cemeteryPlotNumber = dr.GetInt32(3);
                        int price = dr.GetInt32(4);
                        places.Add(new Place
                        {
                            Id = id,
                            CemetaryAdress = cemetaryAdress,
                            CemeterySectorNumber = cemeterySectorNumber,
                            CemeteryPlotNumber = cemeteryPlotNumber,
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
            return places;
        }

        public bool Update(Place edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Place` set `CemetaryAdress`=@cemetaryAdress, `CemeterySectorNumber`=@cemeterySectorNumber, `CemeteryPlotNumber`=@cemeteryPlotNumber, `Price`=@price where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("cemetaryAdress", edit.CemetaryAdress));
                mc.Parameters.Add(new MySqlParameter("cemeterySectorNumber", edit.CemeterySectorNumber));
                mc.Parameters.Add(new MySqlParameter("cemeteryPlotNumber", edit.CemeteryPlotNumber));
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


        public bool Remove(Place remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Place` where `id` = {remove.Id}");
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




        static PlacesDB db;
        public static PlacesDB GetDb()
        {
            if (db == null)
                db = new PlacesDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


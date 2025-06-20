﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    public class DeathPlacesDB
    {
        DbConnection connection;

        private DeathPlacesDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(DeathPlace deathplace)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `DeathPlace` Values (0, @title, @graveTypeId, @coffinTyprId, @price, @flowersId );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", deathplace.Title));
                cmd.Parameters.Add(new MySqlParameter("graveTypeId", deathplace.Grave.Id));
                cmd.Parameters.Add(new MySqlParameter("coffinTyprId", deathplace.Coffin.Id));
                cmd.Parameters.Add(new MySqlParameter("price", deathplace.Price));
                cmd.Parameters.Add(new MySqlParameter("flowersId", deathplace.Flower.Id));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        deathplace.Id = id;
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

        public List<DeathPlace> SelectAll()
        {
            List<DeathPlace> deathPlaces = new List<DeathPlace>();
            if (connection == null)
                return deathPlaces;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("SELECT dp.id, dp.Title, dp.GraveTypeId, dp.CoffinTypeId, dp.Price, dp.FlowersId, gt.Title, gt.Price, ct.Title, ct.Price, f.Title, f.Price  FROM DeathPlace dp JOIN CoffinType ct ON dp.CoffinTypeId = ct.id JOIN GraveType gt ON dp.GraveTypeId = gt.id JOIN Flowers f ON dp.FlowersId = f.id");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString(1);
                        int graveTypeId = dr.GetInt32(2);
                        int coffinTypeId = dr.GetInt32(3); 
                        int price = dr.GetInt32(4);
                        int flowersId = dr.GetInt32(5);

                        string gravetitle = string.Empty;
                        if (!dr.IsDBNull(6))
                            gravetitle = dr.GetString(6);
                        int graveprice = dr.GetInt32(7);

                        string coffintitle = string.Empty;
                        if (!dr.IsDBNull(8))
                            coffintitle = dr.GetString(8);
                        int coffinprice = dr.GetInt32(9);

                        string flowerstitle = string.Empty;
                        if (!dr.IsDBNull(10))
                            flowerstitle = dr.GetString(10);
                        int flowerprice = dr.GetInt32(11);


                        Grave DeathPlaceGrave = new Grave
                        {
                            Id = graveTypeId,
                            Title = gravetitle,  
                            Price = graveprice
                        };

                        Coffin DeathPlaceCoffin = new Coffin
                        {
                            Id = coffinTypeId,
                            Title = coffintitle,
                            Price = coffinprice
                        };

                        Flower DeathPlaceFlower = new Flower
                        {
                            Id = flowersId,
                            Title = flowerstitle,
                            Price = flowerprice
                        };

                        deathPlaces.Add(new DeathPlace
                        {
                            Id = id,
                            Title = title,
                            GraveTypeId = graveTypeId,
                            CoffinTypeId = graveTypeId,
                            FlowersId = flowersId,
                            Price = price,

                            Coffin = DeathPlaceCoffin,
                            Grave = DeathPlaceGrave,
                            Flower = DeathPlaceFlower

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return deathPlaces;
        }

        public bool Update(DeathPlace edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `DeathPlace` set `Title`=@title, `GraveTypeId`=@graveTypeId, `CoffinTypeId`=@coffinTypeId, `Price`=@price, `FlowersId`=@flowersId where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("title", edit.Title));
                mc.Parameters.Add(new MySqlParameter("graveTypeId", edit.Grave.Id));
                mc.Parameters.Add(new MySqlParameter("coffinTypeId", edit.Coffin.Id));
                mc.Parameters.Add(new MySqlParameter("price", edit.Price));
                mc.Parameters.Add(new MySqlParameter("flowersId", edit.Flower.Id));


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


        public bool Remove(DeathPlace remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `DeathPlace` where `id` = {remove.Id}");
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




        static DeathPlacesDB db;
        public static DeathPlacesDB GetDb()
        {
            if (db == null)
                db = new DeathPlacesDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


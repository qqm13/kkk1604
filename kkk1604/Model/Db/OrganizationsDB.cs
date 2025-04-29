using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.Model.Db
{
    class OrganizationsDB
    {

        DbConnection connection;

        private OrganizationsDB(DbConnection db)
        {
            connection = db;
        }

        public bool Insert(Organization organization)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Organization` Values (0, @DeathplaceId, @GuestCount, @Necrology, @LastDinner, @LastSlideShow, @GuestBus, @Catafalque, @Priest, @Date, @PlaceId, @Price, @Status );select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("DeathplaceId", organization.DeathPlaceId));
                cmd.Parameters.Add(new MySqlParameter("GuestCount", organization.GuestCount));
                cmd.Parameters.Add(new MySqlParameter("Necrology", organization.Necrology));
                cmd.Parameters.Add(new MySqlParameter("LastDinner", organization.LastDinner));
                cmd.Parameters.Add(new MySqlParameter("LastSlideShow", organization.LastSlideshow));
                cmd.Parameters.Add(new MySqlParameter("GuestBus", organization.GuestBus));
                cmd.Parameters.Add(new MySqlParameter("Catafalque", organization.Catafalque));
                cmd.Parameters.Add(new MySqlParameter("Priest", organization.Priest));
                cmd.Parameters.Add(new MySqlParameter("Date", organization.Date));
                cmd.Parameters.Add(new MySqlParameter("PlaceId", organization.PlaceId)); 
                cmd.Parameters.Add(new MySqlParameter("Price", organization.Price));
                cmd.Parameters.Add(new MySqlParameter("Status", organization.Status));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        organization.Id = id;
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

        public List<Organization> SelectAll()
        {
            List<Organization> organizations = new List<Organization>();
            if (connection == null)
                return organizations;


            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("SELECT o.id, DeathplaceId,GuestCount,Necrology,LastDinner,LastSlideshow,GuestBus,Catafalque,Priest,  date,  PlaceId,  o.Price,  p.CemetaryAdress,  p.CemeterySectorNumber,  p.CemeteryPlotNumber,  dp.Title,  dp.GraveTypeId,  dp.CoffinTypeId,dp.FlowersId,ct.Title,  gt.Title,  f.Title, Status FROM Organization o JOIN Place p ON o.PlaceId = p.id JOIN DeathPlace dp ON o.DeathplaceId = dp.id JOIN CoffinType ct ON dp.CoffinTypeId = ct.id JOIN GraveType gt ON dp.GraveTypeId = gt.id JOIN Flowers f ON dp.FlowersId = f.id");

                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int deathplaceId = dr.GetInt32(1);
                        int guestCount = dr.GetInt32(2);
                        bool necrology = dr.GetBoolean(3);
                        bool lastDiner = dr.GetBoolean(4);
                        bool lastSlideShow = dr.GetBoolean(5);
                        bool guestBus = dr.GetBoolean(6);
                        bool catafalque = dr.GetBoolean(7);
                        bool priest = dr.GetBoolean(8);
                        DateTime dateEvent = new DateTime();
                          dateEvent = dr.GetDateTime(9);
                        int placeId = dr.GetInt32(10);
                        int price = dr.GetInt32(11);
                        string cemetaryAdress = string.Empty;
                        if (!dr.IsDBNull(12))
                            cemetaryAdress = dr.GetString(12);
                        int cemeterySectorNumber = dr.GetInt32(13);
                        int cemeteryPlotNumber = dr.GetInt32(14);
                        string deathPlaceTitle = string.Empty;
                        if (!dr.IsDBNull(15))
                            deathPlaceTitle = dr.GetString(15);
                        int graveId = dr.GetInt32(16);
                        int coffinid = dr.GetInt32(17);
                        int flowerId = dr.GetInt32(18);
                        string coffinTitle = string.Empty;
                        if (!dr.IsDBNull(19))
                            coffinTitle = dr.GetString(19);
                        string graveTitle = string.Empty;
                        if (!dr.IsDBNull(20))
                            graveTitle = dr.GetString(20); 
                        string flowerTitle = string.Empty;
                        if (!dr.IsDBNull(21))
                            flowerTitle = dr.GetString(21);
                        bool status = dr.GetBoolean(22);

                        Grave organizationGrave = new Grave
                        {
                            Id = graveId,
                            Title = graveTitle
                        };

                        Coffin organizationCoffin = new Coffin
                        {
                            Id = coffinid,
                            Title = coffinTitle

                        };

                        Flower organizationFlower = new Flower
                        {
                            Id = flowerId,
                            Title = flowerTitle
                        };

                        Place organizationPlace = new Place
                        {
                            Id = placeId,
                            CemetaryAdress = cemetaryAdress,
                            CemeteryPlotNumber = cemeteryPlotNumber,
                            CemeterySectorNumber = cemeterySectorNumber
                        };

                        DeathPlace organizationDeathPlace = new DeathPlace
                        {
                            Id = deathplaceId,
                            Title = deathPlaceTitle,
                            CoffinTypeId = coffinid,
                            GraveTypeId = graveId,
                            FlowersId = flowerId,

                            Coffin = organizationCoffin,
                            Grave = organizationGrave,
                            Flower = organizationFlower
                        };

                        organizations.Add(new Organization
                        {
                            Id = id,
                            DeathPlaceId = deathplaceId,
                            DeathPlace = organizationDeathPlace,
                            GuestCount = guestCount,
                            Necrology = necrology,
                            LastDinner = lastDiner,
                            LastSlideshow = lastSlideShow,
                            GuestBus = guestBus,
                            Catafalque = catafalque,
                            Priest = priest,
                            Date = dateEvent,
                            PlaceId = placeId,
                            Place = organizationPlace,
                            Price = price,
                            Status = status


                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return organizations;
        }

        public bool Update(Organization edit)
        {
            bool result = false;
            if (connection == null)
                return result; if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Organization` set `DeathplaceId`=@DeathplaceId, `GuestCount`=@GuestCount, `Necrology`=@Necrology, `LastDinner`=@LastDinner, `LastSlideshow`=@LastSlideshow,`GuestBus`=@GuestBus,`Catafalque`=@Catafalque,`Priest`=@Priest,`Date`=@Date,`PlaceId`=@PlaceId, `Price`=@Price, `Status`=@Status where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("DeathplaceId", edit.DeathPlace.Id));
                mc.Parameters.Add(new MySqlParameter("GuestCount", edit.GuestCount));
                mc.Parameters.Add(new MySqlParameter("Necrology", edit.Necrology));
                mc.Parameters.Add(new MySqlParameter("LastDinner", edit.LastDinner));
                mc.Parameters.Add(new MySqlParameter("LastSlideshow", edit.LastSlideshow));
                mc.Parameters.Add(new MySqlParameter("GuestBus", edit.GuestBus));
                mc.Parameters.Add(new MySqlParameter("Catafalque", edit.Catafalque));
                mc.Parameters.Add(new MySqlParameter("Priest", edit.Priest));
                mc.Parameters.Add(new MySqlParameter("Date", edit.Date));
                mc.Parameters.Add(new MySqlParameter("PlaceId", edit.Place.Id));
                mc.Parameters.Add(new MySqlParameter("Price", edit.Price));
                mc.Parameters.Add(new MySqlParameter("Status", edit.Status));



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


        public bool Remove(Organization remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Organization` where `id` = {remove.Id}");
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




        static OrganizationsDB db;
        public static OrganizationsDB GetDb()
        {
            if (db == null)
                db = new OrganizationsDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}


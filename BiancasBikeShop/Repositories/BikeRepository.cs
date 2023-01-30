using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BiancasBikeShop.Models;
using BiancasBikeShop.Utils;

namespace BiancasBikeShop.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection("server=localhost\\SQLExpress;database=BiancasBikeShop;integrated security=true;TrustServerCertificate=true");
            }
        }

        public List<Bike> GetAllBikes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT b.Id, b.Brand, b.Color, b.OwnerId, 
                               b.BikeTypeId,

                        o.Id, o.Name

                        FROM Bike b 
                        LEFT JOIN Owner o on b.OwnerId = o.Id                      
                        ORDER BY b.Brand
                    ";
                    var bikes = new List<Bike>();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var bike = new Bike()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Brand = DbUtils.GetString(reader, "Brand"),
                            Color = DbUtils.GetString(reader, "Color"),
                            OwnerId = DbUtils.GetInt(reader, "OwnerId"),
                            Owner = new Owner()
                            {
                                Id = DbUtils.GetInt(reader, "OwnerId"),
                                Name = DbUtils.GetString(reader, "Name"),
                            }
                        };
                        bikes.Add(bike);
                    }
                    reader.Close();
                    // implement code here... 
                    return bikes;
                }

                //public Bike GetBikeById(int id)
                //{
                //    Bike bike = null;
                //    //implement code here...
                //    return bike;
                //}

                //public int GetBikesInShopCount()
                //{
                //    int count = 0;
                //    // implement code here... 
                //    return count;
                //}
            }
        }
    }
}

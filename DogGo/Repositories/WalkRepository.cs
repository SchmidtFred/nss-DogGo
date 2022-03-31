using System;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _configuration;

        public WalkRepository(IConfiguration config)
        {
            _configuration = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT w.Id AS WalkId, Date, Duration, WalkerId, DogId,
                                   d.Id AS DId, d.[Name] AS DogName, OwnerId, Breed, Notes, ImageUrl,
                                   o.Id AS OId, Email, o.[Name] AS OwnerName, Address, NeighborhoodId, Phone
                            FROM Walks w
                                 LEFT JOIN Dog d ON d.Id = w.DogId
                                 LEFT JOIN Owner o ON o.Id = d.OwnerId
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();
                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkId")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Dog = new Dog()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("DId")),
                                    Name = reader.GetString(reader.GetOrdinal("DogName")),
                                    OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                    Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl")),
                                    Owner = new Owner()
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("OId")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                        Address = reader.GetString(reader.GetOrdinal("Address")),
                                        NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                        Phone = reader.GetString(reader.GetOrdinal("Phone"))
                                    }
                                }
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }

        public Walk GetWalkById(int walkId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id AS WalkId, Date, Duration, WalkerId, DogId,
                               d.Id AS DId, d.[Name] AS DogName, OwnerId, Breed, Notes, ImageUrl,
                               o.Id AS OId, Email, o.[Name] AS OwnerName, Address, NeighborhoodId, Phone
                        FROM Walks w
                             LEFT JOIN Dog d ON d.Id = w.DogId
                             LEFT JOIN Owner o ON o.Id = d.OwnerId
                        WHERE w.Id = @id";

                    cmd.Parameters.AddWithValue("@id", walkId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkId")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Dog = new Dog()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("DId")),
                                    Name = reader.GetString(reader.GetOrdinal("DogName")),
                                    OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                    Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl")),
                                    Owner = new Owner()
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("OId")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                        Address = reader.GetString(reader.GetOrdinal("Address")),
                                        NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                        Phone = reader.GetString(reader.GetOrdinal("Phone"))
                                    }
                                }
                            };

                            return walk;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Walk> GetWalksByWalker(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id AS WalkId, Date, Duration, WalkerId, DogId,
                               d.Id AS DId, d.[Name] AS DogName, OwnerId, Breed, Notes, ImageUrl,
                               o.Id AS OId, Email, o.[Name] AS OwnerName, Address, NeighborhoodId, Phone
                        FROM Walks w
                             LEFT JOIN Dog d ON d.Id = w.DogId
                             LEFT JOIN Owner o ON o.Id = d.OwnerId
                        WHERE w.WalkerId = @id";

                    cmd.Parameters.AddWithValue("@id", walkerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();

                        while (reader.Read())
                        {
                            Walk walk = new Walk
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkId")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Dog = new Dog()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("DId")),
                                    Name = reader.GetString(reader.GetOrdinal("DogName")),
                                    OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                    Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                    ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl")),
                                    Owner = new Owner()
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("OId")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                        Address = reader.GetString(reader.GetOrdinal("Address")),
                                        NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                        Phone = reader.GetString(reader.GetOrdinal("Phone"))
                                    }
                                }
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }

        public void AddWalk(Walk walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Walks (Date, Duration, WalkerId, DogId)
                        OUPUT INSERTED.ID
                        VALUES (@date, @duration, @walkerId, @dogId)
                    ";

                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);
                    cmd.Parameters.AddWithValue("@walkerId", walk.WalkerId);
                    cmd.Parameters.AddWithValue("@dogId", walk.DogId);

                    int id = (int)cmd.ExecuteScalar();

                    walk.Id = id;
                }
            }
        }

        public void DeleteWalk(int walkId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Walks
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", walkId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

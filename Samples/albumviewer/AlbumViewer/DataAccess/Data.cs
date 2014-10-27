using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Configuration;

namespace AlbumViewer
{
    public class Data
    {
        /// <summary>
        /// Get all albums and associated photo values
        /// </summary>
        /// <returns>Collection of Albums</returns>
        public static ReadOnlyCollection<Album> GetPhotoAlbums()
        {
            List<Album> albums = new List<Album>();
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetAlbums", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    // Use using here so SqlDataReader will be closed automatically
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            albums.Add(new Album()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2)
                            }
                            );
                        }
                    }
                }

                // Now get all the photos for each each album
                // This could be obtained by a single query with multiple
                // result sets but for illustrative purposes it is broken
                // into two processes
                using(SqlCommand cmd = new SqlCommand("GetPhotosByAlbum", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@albumId", SqlDbType.Int);
                    for(int x = 0; x < albums.Count; x++)
                    {
                        cmd.Parameters["@albumId"].Value = albums[x].Id;

                        List<Photo> photos = new List<Photo>();
                        // Use using here so SqlDataReader will be closed automatically
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                photos.Add(new Photo()
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        Description = reader.GetString(2),
                                        Image = (byte[])reader.GetValue(3)
                                    }
                                );
                            }
                        }

                        // Annoying because
                        // albums[x].Photos = photos.AsReadOnly();
                        // produces the error, Cannot modify the return value of xxx because it is not a variable
                        // The error could be avoided by using class rather than struct
                        Album temp = albums[x];
                        temp.Photos = photos.AsReadOnly();
                        albums[x] = temp;
                    }
                }
            }

            return albums.AsReadOnly();
        }

        /// <summary>
        /// Add new album to database
        /// </summary>
        /// <returns>Newly created Album</returns>
        public static Album AddAlbum()
        {
            Album album = new Album()
            {
                Name = "New Album",
                Description = "Enter Description"
            };

            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("InsertAlbum", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    // Add the return value parameter
                    SqlParameter param = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                    param.Direction = ParameterDirection.ReturnValue;

                    // Add the name parameter and set the value
                    cmd.Parameters.AddWithValue("@name", album.Name);
                    // Add the description parameter and set the value
                    cmd.Parameters.AddWithValue("@desc", album.Description);

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    // The return value is the index of the newly added album
                    album.Id = (int)cmd.Parameters["RETURN_VALUE"].Value;
                }
            }

            return album;
        }

        /// <summary>
        /// Add photo to Album
        /// </summary>
        /// <param name="albumId">Id of Album to add photo to</param>
        /// <param name="photo">Photo to add</param>
        public static void AddPhoto(int albumId, Photo photo)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("InsertPhoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                     // Add the name parameter and set the value
                    cmd.Parameters.AddWithValue("@name", photo.Name);
                    // Add the description parameter and set the value
                    cmd.Parameters.AddWithValue("@desc", photo.Description);
                    // Add the image parameter and set the value
                    cmd.Parameters.AddWithValue("@photo", photo.Image);
                    // Add the album parameter and set the value
                    cmd.Parameters.AddWithValue("@albumId", albumId);

                    // Add the return value parameter
                    SqlParameter param = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                    param.Direction = ParameterDirection.ReturnValue;

                    // Execute the insert
                    cmd.ExecuteNonQuery();

                    // Return value will be the index of the newly added photo
                    photo.Id = (int)cmd.Parameters["RETURN_VALUE"].Value;
                }
            }
        }

        /// <summary>
        /// Get photo matching Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Photo GetPhoto(int id)
        {
            Photo photo = new Photo()
            {
                Id = id
            };

            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetPhoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    if(dt.Rows.Count != 0)
                    {
                        photo.Name = dt.Rows[0]["name"].ToString();
                        photo.Description = dt.Rows[0]["description"].ToString();
                        photo.Image = (byte[])dt.Rows[0]["photo"];
                    }

                    // Could also use a SqlDataReader
                    //SqlDataReader reader = cmd.ExecuteReader();
                    //while(reader.Read())
                    //{
                    //    photo.Name = reader.GetString(0);
                    //    photo.Description = reader.GetString(1);
                    //    //photo.Image = (byte[])reader.GetValue(2);
                    //}
                }
            }

            return photo;
        }

        /// <summary>
        /// Delete Album and all photos associated with it
        /// </summary>
        /// <param name="id">Id of Album to delete</param>
        public static void DeleteAlbum(int id)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("DeleteAlbum", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Delete photo
        /// </summary>
        /// <param name="id">Id of photo to delete</param>
        public static void DeletePhoto(int id)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("DeletePhoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update Album
        /// </summary>
        /// <param name="album">Album info to update</param>
        public static void UpdateAlbum(Album album)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("UpdateAlbum", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", album.Id);
                    cmd.Parameters.AddWithValue("@name", album.Name);
                    cmd.Parameters.AddWithValue("@desc", album.Description);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update Photo
        /// </summary>
        /// <param name="photo">Photo info to update</param>
        public static void UpdatePhoto(Photo photo)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("UpdatePhoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", photo.Id);
                    cmd.Parameters.AddWithValue("@name", photo.Name);
                    cmd.Parameters.AddWithValue("@desc", photo.Description);
                    
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region Properties

        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Data"].ConnectionString; }
        }

        #endregion
    }
}

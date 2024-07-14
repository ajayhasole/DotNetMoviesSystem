
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Movies.Models;

public partial class Movie
{
    internal readonly object Bookings;

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Actors { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public string PosterUrl { get; set; } = null!;





    public static List<Movie> getAll()
    {

        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = @" Data Source = (localdb)\ProjectModels; Initial Catalog = Movies; Integrated Security = True;";
        List<Movie> movies = new List<Movie>();

        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Movies";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Movie mov = new Movie();


                mov.Id = dr.GetInt32("Id");
                mov.Title = dr.GetString("title");
                mov.Actors = dr.GetString("actors");
                mov.ReleaseDate = dr.GetDateTime("releaseDate");
                mov.PosterUrl = dr.GetString("posterUrl");

                movies.Add(mov);
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            cn.Close();
        }


      return movies;
  }
}



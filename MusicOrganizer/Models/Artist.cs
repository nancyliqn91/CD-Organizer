using System.Collections.Generic;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace MusicOrganizer.Models
{ 
  public class Artist
  {

    public string Name { get; set; }
    public int Id { get; set; }
    
    public Artist(string artistName)
    {
      Name = artistName;
    }

    public Artist(string artistName, int id)
    {
      Name = artistName;
      Id = id;
    }

    public override bool Equals(System.Object otherArtist)
    {
      if (!(otherArtist is Artist))
      {
        return false;
      }
      else
      {
        Artist newArtist = (Artist) otherArtist;
        bool idEquality = (this.Id == newArtist.Id);
        bool nameEquality = (this.Name == newArtist.Name);
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      // Begin new code
      cmd.CommandText = "INSERT INTO artists (name) VALUES (@ArtistName);";
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ArtistName";
      param.Value = this.Name;
      cmd.Parameters.Add(param);    
      cmd.ExecuteNonQuery();
      // Returning an id from the Database
      Id = (int) cmd.LastInsertedId;
      // End new code
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
 
    
    public static List<Artist> GetAll()
    {
      List<Artist> allArtists = new List<Artist> { };

      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM artists;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int artistId = rdr.GetInt32(0);
        string artistName = rdr.GetString(1);
        Artist newArtist = new Artist(artistName, artistId);
        allArtists.Add(newArtist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allArtists;
    }

    public static Artist Find(int id)
    {
    // We open a connection.
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. 
      // We always need to do this to make a SQL query.
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM artists WHERE id = @ThisId;";

      // We have to use parameter placeholders @ThisId and a `MySqlParameter` object to 
      // prevent SQL injection attacks. 
      // This is only necessary when we are passing parameters into a query. 
      // We also did this with our Save() method.
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = id;
      cmd.Parameters.Add(param);

      // We use the ExecuteReader() method because our query will be returning results and 
      // we need this method to read these results. 
      // This is in contrast to the ExecuteNonQuery() method, which 
      // we use for SQL commands that don't return results like our Save() method.
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int artistId = 0;
      string artistName = "";
      while (rdr.Read())
      {
        artistId = rdr.GetInt32(0);
        artistName = rdr.GetString(1);
      }
      Artist foundArtist = new Artist(artistName, artistId);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundArtist;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM artists;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    

  }
}
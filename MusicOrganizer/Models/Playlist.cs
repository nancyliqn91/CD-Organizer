using System.Collections.Generic;

namespace MusicOrganizer.Models
{
  public class Playlist
  {
    private static List<Playlist> _instances = new List<Playlist> {};
    public string Name { get; set; }
    public int Id { get; }
    public List<Artist> Artists { get; set; }
    
    public Playlist(string playListName)
    {
      Name = playListName;
      _instances.Add(this);
      Id = _instances.Count;
      Artists = new List<Artist>{};
    }

    public static void ClearAll()
    {
      _instances.Clear();
    }

    public static List<Playlist> GetAll()
    {
      return _instances;
    }   

    public static Playlist Find(int searchId)
    {
      return _instances[searchId -1];
    }

    public void AddArtist(Artist artist)
    {
      Artists.Add(artist);
    }
 
  }
}
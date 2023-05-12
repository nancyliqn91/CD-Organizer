using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicOrganizer.Models;
using System.Collections.Generic;
using System;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class PlaylistTests : IDisposable
  {
    public void Dispose()
    {
      Playlist.ClearAll();
    }
    // Test methods go here
    [TestMethod]
    public void Playlist_CreateInstanceOfPlayList_PlayList()
    {
      //  Assert.AreEqual(ExpectedResult, CodeToTest);
      Playlist newPlayList = new Playlist("test playList");
      Assert.AreEqual(typeof(Playlist), newPlayList.GetType());
    }

    [TestMethod]
    public void GetPlaylist_ReturnPlaylist_String()
    {
      string name = "Test Playlist";
      Playlist newPlayList = new Playlist(name);

      string result = newPlayList.Name;
      
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsPlaylistId_Int()
    {
      string name = "Test Playlist";
      Playlist newPlayList = new Playlist(name);
      
      int result = newPlayList.Id;

      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllPlaylistObjects_PlaylistList()
    {
      string name1 = "Study";
      string name2 = "Workout";
      Playlist newPlaylist1 = new Playlist(name1);
      Playlist newPlaylist2 = new Playlist(name2);  
      List<Playlist> newList = new List<Playlist> {newPlaylist1, newPlaylist2};

      List<Playlist> result = Playlist.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }
    
    [TestMethod]
    public void Find_ReturnsCorrectPlaylist_Playlist()
    {
      string name1 = "Study";
      string name2 = "Workout";
      Playlist newPlaylist1 = new Playlist(name1);
      Playlist newPlaylist2 = new Playlist(name2);

      Playlist result = Playlist.Find(2);
      
      Assert.AreEqual(newPlaylist2, result);
    }
    
    [TestMethod]
    public void AddArtist_AssociatesArtistWithCategory_ArtistList()
    {
      string artistName = "Lady Gaga";
      Artist newArtist = new Artist(artistName);
      List<Artist> newList = new List<Artist> {newArtist};
      string playlistName = "Popular";
      Playlist newPlaylist = new Playlist(playlistName);
      newPlaylist.AddArtist(newArtist);

      List<Artist> result = newPlaylist.Artists;

      CollectionAssert.AreEqual(newList, result);
    }
  }
}

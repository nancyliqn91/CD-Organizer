using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MusicOrganizer.Models;

namespace MusicOrganizer.Controllers
{
  public class PlaylistsController : Controller
  {
    [HttpGet("/playlists")]
    public ActionResult Index()
    {
      List<Playlist> allPlaylists = Playlist.GetAll();
      return View(allPlaylists);
    }  

    [HttpGet("/playlists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/playlists")]
    public ActionResult Create(string playlistName)
    {
      Playlist newPlaylist = new Playlist(playlistName);
      return RedirectToAction("Index");
    }

    [HttpGet("/playlists/{id}")]
    public ActionResult Show(int id)
    {

      Dictionary<string, object> model = new Dictionary<string, object>();
      Playlist selectedPlaylist= Playlist.Find(id);
      List<Artist> playlistArtists = selectedPlaylist.Artists;
      model.Add("playlist", selectedPlaylist);
      model.Add("artists", playlistArtists);
      return View(model);
    }

    // This one creates new Artists within a given Playlist, not new Playlists:
    [HttpPost("/playlists/{playlistId}/artists")]
    public ActionResult Create(int playlistId, string artistName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Playlist foundPlaylist = Playlist.Find(playlistId);
      Artist newArtist = new Artist(artistName);
      // new code
      newArtist.Save();
      foundPlaylist.AddArtist(newArtist);
      List<Artist> playlistArtists = foundPlaylist.Artists;
      model.Add("artists", playlistArtists);
      model.Add("playlist", foundPlaylist);
      return View("Show", model);
    }

  }
}
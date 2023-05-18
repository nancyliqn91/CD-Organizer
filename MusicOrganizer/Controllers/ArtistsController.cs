using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MusicOrganizer.Models;

namespace MusicOrganizer.Controllers
{
  public class ArtistsController : Controller
  {

    [HttpGet("/playlists/{playlistId}/artists/new")]
    public ActionResult New(int playlistId)
    {
      Playlist playlist = Playlist.Find(playlistId);
      return View(playlist);
    }

    [HttpGet("/playlists/{playlistId}/artists/{artistId}")]
    public ActionResult Show(int playlistId, int artistId)
    {
      Artist artist = Artist.Find(artistId);
      Playlist playlist = Playlist.Find(playlistId);
      Dictionary <string, object> model = new Dictionary <string, object> ();
      model.Add("artist", artist);
      model.Add("playlist", playlist);
      return View(model);
    }

  }
}
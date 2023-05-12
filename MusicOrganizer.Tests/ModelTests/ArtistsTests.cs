using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicOrganizer.Models;
using System.Collections.Generic;
using System;

namespace MusicOrganizer.Tests
{
  [TestClass]
  public class ArtistTests : IDisposable
  {
    public void Dispose()
    {
      Artist.ClearAll();
    }
    // Test methods go here
    [TestMethod]
    public void Artist_CreateInstanceOfArtist_Artist()
    {
      // Assert.AreEqual(ExpectedResult, CodeToTest);
      Artist newArtist = new Artist("test artist");
      Assert.AreEqual(typeof(Artist), newArtist.GetType());
    }
  }
}
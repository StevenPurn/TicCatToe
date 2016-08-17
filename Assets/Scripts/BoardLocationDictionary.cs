using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardLocationDictionary : MonoBehaviour {
    public Dictionary<TileLocation, string> SortLayer = new Dictionary<TileLocation, string>()
    {
        { new TileLocation(0, 0), "Fifth_Row" },
        { new TileLocation(0, 1), "Fourth_Row" },
        { new TileLocation(0, 2), "Third_Row" },
        { new TileLocation(0, 3), "Second_Row" },
        { new TileLocation(0, 4), "First_Row" },
        { new TileLocation(1, 0), "Sixth_Row" },
        { new TileLocation(1, 1), "Fifth_Row" },
        { new TileLocation(1, 2), "Fourth_Row" },
        { new TileLocation(1, 3), "Third_Row" },
        { new TileLocation(1, 4), "Second_Row" },
        { new TileLocation(2, 0), "Seventh_Row" },
        { new TileLocation(2, 1), "Sixth_Row" },
        { new TileLocation(2, 2), "Fifth_Row" },
        { new TileLocation(2, 3), "Fourth_Row" },
        { new TileLocation(2, 4), "Third_Row" },
        { new TileLocation(3, 0), "Eighth_Row" },
        { new TileLocation(3, 1), "Seventh_Row" },
        { new TileLocation(3, 2), "Sixth_Row" },
        { new TileLocation(3, 3), "Fifth_Row" },
        { new TileLocation(3, 4), "Fourth_Row" },
        { new TileLocation(4, 0), "Ninth_Row" },
        { new TileLocation(4, 1), "Eighth_Row" },
        { new TileLocation(4, 2), "Seventh_Row" },
        { new TileLocation(4, 3), "Sixth_Row" },
        { new TileLocation(4, 4), "Fifth_Row" }
    };

    public Dictionary<int, TileLocation> RandomSpawnPoints = new Dictionary< int, TileLocation>()
    {
        { 1, new TileLocation(0, 1) },
        { 2, new TileLocation(0, 3) },
        { 3, new TileLocation(1, 0) },
        { 4, new TileLocation(1, 4) },
        { 5, new TileLocation(3, 0) },
        { 6, new TileLocation(3, 4) },
        { 7, new TileLocation(4, 1) },
        { 8, new TileLocation(4, 3) }
    };
}
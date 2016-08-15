using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardLocationDictionary : MonoBehaviour {
    public Dictionary<Vector2, string> SortLayer = new Dictionary<Vector2, string>()
    {
        {new Vector2(0,0), "Fifth_Row" },
        {new Vector2(0,1), "Fourth_Row" },
        {new Vector2(0,2), "Third_Row" },
        {new Vector2(0,3), "Second_Row" },
        {new Vector2(0,4), "First_Row" },
        {new Vector2(1,0), "Sixth_Row" },
        {new Vector2(1,1), "Fifth_Row" },
        {new Vector2(1,2), "Fourth_Row" },
        {new Vector2(1,3), "Third_Row" },
        {new Vector2(1,4), "Second_Row" },
        {new Vector2(2,0), "Seventh_Row" },
        {new Vector2(2,1), "Sixth_Row" },
        {new Vector2(2,2), "Fifth_Row" },
        {new Vector2(2,3), "Fourth_Row" },
        {new Vector2(2,4), "Third_Row" },
        {new Vector2(3,0), "Eight_Row" },
        {new Vector2(3,1), "Seventh_Row" },
        {new Vector2(3,2), "Sixth_Row" },
        {new Vector2(3,3), "Fifth_Row" },
        {new Vector2(3,4), "Fourth_Row" },
        {new Vector2(4,0), "Ninth_Row" },
        {new Vector2(4,1), "Eight_Row" },
        {new Vector2(4,2), "Seventh_Row" },
        {new Vector2(4,3), "Sixth_Row" },
        {new Vector2(4,4), "Fifth_Row" }
    };

    public Dictionary<Vector2, Vector2> BoardLocation = new Dictionary<Vector2, Vector2>()
    {
        {new Vector2(0,0), new Vector2(-3.48f,0) },
        {new Vector2(0,1), new Vector2(-2.61f,0.6f) },
        {new Vector2(0,2), new Vector2(-1.74f,1.2f) },
        {new Vector2(0,3), new Vector2(-0.87f,1.2f) },
        {new Vector2(0,4), new Vector2(0,2.4f) },
        {new Vector2(1,0), new Vector2(-2.61f,-0.6f) },
        {new Vector2(1,1), new Vector2(-1.74f,0) },
        {new Vector2(1,2), new Vector2(-0.87f,0.6f) },
        {new Vector2(1,3), new Vector2(0,1.2f) },
        {new Vector2(1,4), new Vector2(0.87f,1.2f) },
        {new Vector2(2,0), new Vector2(-1.74f,-1.2f) },
        {new Vector2(2,1), new Vector2(-0.87f,-0.6f) },
        {new Vector2(2,2), new Vector2(0,0) },
        {new Vector2(2,3), new Vector2(0.87f,0.6f) },
        {new Vector2(2,4), new Vector2(1.74f,1.2f) },
        {new Vector2(3,0), new Vector2(-0.87f,-1.8f) },
        {new Vector2(3,1), new Vector2(0,-1.2f) },
        {new Vector2(3,2), new Vector2(0.87f,-0.6f) },
        {new Vector2(3,3), new Vector2(1.74f,0) },
        {new Vector2(3,4), new Vector2(2.61f,0.6f) },
        {new Vector2(4,0), new Vector2(0,-2.4f) },
        {new Vector2(4,1), new Vector2(0.87f,-1.8f) },
        {new Vector2(4,2), new Vector2(1.74f,-1.2f) },
        {new Vector2(4,3), new Vector2(2.61f,-0.6f) },
        {new Vector2(4,4), new Vector2(3.48f,0) }
    };
}
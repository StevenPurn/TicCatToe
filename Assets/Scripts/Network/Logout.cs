using UnityEngine;
using System.Collections;
using GameSparks.Api.Requests;

public class Logout : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LogOut()
    {
        GameSparks.Core.GS.Reset();
        FB.Logout();
    }
}

using UnityEngine;
using System.Collections;
using Facebook;
using GameSparks.Api.Requests;

public class LoginManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CallFBInit();
	}

    private void CallFBInit()
    {
        FB.Init(OnInitComplete, OnHideUnity);
    }

    private void OnInitComplete()
    {
        CallFBLogin();
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Game showing: " + isGameShown);
    }
	
    private void CallFBLogin()
    {
        FB.Login("email,user_friends", GameSparksLogin);
    }

    private void GameSparksLogin(FBResult fbResult)
    {
        if (FB.IsLoggedIn)
        {
            new FacebookConnectRequest().SetAccessToken(FB.AccessToken).Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Somthing failed when connecting: " + response.Errors);
                }
                else
                {
                    UserManager.instance.UpdateInformation();
                }
            });
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

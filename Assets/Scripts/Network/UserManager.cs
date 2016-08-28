using UnityEngine;
using System.Collections;
using GameSparks.Api.Requests;
using UnityEngine.UI;

public class UserManager : MonoBehaviour {

    public static UserManager instance;

    public string userName;
    public string userId;
    private string facebookId;

    public Text userNameLabel;
    public Image profilePicture;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
    public void UpdateInformation()
    {
        new AccountDetailsRequest().Send((response) =>
        {
            UpdateGUI(response.DisplayName, response.UserId, response.ExternalIds.GetString("FB").ToString());
        });
    }

    public void UpdateGUI(string name, string uid, string fbid)
    {
        userName = name;
        userNameLabel.text = userName;
        userId = uid;
        facebookId = fbid;
        StartCoroutine(getFBPicture());
    }

    public IEnumerator getFBPicture()
    {
        var www = new WWW("http://graph.facebook.com/" + facebookId + "/picture?width=210&height=210");
        yield return www;
        Texture2D tempPic = new Texture2D(240, 240);
        www.LoadImageIntoTexture(tempPic);
        profilePicture.sprite = Sprite.Create(tempPic,new Rect(0,0,240,240),Vector2.zero);
    }
}

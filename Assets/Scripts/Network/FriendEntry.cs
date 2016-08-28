using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FriendEntry : MonoBehaviour {

    public string userName, id, facebookId;
    public bool isOnline;

    public Text nameLabel;
    public Image profilePicture;
    public Image onlineTexture;


    // Use this for initialization
    void Start () {
        UpdateFriend();
	}

    public void UpdateFriend()
    {
        nameLabel.text = userName;
        onlineTexture.color = isOnline ? Color.green : Color.grey;
        StartCoroutine(getFBPicture());
    }

    public IEnumerator getFBPicture()
    {
        //Debug.Log("http://graph.facebook.com/" + facebookId + "/picture?width=210&height=210");
        var www = new WWW("http://graph.facebook.com/" + facebookId + "/picture?width=210&height=210");
        yield return www;
        Texture2D tempPic = new Texture2D(240, 240);
        www.LoadImageIntoTexture(tempPic);
        profilePicture.sprite = Sprite.Create(tempPic, new Rect(0, 0, 240, 240), Vector2.zero);
    }
	
	// Update is called once per frame
	public void StartChallenge () {
        List<string> gsId = new List<string>();

        gsId.Add(id);

        ChallengeManager.instance.ChallengeUser(gsId);
	}
}

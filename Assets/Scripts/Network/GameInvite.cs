using UnityEngine;
using System.Collections;
using GameSparks.Api.Requests;
using UnityEngine.UI;

public class GameInvite : MonoBehaviour {

    public string inviteName, challengeExpiry, challengeId, facebookId;
    public bool canDestroy = false;

    public Text inviteNameText, inviteExpiryText;
    public Image profilePicture;

	// Use this for initialization
	void Start () {
        inviteNameText.text = inviteName + "has invited you to play";
    }

    public void AcceptChallenge()
    {
        new AcceptChallengeRequest().SetChallengeInstanceId(challengeId).Send((response) =>
        {
            if (response.HasErrors)
            {
                Debug.Log(response.Errors);
            }
            else
            {
                canDestroy = true;
            }
        });
    }

    public void DeclineChallengeRequest()
    {
        new DeclineChallengeRequest().SetChallengeInstanceId(challengeId).Send((response) =>
        {
            if (response.HasErrors)
            {
                Debug.Log(response.Errors);
            }
            else
            {
                canDestroy = true;
            }
        });
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

    public void DestroyAfterTween()
    {
        if (canDestroy)
        {
            ChallengeManager.instance.gameInvites.Remove(gameObject);
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

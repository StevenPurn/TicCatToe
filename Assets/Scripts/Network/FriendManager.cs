using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Requests;

public class FriendManager : MonoBehaviour {

    public GameObject friendEntryPrefab;

    public GameObject friendParent;

    public List<GameObject> friends = new List<GameObject>();

    // Use this for initialization
    void Start () {
	
	}

    public void GetFriends()
    {
        for (int i = 0; i < friends.Count; i++)
        {
            Destroy(friends[i]);
        }
        friends.Clear();

        new ListGameFriendsRequest().Send((response) =>
        {
            foreach (var friend in response.Friends)
            {
                GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Friend"), friendParent.transform);

                FriendEntry fe = go.GetComponent<FriendEntry>();
                fe.userName = friend.DisplayName;
                fe.id = friend.Id;
                fe.isOnline = friend.Online.Value;
                fe.facebookId = friend.ExternalIds.GetString("FB");

                friends.Add(go);

            }
        });
    }


	// Update is called once per frame
	void Update () {
	
	}
}

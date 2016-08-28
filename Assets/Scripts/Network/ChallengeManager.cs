using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Requests;
using System;

public class ChallengeManager : MonoBehaviour {

    public static ChallengeManager instance;

    public GameObject challengeParent;
    public GameObject runningPrefab;
    public List<GameObject> runningGames = new List<GameObject>();

    public GameObject inviteParent;
    public GameObject invitePrefab;
    public List<GameObject> gameInvites = new List<GameObject>();

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
    public void ChallengeUser(List<string> userIds)
    {
        new CreateChallengeRequest().SetChallengeShortCode("ticcattoe")
            .SetUsersToChallenge(userIds)
            .SetEndTime(System.DateTime.Today.AddDays(1))
            .SetChallengeMessage("I challenge you to a duel")
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log(response.Errors);
                }
                else
                {
                    Debug.Log("challenge sent");
                }
            });
    }

    public void GetChallengeInvites()
    {
        for (int i = 0; i < gameInvites.Count; i++)
        {
            Destroy(gameInvites[i]);
        }
        gameInvites.Clear();

        new ListChallengeRequest().SetShortCode("ticcattoe")
            .SetState("RECEIVED")
            .SetEntryCount(50)
            .Send((response) =>
            {
                foreach (var challenge in response.ChallengeInstances)
                {
                    GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Challenge"), challengeParent.transform);
                    GameInvite invite = go.GetComponent<GameInvite>();
                    invite.challengeId = challenge.ChallengeId;
                    invite.inviteName = challenge.Challenger.Name;
                    invite.inviteExpiryText.text = challenge.EndDate.ToString();

                    gameInvites.Add(go);

                }
            });

    }

	public void GetRunningChallenges () {
	
	}
}

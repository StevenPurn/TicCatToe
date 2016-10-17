using UnityEngine;
using System.Collections;

public class RateUs : MonoBehaviour {
	public void RateApp(){
		if (Application.platform == RuntimePlatform.WindowsPlayer){
			Application.OpenURL ("itms-apps:itunes.apple.com/us/app/tic-cat-toe/id1146712155");
		}else if(Application.platform == RuntimePlatform.Android){
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.GamesGames.TicCatToe");
		}
	}
}

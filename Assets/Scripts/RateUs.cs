using UnityEngine;
using System.Collections;

public class RateUs : MonoBehaviour {
	public void RateApp(){
		if (Application.platform == RuntimePlatform.WindowsPlayer){
			Application.OpenURL ("http://itunes.apple.com/app/tic-cat-toe/id1146712155?ls=1&mt=8");
		}else if(Application.platform == RuntimePlatform.Android){
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.GamesGames.TicCatToe");
		}
	}
}

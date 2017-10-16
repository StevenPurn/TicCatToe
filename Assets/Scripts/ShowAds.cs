using UnityEngine;
using UnityEngine.UI;

public class ShowAds : MonoBehaviour {

	//ConnectionTesterStatus connectionTestResult;

	public GameObject networkErrorText;

	public void ShowAd()
	{
		//connectionTestResult = Network.TestConnection ();

        networkErrorText.GetComponent<Text>().text = "Ads disabled on web version";
        networkErrorText.SetActive(true);

		/*if (connectionTestResult == ConnectionTesterStatus.Error || connectionTestResult == ConnectionTesterStatus.Undetermined) {
			networkErrorText.SetActive (true);
		}else if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}*/
	}
}

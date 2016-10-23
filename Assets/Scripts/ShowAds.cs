using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAds : MonoBehaviour {

	ConnectionTesterStatus connectionTestResult;

	public GameObject networkErrorText;

	public void ShowAd()
	{
		connectionTestResult = Network.TestConnection ();

		if (connectionTestResult == ConnectionTesterStatus.Error || connectionTestResult == ConnectionTesterStatus.Undetermined) {
			networkErrorText.SetActive (true);
		}else if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}

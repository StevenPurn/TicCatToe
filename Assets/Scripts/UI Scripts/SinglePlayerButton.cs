using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerButton : MonoBehaviour
{

    public Player player;

    public void StartSinglePlayerGame()
    {
        GlobalData.AiPlayer = player;
    }
}

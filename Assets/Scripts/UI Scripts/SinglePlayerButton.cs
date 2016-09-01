using UnityEngine;

public class SinglePlayerButton : MonoBehaviour
{
    public void StartSinglePlayerGame()
    {
        GlobalData.AiPlayer = Player.playerOne;
    }
}

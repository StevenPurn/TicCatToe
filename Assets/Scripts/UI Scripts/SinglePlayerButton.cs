using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerButton : MonoBehaviour
{
    public void StartSinglePlayerGame(Player player, Toggle toggle)
    {
        GlobalData.AiPlayer = player;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayerInitiative : MonoBehaviour {

	public void MENU_ACTION_setPlayerInitiative()
    {
        Debug.Log("Setting player initiative!");
        string playerName = this.transform.GetComponentInChildren<Text>().text;

        foreach (Player player in MatchDatas.getPlayers())
        {
            if (player.getPlayerName().Equals(playerName))
            {
                player.setInitiative();
                Debug.Log(player.getPlayerName() + " now has initiative!");
                Destroy(GameObject.Find("Initiative panel"));
            }
        }
    }
}

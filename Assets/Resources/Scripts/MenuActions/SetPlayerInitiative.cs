using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Sets the initiative for the selected player*/
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
                PhaseHandlerService.nextPhase();

                // THIS IS ONLY FOR TESTING!! IT SKIPS THE ASTEROID PLACEMENT PHASE!!!!!
                //PhaseHandlerService.nextPhase();
                // This call should be placed elsewhere, after asteroid placement is done!!
                MatchHandler.collectUpcomingAvailableShips(true);

                Destroy(GameObject.Find("Initiative panel"));
            }
        }
    }
}

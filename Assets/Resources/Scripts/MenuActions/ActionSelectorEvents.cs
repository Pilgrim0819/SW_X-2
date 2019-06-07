using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionSelectorEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private string actionName;

    public void setActionName(string name)
    {
        this.actionName = name;
    }

    public string getActionName()
    {
        return this.actionName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color background = SquadBuilderConstants.getHighlightPanelBackground();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color background = SquadBuilderConstants.getDefaultAddPilotBackground();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Action name: " + this.actionName);
        Debug.Log("PHASE: " + MatchDatas.getCurrentPhase());

        if (MatchDatas.getCurrentPhase() == MatchDatas.phases.ACTION)
        {
            IToken token = null;

            switch (this.actionName)
            {
                case "focus":
                    token = new FocusToken();
                    break;
                case "evade":
                    token = new EvadeToken();
                    break;
                case "target lock":
                    break;
                case "boost":
                    break;
                case "barrel roll":
                    break;
                case "slam":
                    break;
                case "reinforce":
                    break;
                case "cloak":
                    break;
                case "decloak":
                    break;
                case "coordinate":
                    break;
                case "jam":
                    break;
                case "recover":
                    break;
                case "reload":
                    break;
                case "rotate arc":
                    break;
            }

            if (token != null)
            {
                Debug.Log("Adding token...");
                MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().addToken(token);
                MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().registerPreviousAction(this.actionName);
            }

            // TODO Check if multiple actions can be chosen!
            // TODO Deactivate actions!!!!
        }

        if (GameObject.Find("ActionChoserPopup") != null && MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().getNumOfActions() == 0)
        {
            GameObject.Find("ActionChoserPopup").gameObject.SetActive(false);
            PhaseHandlerService.endActionPhase();
        }
    }
}

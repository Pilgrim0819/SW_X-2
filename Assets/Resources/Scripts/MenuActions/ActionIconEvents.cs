using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionIconEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        Image img = gameObject.GetComponent<Image>();
        img.color = background;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color background = SquadBuilderConstants.getDefaultAddPilotBackground();
        Image img = gameObject.GetComponent<Image>();
        img.color = background;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MatchDatas.getCurrentPhase() == MatchDatas.phases.ACTIVATION)
        {
            IToken token = null;

            switch (actionName)
            {
                case "Focus":
                    token = new FocusToken();
                    break;
            }

            if (token != null)
            {
                MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().addToken(token);

                // TODO Check if multiple actions can be chosen!
                // TODO Deactivate actions!!!!
            }
        }
    }
}

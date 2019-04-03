using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*This fixes the ship's position during setup phase.*/
public class ConfirmPositionEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

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
        Debug.Log("Position fixing button clicked!");
        MatchDatas.getActiveShip().GetComponent<ShipProperties>().setMovable(false);
        MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().setHasBeenActivatedThisRound(true);

        bool allShipsActivated = true;

        foreach (LoadedShip ship in MatchHandler.getAvailablehips())
        {
            if (!ship.isHasBeenActivatedThisRound())
            {
                allShipsActivated = false;
            }
        }

        if (allShipsActivated)
        {
            MatchHandler.initNextShips(true);
        }

        this.gameObject.SetActive(false);
    }
}

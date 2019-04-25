using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*This fixes the ship's position during setup phase.*/
public class ConfirmPositionEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject target;

    public void setTarget(GameObject t)
    {
        this.target = t;
    }

    public GameObject getTarget()
    {
        return this.target;
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
        if (this.target != null)
        {
            if (this.target.transform.GetComponent<ShipProperties>() != null)
            {
                Debug.Log("It's a ship!!");
            } else
            {
                Debug.Log("It's not a ship!!");
            }
        }

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
            MatchHandler.collectUpcomingAvailableShips(true);
        }

        this.gameObject.SetActive(false);
    }
}

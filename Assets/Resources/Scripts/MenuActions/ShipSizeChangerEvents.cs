using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*Changes the chosen ship size -> makes the ships' list reload*/
public class ShipSizeChangerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string size;

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
        LocalDataWrapper.getPlayer().setChosenSize(size);
        LocalDataWrapper.getPlayer().setSelectedShip(null);
    }
}

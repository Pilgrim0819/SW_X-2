using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ShipsXMLCSharp;

public class SquadBuilderShipPanelEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Ship ship;
    private Color prevColor;

    public void setShip(Ship ship)
    {
        this.ship = ship;
    }

    public Ship getShip()
    {
        return this.ship;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image img = gameObject.GetComponent<Image>();

        prevColor = img.color;
        img.color = SquadBuilderConstants.getHighlightPanelBackground();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Image img = gameObject.GetComponent<Image>();

        img.color = prevColor == null ? SquadBuilderConstants.getDefaultPanelBackground() : prevColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        resetPanelBackgrounds();

        PlayerDatas.setSelectedShip(ship);
        Image img = gameObject.GetComponent<Image>();

        img.color = SquadBuilderConstants.getSelectedPanelBackground();
    }

    public void resetPanelBackgrounds()
    {
        GameObject panelHolder = gameObject.transform.parent.gameObject;

        foreach (Transform child in panelHolder.GetComponentInChildren<Transform>())
        {
            GameObject current = child.gameObject;
            Image img = current.GetComponent<Image>();

            img.color = SquadBuilderConstants.getDefaultPanelBackground();
        }
    }
}

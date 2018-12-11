using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PilotsXMLCSharp;

public class SquadBuilderPilotPanelEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Pilot pilot;

    public void setShip(Pilot pilot)
    {
        this.pilot = pilot;
    }

    public Pilot getShip()
    {
        return this.pilot;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color background = SquadBuilderConstants.getHighlightPanelBackground();
        Image img = gameObject.GetComponent<Image>();
        img.color = background;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color background = SquadBuilderConstants.getDefaultPanelBackground();
        Image img = gameObject.GetComponent<Image>();
        img.color = background;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerDatas.setSelectedPilot(pilot);
    }
}

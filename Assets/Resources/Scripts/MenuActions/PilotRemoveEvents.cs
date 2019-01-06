using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PilotsXMLCSharp;

public class PilotRemoveEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Pilot pilot;
    private int pilotId;

    public void setPilot(Pilot pilot)
    {
        this.pilot = pilot;
    }

    public Pilot getPilot()
    {
        return this.pilot;
    }

    public void setPilotId(int id)
    {
        this.pilotId = id;
    }

    public int getPilotId()
    {
        return this.pilotId;
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
        try
        {
            PlayerDatas.removePilotFromSquadron(this.pilot, this.pilotId);
        }
        catch (System.ApplicationException e)
        {
            SystemMessageService.showErrorMsg(e.Message, GameObject.Find("SystemMessagePanel"), 2, null);
        }
    }
}

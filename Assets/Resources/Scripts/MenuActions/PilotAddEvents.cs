using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*Tries to add the selected ship-player pair to the squadron.*/
public class PilotAddEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        try
        {
            PlayerDatas.addPilotToSquadron(PlayerDatas.getSelectedPilot());
        } catch (System.ApplicationException e)
        {
            SystemMessageService.showErrorMsg(e.Message, GameObject.Find("SystemMessagePanel"), 2, null);
        }
    }
}

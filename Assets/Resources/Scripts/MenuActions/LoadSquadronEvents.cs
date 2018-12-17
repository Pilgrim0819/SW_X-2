using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadSquadronEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        // TODO Use selector to make the user select, which squadron they want to load!
        string squadronName = "Name 1";

        SquadPersistenceUtil.loadSquadron(squadronName);
    }
}

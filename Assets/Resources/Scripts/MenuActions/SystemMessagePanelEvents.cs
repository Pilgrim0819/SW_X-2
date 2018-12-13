using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemMessagePanelEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        // Close system message panel (destroy it)
        Destroy(gameObject.transform.parent.gameObject);
    }
}

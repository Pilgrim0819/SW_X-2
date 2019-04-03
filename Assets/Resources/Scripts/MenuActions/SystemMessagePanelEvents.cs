using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void PointerClickCallback();

/*General popup for system messages*/
public class SystemMessagePanelEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private PointerClickCallback pointerClickCallback;

    public void setCallback(PointerClickCallback callback)
    {
        pointerClickCallback = callback;
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
        // DEFAULT ACTION: Close system message panel (destroy it)
        Destroy(gameObject.transform.parent.gameObject);

        if (pointerClickCallback != null)
        {
            pointerClickCallback();
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*Calls the persistence util to load a squadron by name*/
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
        string squadronName = gameObject.transform.Find("Name").gameObject.GetComponent<UnityEngine.UI.Text>().text;

        SquadPersistenceUtil.loadSquadron(squadronName);
        LocalDataWrapper.getPlayer().toggleLoadingSquadrons();
    }
}

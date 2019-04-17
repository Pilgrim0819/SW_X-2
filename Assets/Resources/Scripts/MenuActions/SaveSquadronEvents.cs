using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*Tries to save the current squadron under the given name*/
public class SaveSquadronEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        GameObject mainPanel = GameObject.Find("MainPanel");
        string squadronName = null;

        foreach (InputField inputField in mainPanel.GetComponentsInChildren<InputField>())
        {
            if (inputField.name == "SquadronNameInput")
            {
                foreach (Text text in inputField.GetComponentsInChildren<Text>())
                {
                    if (text.gameObject.name != "Placeholder")
                        squadronName = text.text;
                }
            }
        }

        SquadPersistenceUtil.saveSquadron(squadronName);
    }
}

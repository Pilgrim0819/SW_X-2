using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FactionPanelVents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string faction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.Find("Image/Overlay").gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.Find("Image/Overlay").gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerDatas.setChosenSide(faction);
        SceneManager.LoadScene("SquadronBuilder", LoadSceneMode.Single);
    }
}

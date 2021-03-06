﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*Sets the chosen side for current player*/
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
        //TODO Check, if side can be chosen (if (squadTotal == 0){...})
        LocalDataWrapper.getPlayer().setChosenSide(faction);
        SceneManager.LoadScene("SquadronBuilder", LoadSceneMode.Single);
    }
}

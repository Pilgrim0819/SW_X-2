﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ShipsXMLCSharp;

public class UpgradeSlotEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private string upgradeType;
    private int slotId;
    private LoadedShip ship;

    public void setUpgradeType(string type)
    {
        this.upgradeType = type;
    }

    public string getUpgradeType()
    {
        return this.upgradeType;
    }

    public void setSlotId(int id)
    {
        this.slotId = id;
    }

    public int getSlotId()
    {
        return this.slotId;
    }

    public void setShip(LoadedShip ship)
    {
        this.ship = ship;
    }

    public LoadedShip getShip()
    {
        return this.ship;
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
        Debug.Log("Setting upgradeslot player data - slot: " + this.slotId + ", type: " + this.upgradeType + ", ship: " + this.ship.getShip().ShipName);

        PlayerDatas.setChosenLoadedShip(this.ship);
        PlayerDatas.setChosenUpgraType(this.upgradeType);
        PlayerDatas.setChosenSlotId(this.slotId);
    }
}
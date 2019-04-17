using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UpgradesXMLCSharp;

/*Adds the selected upgrade for the current ship's current slot*/
public class UpgradePanelEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Upgrade upgrade;
    private LoadedShip ship;
    private int slotId;

    public void setUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
    }

    public Upgrade getUpgrade()
    {
        return this.upgrade;
    }

    public void setShip(LoadedShip ship)
    {
        this.ship = ship;
    }

    public LoadedShip getShip()
    {
        return this.ship;
    }

    public void setSlotId(int slotId)
    {
        this.slotId = slotId;
    }

    public int getSlotId()
    {
        return this.slotId;
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
        PlayerDatas.addUpgradeToShip(ship, upgrade, slotId);
        PlayerDatas.setChosenLoadedShip(null);
        PlayerDatas.setChosenSlotId(0);
        PlayerDatas.setChosenUpgraType("");
    }
}

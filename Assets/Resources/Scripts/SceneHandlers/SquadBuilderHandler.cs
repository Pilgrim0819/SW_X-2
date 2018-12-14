using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShipsXMLCSharp;
using PilotsXMLCSharp;
using UpgradesXMLCSharp;

public class SquadBuilderHandler : MonoBehaviour {

    public GameObject squadPointsHolder;
    public GameObject shipsScroll;
    public GameObject pilotsScroll;
    public GameObject factionIconHolder;
    public GameObject shipDataPreview;
    public GameObject pilotDataPreview;
    public GameObject SquadronHolderContent;
    public GameObject UpgradesPopup;

    private string prevChosenShip = "";
    private string prevChosenPilot = "";
    private int prevSquadronSize = 0;
    private int prevSquadronCost = 0;
    private bool upgradesPopupActive = false;

    private Ships ships;
    private Pilots pilots;
    private UpgradesXMLCSharp.Upgrades upgrades;

    void Start () {
        /*********************************TODO remove when testing is done!!*/
        if (PlayerDatas.getChosenSide() == null || PlayerDatas.getChosenSide().Equals(""))
        {
            PlayerDatas.setChosenSide(SquadBuilderConstants.FACTION_REBELS);
        }

        if (PlayerDatas.getChosenSize() == null || PlayerDatas.getChosenSize().Equals(""))
        {
            PlayerDatas.setChosenSize("small");
        }
        /*********************************TODO remove when testing is done!!*/

        this.ships = SquadBuilderUtil.loadShipsForChosenSide();
        this.pilots = SquadBuilderUtil.loadPilotsForEachShip(this.ships);
        this.upgrades = SquadBuilderUtil.loadUpgrades(PlayerDatas.getChosenSide(), PlayerDatas.getChosenSize());

        showChosenSideIcon();
        showCurrentSquadPoints();
        showShips();
    }

    void Update()
    {
        showCurrentSquadPoints();

        //By default, load the first ship's pilots as it would be selected...
        if (PlayerDatas.getSelectedShip() == null)
        {
            PlayerDatas.setSelectedShip(ships.Ship[0]);
        }

        if (!prevChosenShip.Equals(PlayerDatas.getSelectedShip().ShipId))
        {
            showPilots();
            showShipDataPreview();
            prevChosenShip = PlayerDatas.getSelectedShip().ShipId;
            prevChosenPilot = "";
        }

        if (!prevChosenPilot.Equals(PlayerDatas.getSelectedPilot().Name))
        {
            showPilotDataPreview();
            prevChosenPilot = PlayerDatas.getSelectedPilot().Name;
        }
        
        // TODO Examine if deleting the last ship from squadron causes any problem (maybe not calling re-render as the List is null??)
        if ((PlayerDatas.getSquadron() != null && prevSquadronSize != PlayerDatas.getSquadron().Count) || prevSquadronCost != PlayerDatas.getCumulatedSquadPoints())
        {
            prevSquadronCost = PlayerDatas.getCumulatedSquadPoints();
            prevSquadronSize = PlayerDatas.getSquadron().Count;
            showSquadron();
        }

        if (!PlayerDatas.getChosenUpgradeType().Equals("") && PlayerDatas.getChosenSlotId() != 0 && PlayerDatas.getChosenLoadedShip() != null)
        {
            showUpgradesPopup();
        } else
        {
            if (upgradesPopupActive)
            {
                closeUpgradesPopup();
            }
        }
    }

    private void showCurrentSquadPoints()
    {
        squadPointsHolder.GetComponent<Text>().text = PlayerDatas.getCumulatedSquadPoints() + "/" + PlayerDatas.getPointsToSpend();
    }

    private void showShips()
    {
        int shipIndex = 0;

        foreach (Ship ship in ships.Ship)
        {
            Transform shipPanelPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.SHIP_NAME_PANEL);
            RectTransform rt = (RectTransform)shipPanelPrefab;
            float shipPanelHeight = rt.rect.height;

            Transform shipPanel = (Transform)GameObject.Instantiate(
                shipPanelPrefab,
                new Vector3(SquadBuilderConstants.SHIP_PANEL_X_OFFSET, (shipIndex * shipPanelHeight * -1) + SquadBuilderConstants.SHIP_PANEL_Y_OFFSET, SquadBuilderConstants.SHIP_PANEL_Z_OFFSET),
                Quaternion.identity
            );

            shipPanel.transform.SetParent(shipsScroll.transform, false);
            shipPanel.transform.Find("ShipName").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.ShipName.ToLower();
            
            SquadBuilderShipPanelEvents shipPanelUIEvent = shipPanel.transform.GetComponent<SquadBuilderShipPanelEvents>();
            shipPanelUIEvent.setShip(ship);

            shipIndex++;
        }
    }

    private void showPilots()
    {
        SquadBuilderUtil.resetScrollView(pilotsScroll);

        int pilotIndex = 0;

        foreach (PilotsXMLCSharp.Pilot pilot in pilots.Pilot)
        {
            if (pilot.ShipId.Equals(PlayerDatas.getSelectedShip().ShipId))
            {
                //By default, load the first pilot as it would be selected...
                if (pilotIndex == 0)
                {
                    PlayerDatas.setSelectedPilot(pilot);
                }

                Transform pilotPanelPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.PILOT_NAME_PANEL);
                RectTransform rt = (RectTransform)pilotPanelPrefab;
                float pilotPanelHeight = rt.rect.height;

                Transform pilotPanel = (Transform)GameObject.Instantiate(
                    pilotPanelPrefab,
                    new Vector3(SquadBuilderConstants.PILOT_PANEL_X_OFFSET, (pilotIndex * pilotPanelHeight * -1) + SquadBuilderConstants.PILOT_PANEL_Y_OFFSET, SquadBuilderConstants.PILOT_PANEL_Z_OFFSET),
                    Quaternion.identity
                );

                pilotPanel.transform.SetParent(pilotsScroll.transform, false);
                pilotPanel.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Name.ToLower();

                SquadBuilderPilotPanelEvents pilotPanelUIEvent = pilotPanel.transform.GetComponent<SquadBuilderPilotPanelEvents>();
                pilotPanelUIEvent.setShip(pilot);

                pilotIndex++;
            }
        }
    }

    private void showChosenSideIcon()
    {
        string chosenSide = PlayerDatas.getChosenSide();
        Sprite sideIcon = null;

        switch(chosenSide)
        {
            case SquadBuilderConstants.FACTION_REBELS:
                sideIcon = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + SquadBuilderConstants.REBEL_ICON_IMAGE);
                break;
            case SquadBuilderConstants.FACTION_EMPIRE:
                sideIcon = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + SquadBuilderConstants.EMPIRE_ICON_IMAGE);
                break;
        }

        factionIconHolder.GetComponent<Image>().sprite = sideIcon;
    }

    private void showShipDataPreview()
    {
        SquadBuilderUtil.resetImagesInGameObject(shipDataPreview, "ShipDataManeuvers/ShipManeuvers");

        Ship shipToShow = PlayerDatas.getSelectedShip();

        if (shipToShow != null)
        {
            shipDataPreview.transform.Find("ShipDataShipName/ShipName").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.ShipName.ToLower();
            shipDataPreview.transform.Find("ShipDataShipDescription/ShipDescription").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.ShipDescription;

            shipDataPreview.transform.Find("ShipDataShipAttributes/ShipDataAttackPower/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.Weapon.ToString();
            shipDataPreview.transform.Find("ShipDataShipAttributes/ShipDataAgility/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.Agility.ToString();
            shipDataPreview.transform.Find("ShipDataShipAttributes/ShipDataShield/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.Shield.ToString();
            shipDataPreview.transform.Find("ShipDataShipAttributes/ShipDataHull/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.Hull.ToString();

            string actions = "Actions: \n";

            foreach (string action in shipToShow.Actions.Action)
            {
                actions += "* " + action + "\n";
            }

            shipDataPreview.transform.Find("ShipDataActions/ShipActions").gameObject.GetComponent<UnityEngine.UI.Text>().text = actions;

            foreach (Maneuver maneuver in shipToShow.Maneuvers.Maneuver)
            {
                Image image = null;
                Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + maneuver.Bearing + "_" + maneuver.Difficulty);
                string maneuverHolderName = maneuver.Speed + "_" + maneuver.Bearing;

                if (maneuverHolderName.Contains("koiogran") || maneuverHolderName.Contains("segnor") || maneuverHolderName.Contains("tallon"))
                {
                    if (maneuverHolderName.Contains("left"))
                    {
                        shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>().sprite = sprite;
                        image = shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>();
                    } else if (maneuverHolderName.Contains("right"))
                    {
                        shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_right/Image").gameObject.GetComponent<Image>().sprite = sprite;
                        image = shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_right/Image").gameObject.GetComponent<Image>();
                    } else
                    {
                        shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>().sprite = sprite;
                        image = shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>();
                    }
                } else
                {
                    shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuverHolderName + "/Image").gameObject.GetComponent<Image>().sprite = sprite;
                    image = shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuverHolderName + "/Image").gameObject.GetComponent<Image>();
                }

                if (image != null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
                }
            }
        }
    }

    private void showPilotDataPreview()
    {
        SquadBuilderUtil.resetImagesInGameObject(pilotDataPreview, "PilotDataUpgradeSlots");

        Pilot pilotToShow = PlayerDatas.getSelectedPilot();

        if (pilotToShow != null)
        {
            string pilotName = pilotToShow.Unique ? "*" + pilotToShow.Name.ToLower() : pilotToShow.Name.ToLower();

            pilotDataPreview.transform.Find("PilotLevel/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilotToShow.Level.ToString();
            pilotDataPreview.transform.Find("PilotCost/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilotToShow.Cost.ToString();
            pilotDataPreview.transform.Find("PilotDataPilotName/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilotName;
            pilotDataPreview.transform.Find("PilotDataPilotDescription/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilotToShow.Text;

            int upgradeIndex = 0;

            foreach (UpgradeSlot upgrade in pilotToShow.UpgradeSlots.UpgradeSlot)
            {
                Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + upgrade.Type);

                Transform upgradeImageHolderPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.UPGRADE_IMAGE_HOLDER);
                RectTransform rt = (RectTransform)upgradeImageHolderPrefab;
                float upgradeImageHolderWidth = rt.rect.width;

                Transform upgradeImageHolder = (Transform)GameObject.Instantiate(
                    upgradeImageHolderPrefab,
                    new Vector3((upgradeIndex * upgradeImageHolderWidth) +  SquadBuilderConstants.UPGRADE_IMAGE_X_OFFSET, SquadBuilderConstants.UPGRADE_IMAGE_Y_OFFSET, SquadBuilderConstants.UPGRADE_IMAGE_Z_OFFSET),
                    Quaternion.identity
                );

                Transform upgradesBar = pilotDataPreview.transform.Find("PilotDataUpgradeSlots");
                Image upgradeImage = upgradeImageHolder.gameObject.GetComponent<Image>();

                upgradeImageHolder.transform.SetParent(upgradesBar, false);
                upgradeImage.sprite = sprite;
                upgradeImage.color = new Color(upgradeImage.color.r, upgradeImage.color.g, upgradeImage.color.b, 1.0f);

                upgradeIndex++;
            }
        }
    }

    private void showSquadron()
    {
        SquadBuilderUtil.resetScrollView(SquadronHolderContent);

        int shipPanelIndex = 0;

        foreach (LoadedShip loadedShip in PlayerDatas.getSquadron())
        {
            Transform shipPanelPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.SQUADRON_SHIP_HOLDER);
            RectTransform rt = (RectTransform)shipPanelPrefab;
            float shipPanelHeight = rt.rect.height;

            Transform shipPanel = (Transform)GameObject.Instantiate(
                shipPanelPrefab,
                new Vector3(SquadBuilderConstants.SQUADRON_SHIP_PANEL_X_OFFSET, (shipPanelIndex * shipPanelHeight * -1) + SquadBuilderConstants.SQUADRON_SHIP_PANEL_Y_OFFSET, SquadBuilderConstants.SQUADRON_SHIP_PANEL_Z_OFFSET),
                Quaternion.identity
            );

            shipPanel.transform.SetParent(SquadronHolderContent.transform, false);

            PilotRemoveEvents pilotRemoveEvent = shipPanel.transform.Find("DeleteButton").gameObject.GetComponent<PilotRemoveEvents>();
            pilotRemoveEvent.setPilot(loadedShip.getPilot());
            pilotRemoveEvent.setPilotId(loadedShip.getPilotId());

            Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + loadedShip.getShip().ShipId);
            shipPanel.transform.Find("ShipImage").gameObject.GetComponent<Image>().sprite = sprite;
            Image image = shipPanel.transform.Find("ShipImage").gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);

            shipPanel.transform.Find("ShipImage/PilotLevel").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getPilot().Level.ToString();
            shipPanel.transform.Find("ShipImage/PilotCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getPilot().Cost.ToString();
            shipPanel.transform.Find("AttackPower").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getShip().Weapon.ToString();
            shipPanel.transform.Find("Agility").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getShip().Agility.ToString();
            shipPanel.transform.Find("Shield").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getShip().Shield.ToString();
            shipPanel.transform.Find("Hull").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getShip().Hull.ToString();
            shipPanel.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getPilot().Name.ToLower();
            shipPanel.transform.Find("PilotDescription").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getPilot().Text;

            int upgradeSlotIndex = 0;

            foreach (UpgradeSlot upgrade in loadedShip.getPilot().UpgradeSlots.UpgradeSlot)
            {
                upgrade.upgradeSlotId = upgradeSlotIndex + 1;

                Transform upgradeSlotPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.UPGRADE_SLOT);
                rt = (RectTransform)upgradeSlotPrefab;
                float upgradeSlotWidth = rt.rect.width;

                Transform upgradeSlot = (Transform)GameObject.Instantiate(
                    upgradeSlotPrefab,
                    new Vector3((upgradeSlotIndex * upgradeSlotWidth) + SquadBuilderConstants.UPGRADE_SLOT_X_OFFSET, SquadBuilderConstants.UPGRADE_SLOT_Y_OFFSET, SquadBuilderConstants.UPGRADE_SLOT_Z_OFFSET),
                    Quaternion.identity
                );

                upgradeSlot.transform.SetParent(shipPanel.transform, false);

                sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + upgrade.Type);
                upgradeSlot.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
                image = upgradeSlot.transform.Find("Image").gameObject.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);

                UpgradeSlotEvents upgradeSlotEvents = upgradeSlot.transform.GetComponent<UpgradeSlotEvents>();
                upgradeSlotEvents.setUpgradeType(upgrade.Type);
                upgradeSlotEvents.setShip(loadedShip);
                upgradeSlotEvents.setSlotId(upgrade.upgradeSlotId);

                if (upgrade.upgrade == null)
                {
                    upgradeSlot.transform.Find("Cost").gameObject.GetComponent<UnityEngine.UI.Text>().text = "";
                    upgradeSlot.transform.Find("UpgradeDescription/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "EMPTY";
                } else
                {
                    upgradeSlot.transform.Find("Cost").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.upgrade.Cost.ToString();
                    upgradeSlot.transform.Find("UpgradeDescription/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.upgrade.Description;

                    // TODO If upgrade has attack power (and range), show them as well...
                }

                upgradeSlotIndex++;
            }

            shipPanelIndex++;
        }
    }

    private void showUpgradesPopup()
    {
        UpgradesPopup.transform.Find("UpgradeType").gameObject.GetComponent<UnityEngine.UI.Text>().text = PlayerDatas.getChosenUpgradeType();

        Transform scrollViewContent = UpgradesPopup.transform.Find("Scroll View/Viewport/Content");
        Transform upgradePanelPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.UPGRADE_PANEL);
        Transform upgradePanel = (Transform)GameObject.Instantiate(
            upgradePanelPrefab,
            new Vector3(SquadBuilderConstants.UPGRADE_PANEL_X_OFFSET, SquadBuilderConstants.UPGRADE_PANEL_Y_OFFSET, SquadBuilderConstants.UPGRADE_PANEL_Z_OFFSET),
            Quaternion.identity
        );

        upgradePanel.transform.SetParent(scrollViewContent.transform, false);
        upgradePanel.transform.Find("Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = "none";

        UpgradePanelEvents upgradePanelEvents = upgradePanel.GetComponent<UpgradePanelEvents>();

        upgradePanelEvents.setShip(PlayerDatas.getChosenLoadedShip());
        upgradePanelEvents.setSlotId(PlayerDatas.getChosenSlotId());
        upgradePanelEvents.setUpgrade(null);

        int upgradePanelIndex = 1;

        foreach (Upgrade upgrade in this.upgrades.Upgrade)
        {
            if (upgrade.Type.Equals(PlayerDatas.getChosenUpgradeType()))
            {
                upgradePanelPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.UPGRADE_PANEL);
                RectTransform rt = (RectTransform)upgradePanelPrefab;
                float upgradePanelHeight = rt.rect.height;

                upgradePanel = (Transform)GameObject.Instantiate(
                    upgradePanelPrefab,
                    new Vector3(SquadBuilderConstants.UPGRADE_PANEL_X_OFFSET, (upgradePanelIndex * upgradePanelHeight * -1) + SquadBuilderConstants.UPGRADE_PANEL_Y_OFFSET, SquadBuilderConstants.UPGRADE_PANEL_Z_OFFSET),
                    Quaternion.identity
                );

                upgradePanel.transform.SetParent(scrollViewContent.transform, false);

                upgradePanel.transform.Find("Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.Name;
                upgradePanel.transform.Find("Description").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.Description;
                upgradePanel.transform.Find("Cost").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.Cost.ToString();

                // TODO Add attackpower and range attributes to Upgrade model!!!!
                /*if (upgrade.AttackPower != 0 && upgrade.Range != 0)
                {
                    upgradePanel.transform.Find("AttackPower").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.AttackPower.ToString();
                    upgradePanel.transform.Find("Range").gameObject.GetComponent<UnityEngine.UI.Text>().text = upgrade.Range.ToString();
                }*/

                upgradePanelEvents = upgradePanel.GetComponent<UpgradePanelEvents>();

                upgradePanelEvents.setShip(PlayerDatas.getChosenLoadedShip());
                upgradePanelEvents.setSlotId(PlayerDatas.getChosenSlotId());
                upgradePanelEvents.setUpgrade(upgrade);

                upgradePanelIndex++;
            }
        }

        UpgradesPopup.SetActive(true);
        upgradesPopupActive = true;
    }

    private void closeUpgradesPopup()
    {
        UpgradesPopup.SetActive(false);
        SquadBuilderUtil.resetScrollView(UpgradesPopup.transform.Find("Scroll View/Viewport/Content").gameObject);
    }
}

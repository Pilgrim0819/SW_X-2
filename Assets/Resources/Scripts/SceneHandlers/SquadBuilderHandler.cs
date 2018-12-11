using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ShipsXMLCSharp;
using PilotsXMLCSharp;

public class SquadBuilderHandler : MonoBehaviour {

    public GameObject squadPointsHolder;
    public GameObject shipsScroll;
    public GameObject pilotsScroll;
    public GameObject factionIconHolder;
    public GameObject shipDataPreview;
    public GameObject pilotDataPreview;

    private string prevChosenShip = "";
    private string prevChosenPilot = "";

    private Ships ships;
    private Pilots pilots;

    void Start () {
        showChosenSideIcon();
        showCurrentSquadPoints();
        loadShipsForChosenSide();
        loadPilotsForEachShip();

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
    }

    private void loadShipsForChosenSide()
    {
        string chosenSide = PlayerDatas.getChosenSide();

        /*********************************TODO remove when testing is done!!*/
        if (chosenSide == null || chosenSide.Equals(""))
        {
            chosenSide = "Rebels";
        }
        /*********************************TODO remove when testing is done!!*/
        
        this.ships = new Ships();

        switch (chosenSide)
        {
            case "Rebels":
                this.ships = XMLLoader.getShips("rebel_ships.xml");
                break;
            case "Empire":
                this.ships = XMLLoader.getShips("imperial_ships.xml");
                break;
        }
    }

    private void loadPilotsForEachShip()
    {
        foreach (Ship ship in this.ships.Ship)
        {
            if (this.pilots == null || this.pilots.Pilot == null || this.pilots.Pilot.Capacity == 0)
            {
                this.pilots = XMLLoader.getPilots(ship.ShipId.ToString() + "_pilots.xml");
            }
            else
            {
                this.pilots.Pilot.AddRange(XMLLoader.getPilots(ship.ShipId.ToString() + "_pilots.xml").Pilot);
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
        resetPilotsScroll(pilotsScroll);

        int pilotIndex = 0;

        foreach (PilotsXMLCSharp.Pilot pilot in pilots.Pilot)
        {
            if (pilot.ShipId.Equals(PlayerDatas.getSelectedShip().ShipId))
            {
                //By default, load the first pilot as it would be selected...
                if (PlayerDatas.getSelectedPilot() == null)
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

        /*********************************TODO remove when testing is done!!*/
        if (chosenSide == null || chosenSide.Equals(""))
        {
            chosenSide = SquadBuilderConstants.FACTION_REBELS;
        }
        /*********************************TODO remove when testing is done!!*/

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
        resetManeuverImages();

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
        resetUpgradeImages();

        Pilot pilotToShow = PlayerDatas.getSelectedPilot();

        if (pilotToShow != null)
        {
            string pilotName = pilotToShow.Unique ? "*" + pilotToShow.Name : pilotToShow.Name;
            string upgrades = "Upgrade: ";

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

    private void resetPilotsScroll(GameObject pilotsScroll)
    {
        foreach (Transform child in pilotsScroll.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void resetManeuverImages()
    {
        foreach (Transform child in shipDataPreview.transform.Find("ShipDataManeuvers/ShipManeuvers").GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Image>() != null)
            {
                Image image = child.GetComponent<Image>();

                if (image != null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.0f);
                }
            }
        }
    }

    private void resetUpgradeImages()
    {
        foreach (Transform child in pilotDataPreview.transform.Find("PilotDataUpgradeSlots").GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Image>() != null)
            {
                Image image = child.GetComponent<Image>();

                if (image != null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.0f);
                }
            }
        }
    }
}

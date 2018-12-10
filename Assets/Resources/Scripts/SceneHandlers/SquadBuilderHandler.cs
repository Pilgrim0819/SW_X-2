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

    private string prevChosenShip = "";

    private Ships ships;
    private Pilots pilots;

    private const string IMAGE_FOLDER_NAME = "images";
    private const string REBEL_ICON_IMAGE = "rebels";
    private const string EMPIRE_ICON_IMAGE = "imperials";
    private const string PREFABS_FOLDER_NAME = "Prefabs";
    private const string SHIP_NAME_PANEL = "ShipNamePanel";
    private const string PILOT_NAME_PANEL = "PilotNamePanel";
    private const string FACTION_REBELS = "Rebels";
    private const string FACTION_EMPIRE = "Empire";

    private const float SHIP_PANEL_X_OFFSET = 0.0f;
    private const float SHIP_PANEL_Y_OFFSET = -15.0f;
    private const float SHIP_PANEL_Z_OFFSET = 0.0f;

    private const float PILOT_PANEL_X_OFFSET = 0.0f;
    private const float PILOT_PANEL_Y_OFFSET = -15.0f;
    private const float PILOT_PANEL_Z_OFFSET = 0.0f;

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

        if (!prevChosenShip.Equals(PlayerDatas.getChosenShip()))
        {
            showPilots();
            showShipDataPreview();
            prevChosenShip = PlayerDatas.getChosenShip();
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
            //Sprite shipSprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + ship.ShipName.Replace("/", ""));
            Transform shipPanelPrefab = Resources.Load<Transform>(PREFABS_FOLDER_NAME + "/" + SHIP_NAME_PANEL);
            RectTransform rt = (RectTransform)shipPanelPrefab;
            float shipPanelHeight = rt.rect.height;

            Transform shipPanel = (Transform)GameObject.Instantiate(
                shipPanelPrefab,
                new Vector3(SHIP_PANEL_X_OFFSET, (shipIndex * shipPanelHeight * -1) + SHIP_PANEL_Y_OFFSET, SHIP_PANEL_Z_OFFSET),
                Quaternion.identity
            );

            shipPanel.transform.SetParent(shipsScroll.transform, false);
            shipPanel.transform.Find("ShipName").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.ShipName.ToString();

            //Set parameter for click handler script here!!!
            SquadBuilderShipPanelEvents shipPanelUIEvent = shipPanel.transform.GetComponent<SquadBuilderShipPanelEvents>();
            shipPanelUIEvent.setShip(ship);

            shipIndex++;
        }
    }

    private void showPilots()
    {
        resetPilotsScroll(pilotsScroll);

        string chosenShip = PlayerDatas.getChosenShip();

        //By default, load the first ship's pilots as it would be selected...
        if (chosenShip == null || chosenShip.Equals(""))
        {
            chosenShip = ships.Ship[0].ShipId.ToString();
            PlayerDatas.setChosenShip(chosenShip);
        }

        int pilotIndex = 0;

        foreach (PilotsXMLCSharp.Pilot pilot in pilots.Pilot)
        {
            if (pilot.ShipId.Equals(chosenShip))
            {
                Transform pilotPanelPrefab = Resources.Load<Transform>(PREFABS_FOLDER_NAME + "/" + PILOT_NAME_PANEL);
                RectTransform rt = (RectTransform)pilotPanelPrefab;
                float pilotPanelHeight = rt.rect.height;

                Transform pilotPanel = (Transform)GameObject.Instantiate(
                    pilotPanelPrefab,
                    new Vector3(PILOT_PANEL_X_OFFSET, (pilotIndex * pilotPanelHeight * -1) + PILOT_PANEL_Y_OFFSET, PILOT_PANEL_Z_OFFSET),
                    Quaternion.identity
                );

                pilotPanel.transform.SetParent(pilotsScroll.transform, false);
                pilotPanel.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Name.ToString();

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
            chosenSide = FACTION_REBELS;
        }
        /*********************************TODO remove when testing is done!!*/

        switch(chosenSide)
        {
            case FACTION_REBELS:
                sideIcon = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + REBEL_ICON_IMAGE);
                break;
            case FACTION_EMPIRE:
                sideIcon = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + EMPIRE_ICON_IMAGE);
                break;
        }

        factionIconHolder.GetComponent<Image>().sprite = sideIcon;
    }

    private void showShipDataPreview()
    {
        resetManeuverImages();

        Ship shipToShow = getChosenShipData(PlayerDatas.getChosenShip());

        if (shipToShow != null)
        {
            shipDataPreview.transform.Find("ShipDataShipName/ShipName").gameObject.GetComponent<UnityEngine.UI.Text>().text = shipToShow.ShipName;
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
                Sprite sprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + maneuver.Bearing + "_" + maneuver.Difficulty);
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
        // TODO
    }

    private void resetPilotsScroll(GameObject pilotsScroll)
    {
        foreach (Transform child in pilotsScroll.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private Ship getChosenShipData(string shipId)
    {
        foreach (Ship ship in ships.Ship)
        {
            if (ship.ShipId.Equals(shipId))
            {
                return ship;
            }
        }

        return null;
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
}

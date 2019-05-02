using ShipsXMLCSharp;
using UnityEngine;
using UnityEngine.UI;

/*General UI handler to init/delete 2D UI elements*/
public class GUIHandler {

    private const string PREFAB_FOLDER_NAME = "Prefabs/ShipCardPrefab";
    private const string IMAGE_FOLDER_NAME = "images";
    private const float actionImageSize = 15.0f;

    public GameObject TargetCanvas;

    public void showActiveShipsCard(LoadedShip ship)
    {
        Transform shipCardPrefab = Resources.Load<Transform>(PREFAB_FOLDER_NAME);
        Sprite shipSprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + ship.getShip().ShipName.Replace("/", ""));

        Transform shipCard = (Transform)GameObject.Instantiate(
            shipCardPrefab,
            new Vector3(100, 100, 0),
            Quaternion.identity
        );

        shipCard.transform.SetParent(TargetCanvas.transform, false);
        shipCard.transform.Find("Ship Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().ShipName.ToString();
        shipCard.transform.Find("Ship Image").gameObject.GetComponent<Image>().sprite = shipSprite;
        shipCard.transform.Find("Attack Power Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Weapon.ToString();
        shipCard.transform.Find("Agility Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Agility.ToString();
        shipCard.transform.Find("Hull Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Hull.ToString();
        shipCard.transform.Find("Shield Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Shield.ToString();
    }

    public void hideActiveShipsCard(LoadedShip ship)
    {

    }

    public void showPilotCard(GameObject target)
    {
        LoadedShip ship = MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip();
        string side = "";
        Image image;
        Sprite sprite;

        foreach (Player player in MatchDatas.getPlayers())
        {
            foreach (LoadedShip ls in player.getSquadron())
            {
                if (ls.getShip().ShipId == ship.getShip().ShipId && ls.getPilot().Name.Equals(ship.getPilot().Name))
                {
                    side = player.getChosenSide();
                    break;
                }

                if (!side.Equals(""))
                {
                    break;
                }
            }
        }

        target.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Name.ToLower();
        target.transform.Find("ShipType").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().ShipName.ToLower();
        target.transform.Find("Description").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Text.ToLower();
        target.transform.Find("BaseCostHolder/BaseCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Cost.ToString();
        target.transform.Find("ShieldValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Shield.ToString();
        target.transform.Find("HullValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Hull.ToString();
        target.transform.Find("AgilityValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Agility.ToString();
        target.transform.Find("PowerValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Weapon.ToString();
        target.transform.Find("PilotLevelHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Level.ToString();

        int actionIndex = 0;


        // TODO use prefabs instead???
        foreach (string action in ship.getShip().Actions.Action)
        {
            image = null;
            sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + action.Replace(" ", "-"));

            Transform actionIconPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.ACTION_ICON);
            RectTransform rt = (RectTransform)actionIconPrefab;
            float actionIconWidth = rt.rect.width;

            Transform actionIcon = (Transform)GameObject.Instantiate(
                actionIconPrefab,
                new Vector3((actionIndex * actionIconWidth) + SquadBuilderConstants.UPGRADE_IMAGE_X_OFFSET, SquadBuilderConstants.UPGRADE_IMAGE_Y_OFFSET, SquadBuilderConstants.UPGRADE_IMAGE_Z_OFFSET),
                Quaternion.identity
            );

            Transform actionsBar = target.transform.Find("ActionIcons");
            Image actionIconImage = actionIcon.gameObject.GetComponent<Image>();

            actionIcon.transform.SetParent(actionsBar, false);
            actionIconImage.sprite = sprite;
            actionIconImage.color = new Color(actionIconImage.color.r, actionIconImage.color.g, actionIconImage.color.b, 1.0f);

            actionIndex++;
        }

        sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + side + "_pilot_card");
        target.transform.Find("CardImage").gameObject.GetComponent<Image>().sprite = sprite;
        image = target.transform.Find("CardImage").gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);

        sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + ship.getShip().ShipId);
        target.transform.Find("CardImage/ShipImage").gameObject.GetComponent<Image>().sprite = sprite;
        image = target.transform.Find("CardImage/ShipImage").gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);

        target.transform.Find("ShipDataManeuvers").gameObject.SetActive(false);

        target.SetActive(true);
    }

    public void hideGameObject(GameObject target)
    {
        target.SetActive(false);
    }

    public void showGameObject(GameObject target)
    {
        target.SetActive(true);
    }

    public void setGameObjectText(GameObject target, string text)
    {
        target.GetComponent<UnityEngine.UI.Text>().text = text;
    }

    public void showManeuverSelector(GameObject target)
    {
        // TODO Check why this throws NullReferenceException!!
        /*foreach (Transform child in PilotCardPanel.transform.Find("ShipDataManeuvers/ShipManeuvers"))
        {
            if (child.Find("Image").gameObject.GetComponent<ManeuverSelectionEvent>() != null)
            {
                Destroy(child.Find("Image").gameObject.GetComponent<ManeuverSelectionEvent>());
            }
        }*/

        // DUPLACTE FRAGMENT!!!!! (also in squadron builder!)
        foreach (Maneuver maneuver in MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().getShip().Maneuvers.Maneuver)
        {
            Image image = null;
            Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + maneuver.Bearing + "_" + maneuver.Difficulty);
            string maneuverHolderName = maneuver.Speed + "_" + maneuver.Bearing;

            // Maneuver overlay not showing!! Check why....
            Maneuver selectedManeuver = MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().getPlannedManeuver();
            bool isSelected = false;

            if (selectedManeuver != null && maneuver.Speed.Equals(selectedManeuver.Speed) && maneuver.Bearing.Equals(selectedManeuver.Bearing))
            {
                isSelected = true;
            }

            if (maneuverHolderName.Contains("koiogran") || maneuverHolderName.Contains("segnor") || maneuverHolderName.Contains("tallon"))
            {
                if (maneuverHolderName.Contains("left"))
                {
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>().sprite = sprite;
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.AddComponent<ManeuverSelectionEvent>();
                    image = target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>();
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/ManeuverOverlay").gameObject.SetActive(isSelected);
                }
                else if (maneuverHolderName.Contains("right"))
                {
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_right/Image").gameObject.GetComponent<Image>().sprite = sprite;
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_right/Image").gameObject.AddComponent<ManeuverSelectionEvent>();
                    image = target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_right/Image").gameObject.GetComponent<Image>();
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/ManeuverOverlay").gameObject.SetActive(isSelected);
                }
                else
                {
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>().sprite = sprite;
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.AddComponent<ManeuverSelectionEvent>();
                    image = target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/Image").gameObject.GetComponent<Image>();
                    target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/ManeuverOverlay").gameObject.SetActive(isSelected);
                }
            }
            else
            {
                target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuverHolderName + "/Image").gameObject.GetComponent<Image>().sprite = sprite;
                target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuverHolderName + "/Image").gameObject.AddComponent<ManeuverSelectionEvent>();
                image = target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuverHolderName + "/Image").gameObject.GetComponent<Image>();
                target.transform.Find("ShipDataManeuvers/ShipManeuvers/Speed" + maneuver.Speed + "_special_left/ManeuverOverlay").gameObject.SetActive(isSelected);
            }

            if (image != null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }

        target.transform.Find("ShipDataManeuvers").gameObject.SetActive(true);
    }
}

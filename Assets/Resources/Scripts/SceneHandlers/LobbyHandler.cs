using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHandler : MonoBehaviour {
    public GameObject mainCanvas;

    private Mocker mocker = new Mocker();

    private const string PLAYER_PANEL_PREFAB_NAME = "Prefabs/PlayerPanel";
    private const string SHIP_ICON_PREFAB_NAME = "Prefabs/LobbyShipIcon";
    private const string TEXT_SQUAD_TOTAL = "squad total: ";
    private const string TEXT_POINTS = "pts.";
    private const string TEXT_PLAYER_SIDE = "side: ";

    private const float DEFAULT_Z_OFFSET = 0.0f;
    private const float SHIP_ICON_WIDTH = 100.0f;
    private const float SHIP_ICON_HEIGHT = 100.0f;
    private const float SHIP_ICON_X_OFFSET = 50.0f;
    private const float SHIP_ICON_Y_OFFSET = -50.0f;
    private const float SHIP_ICON_MARGIN_LEFT = 15.0f;
    private const float SHIP_ICON_MARGIN_TOP = 15.0f;
    private const float PLAYER_PANEL_X_OFFSET = 320.0f;
    private const float PLAYER_PANEL_Y_OFFSET = -25.0f;

    IEnumerator checkObjectsHaveStopped()
    {
        DiceRollerBase.showDiceArea(1, true);

        Rigidbody[] dice = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
        bool allSleeping = false;

        while (!allSleeping)
        {
            allSleeping = true;

            foreach (Rigidbody dye in dice)
            {
                if (!dye.IsSleeping())
                {
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }

        }

        DiceRollerBase.getDiceResults(dice);
    }

    void Start () {
        mocker.mockPlayerSquadrons();
        DiceRollerBase.setUpDiceRollerBase(ForceMode.VelocityChange, 10.0f);

        string total = "squadron size: " + MatchDatas.getTotalSquadPoints();

        // TODO Show each player's name, squad point total, chosen side, chosen ships (images...)
        int playerIndex = 0;

        foreach (Player player in MatchDatas.getPlayers())
        {
            Transform playerPanelPrefab = Resources.Load<Transform>(PLAYER_PANEL_PREFAB_NAME);
            RectTransform rt = (RectTransform)playerPanelPrefab;
            float playerPanelWidth = rt.rect.width;

            Transform playerPanel = (Transform)GameObject.Instantiate(
                playerPanelPrefab,
                new Vector3((playerIndex * playerPanelWidth) + PLAYER_PANEL_X_OFFSET, PLAYER_PANEL_Y_OFFSET, DEFAULT_Z_OFFSET),
                Quaternion.identity
            );

            playerPanel.transform.SetParent(mainCanvas.transform, false);

            /*PilotRemoveEvents pilotRemoveEvent = shipPanel.transform.Find("DeleteButton").gameObject.GetComponent<PilotRemoveEvents>();
            pilotRemoveEvent.setPilot(loadedShip.getPilot());*/

            /*Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + loadedShip.getShip().ShipId);
            shipPanel.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = sprite;
            Image image = shipPanel.transform.Find("Avatar").gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);*/

            playerPanel.transform.Find("PlayerName").gameObject.GetComponent<UnityEngine.UI.Text>().text = player.getPlayerName();
            playerPanel.transform.Find("PlayerSide").gameObject.GetComponent<UnityEngine.UI.Text>().text = player.getChosenSide();
            playerPanel.transform.Find("SquadTotal").gameObject.GetComponent<UnityEngine.UI.Text>().text = TEXT_SQUAD_TOTAL + player.getCumulatedSquadPoints() + TEXT_POINTS;

            int rowIndex = 0;
            int colIndex = 0;

            foreach (LoadedShip ship in player.getSquadron())
            {
                Transform shipIconPrefab = Resources.Load<Transform>(SHIP_ICON_PREFAB_NAME);

                Transform shipIcon = (Transform)GameObject.Instantiate(
                    shipIconPrefab,
                    new Vector3((colIndex * SHIP_ICON_WIDTH) + SHIP_ICON_X_OFFSET, (rowIndex * SHIP_ICON_HEIGHT) + SHIP_ICON_Y_OFFSET, DEFAULT_Z_OFFSET),
                    Quaternion.identity
                );

                shipIcon.transform.SetParent(playerPanel.transform.Find("ShipIcons"), false);

                Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + ship.getShip().ShipId);
                shipIcon.gameObject.GetComponent<Image>().sprite = sprite;
                Image image = shipIcon.gameObject.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);

                if (colIndex < 5)
                {
                    colIndex++;
                } else
                {
                    colIndex = 0;
                    rowIndex++;
                }
            }

            playerIndex++;
        }
    }
}

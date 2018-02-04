using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ShipsXMLCSharp;

public class OnShipSelectionSceneLoad : MonoBehaviour {
    
    public Transform shipCardPrefab;
    public GameObject cardsHolder;

    private JSONLoader jsonLoader = new JSONLoader();
    private const string IMAGE_FOLDER_NAME = "images";

	// Use this for initialization
	void Start () {
        string chosenSide = PlayerDatas.getChosenSide();
        Ships ships = new Ships();

        switch (chosenSide)
        {
            case "Rebels":
                ships = XMLLoader.getShips("rebel_ships.xml");
                break;
            case "Empire":
                ships = XMLLoader.getShips("imperial_ships.xml");
                break;
        }

        int shipIndex = 0;

        foreach(ShipsXMLCSharp.Ship ship in ships.Ship)
        {
            Sprite shipSprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + ship.ShipName.Replace("/", ""));
            Vector3 position = cardsHolder.transform.position;
            int offsetX = 150;
            int offsetY = -900;
            int shipCardWidth = 200;

            Transform shipCard = (Transform) GameObject.Instantiate(
                shipCardPrefab, 
                new Vector3((position.x - 1000) + (shipCardWidth * shipIndex) + offsetX,position.y + offsetY,position.z), 
                Quaternion.identity
            );

            shipCard.transform.SetParent(cardsHolder.transform, false);
            shipCard.transform.Find("Ship Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.ShipName.ToString();
            shipCard.transform.Find("Ship Image").gameObject.GetComponent<Image>().sprite = shipSprite;
            shipCard.transform.Find("Attack Power Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.Weapon;
            shipCard.transform.Find("Agility Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.Agility;
            shipCard.transform.Find("Hull Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.Hull;
            shipCard.transform.Find("Shield Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.Shield;

            shipIndex++;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

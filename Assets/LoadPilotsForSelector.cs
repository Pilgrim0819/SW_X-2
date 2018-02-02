using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PilotsXMLCSharp;

public class LoadPilotsForSelector : MonoBehaviour {

    public Transform shipCardPrefab;
    public GameObject cardsHolder;

    private const string IMAGE_FOLDER_NAME = "images";

    public void loadPilotsCards()
    {
        string chosenShip = PlayerDatas.getChosenShip();
        Pilots pilots = new Pilots();

        switch (chosenShip)
        {
            case "T-65 X-wing":
                pilots = XMLLoader.getPilots("T-65 X-wing_pilots.xml");
                break;
            case "T-70 X-wing":
                pilots = XMLLoader.getPilots("T-70 X-wing_pilots.xml");
                break;
        }

        int pilotIndex = 0;

        foreach (PilotsXMLCSharp.Pilot pilot in pilots.Pilot)
        {
            Sprite pilotSprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + pilot.Name);
            Vector3 position = cardsHolder.transform.position;
            int offsetX = 150;
            int offsetY = -900;
            int shipCardWidth = 200;

            Transform shipCard = (Transform)GameObject.Instantiate(
                shipCardPrefab,
                new Vector3((position.x - 1000) + (shipCardWidth * pilotIndex) + offsetX, position.y + offsetY, position.z),
                Quaternion.identity
            );

            shipCard.transform.SetParent(cardsHolder.transform, false);
            shipCard.transform.Find("Ship Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Name.ToString();
            shipCard.transform.Find("Ship Image").gameObject.GetComponent<Image>().sprite = pilotSprite;
            shipCard.transform.Find("Attack Power Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Level;
            shipCard.transform.Find("Agility Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Cost;
            shipCard.transform.Find("Hull Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "";
            shipCard.transform.Find("Shield Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "";

            pilotIndex++;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

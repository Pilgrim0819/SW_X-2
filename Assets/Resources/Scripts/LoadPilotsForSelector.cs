using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PilotsXMLCSharp;

public class LoadPilotsForSelector : MonoBehaviour {

    public GameObject cardsHolder;

    private const string IMAGE_FOLDER_NAME = "images";
    private const string PREFAB_FOLDER_NAME = "Prefabs/PilotCardPrefab";
    private const string PILOT_LEVEL_TEXT = "Level: ";
    private const string PILOT_COST_TEXT = "Cost: ";

    public void loadPilotsCards()
    {
        
    }

	// Use this for initialization
	void Start () {
        cardsHolder = GameObject.Find("Ships Scroll");
        string chosenShip = PlayerDatas.getChosenShip();
        Pilots pilots = new Pilots();

        /*********************************TODO remove when testing is done!!*/
        if (chosenShip == null || chosenShip.Equals(""))
        {
            chosenShip = "T-65 X-Wing";
        }
        /*********************************TODO remove when testing is done!!*/

        pilots = XMLLoader.getPilots(chosenShip + "_pilots.xml");

        int pilotIndex = 0;

        foreach (PilotsXMLCSharp.Pilot pilot in pilots.Pilot)
        {
            Transform pilotCardPrefab = Resources.Load<Transform>(PREFAB_FOLDER_NAME);
            Sprite pilotSprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + pilot.Name);
            Vector3 position = cardsHolder.transform.position;
            int offsetX = 150;
            int offsetY = -900;
            int shipCardWidth = 200;

            Transform shipCard = (Transform)GameObject.Instantiate(
                pilotCardPrefab,
                new Vector3((position.x - 1000) + (shipCardWidth * pilotIndex) + offsetX, position.y + offsetY, position.z),
                Quaternion.identity
            );

            shipCard.transform.SetParent(cardsHolder.transform, false);
            shipCard.transform.Find("Pilot Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Name.ToString();
            shipCard.transform.Find("Pilot Image").gameObject.GetComponent<Image>().sprite = pilotSprite;
            shipCard.transform.Find("Pilot Level Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = PILOT_LEVEL_TEXT + pilot.Level;
            shipCard.transform.Find("Pilot Cost Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = PILOT_COST_TEXT + pilot.Cost;
            shipCard.transform.Find("Pilot Description Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = pilot.Text;

            AddPilotToSquardon shipcardPilotScript = shipCard.transform.GetComponent<AddPilotToSquardon>();
            shipcardPilotScript.setPilot(pilot);

            pilotIndex++;
        }
    }
}

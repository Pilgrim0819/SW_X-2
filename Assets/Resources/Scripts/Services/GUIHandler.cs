using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*General UI handler to init/delete 2D UI elements*/
public class GUIHandler : MonoBehaviour {

    private const string PREFAB_FOLDER_NAME = "Prefabs/ShipCardPrefab";
    private const string IMAGE_FOLDER_NAME = "images";

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
}

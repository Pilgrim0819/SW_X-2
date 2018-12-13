using UnityEngine;

public class SystemMessageService : MonoBehaviour {

    public static void showErrorMsg(string msg, GameObject target, int level)
    {
        string header = "";

        switch (level)
        {
            case 1:
                header = "info \n\n";
                break;
            case 2:
                header = "warning! \n\n";
                break;
            case 3:
                header = "error! \n\n";
                break;
        }

        Transform systemMessageHolderPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.SYSTEM_MESSAGE_PANEL);
        Transform systemMessageHolder = (Transform)GameObject.Instantiate(
            systemMessageHolderPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity
        );

        systemMessageHolder.transform.SetParent(GameObject.Find("Canvas").transform, false);
        systemMessageHolder.transform.Find("SystemMessage").gameObject.GetComponent<UnityEngine.UI.Text>().text = header + msg;

        //Debug.Log("There was an exception, but could not find the error message holder gameobject! EXCEPTION: " + e.Message);
    }
}

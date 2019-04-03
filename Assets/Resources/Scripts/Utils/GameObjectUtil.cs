using UnityEngine;

/*Handy functions to handle gameobjects in the scene...*/
public class GameObjectUtil {

    public static GameObject FindChildByName(string parentName, string name)
    {
        GameObject parent = GameObject.Find(parentName);
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }

        return null;
    }
}

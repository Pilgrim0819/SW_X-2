using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuAction : MonoBehaviour {

    public GameObject camera;
    public GameObject canvasToDeactivate;
    public GameObject canvasToActivate;

    private const float cameraDistanceZ = 1300.0f;
    private const float cameraDistanceY = 800.0f;
    private const float cameraDistanceX = 430.0f;

    public void MENU_ACTION_rotateTo(GameObject target)
    {
        /*Vector3 targetPosition = target.transform.position;
        targetPosition.z -= cameraDistanceZ;
        targetPosition.y += cameraDistanceY;
        targetPosition.x -= cameraDistanceX;
        camera.transform.position = targetPosition;

        canvasToActivate.active = true;
        canvasToDeactivate.active = false;*/

        SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

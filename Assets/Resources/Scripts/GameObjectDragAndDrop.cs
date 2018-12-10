using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GameObjectDragAndDrop : MonoBehaviour {

    private GameObject target;
    private bool grabbed = false;
    private Vector3 prevPos;

    private const float dragSpeedMultiplier = 50.0f;

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }

    void OnMouseDrag()
    {
        if (MatchDatas.getRound() == 0) {
            float moveX = Input.GetAxis("Mouse X");
            float moveY = Input.GetAxis("Mouse Y");
            Vector3 newPos = new Vector3(transform.position.x + (moveX * dragSpeedMultiplier), transform.position.y, transform.position.z + (moveY * dragSpeedMultiplier));

            transform.position = newPos;
        }
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);

            if (target != null)
            {
                Cursor.visible = false;
                grabbed = true;
                prevPos = target.transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.visible = true;
            grabbed = false;

            GameObject shipCollection = GameObject.Find("ShipCollection1");
            GameObject setupField = GameObject.Find("Player1SetupField");

            if (!shipCollection.GetComponent<Collider>().bounds.Contains(target.transform.position) && !setupField.GetComponent<Collider>().bounds.Contains(target.transform.position))
            {
                target.transform.position = prevPos;
            }
        }

        if (grabbed && MatchDatas.getRound() == 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                target.transform.RotateAround(target.transform.position, target.transform.TransformDirection(Vector3.up), 2.5f);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                target.transform.RotateAround(target.transform.position, target.transform.TransformDirection(Vector3.up), -2.5f);
            }
        }
    }
}

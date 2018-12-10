using UnityEngine;
using System.Collections;

public class ManeuverHandler : MonoBehaviour {

    public void moveShip(GameObject ship) {
        StartCoroutine(MoveOverSeconds (ship, new Vector3(0.0f, 10f, 0f), 5f));
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end) {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds) {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds) {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }

}

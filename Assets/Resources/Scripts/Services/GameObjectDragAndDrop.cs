using UnityEngine;

// TODO Rewrite to handle asteroids (and other) as well
/*Handles drag'n drop functionalities for 3D gameobjects (the ships, mainly...)*/
public class GameObjectDragAndDrop : MonoBehaviour {

    private GameObject target;
    private bool grabbed;
    private Vector3 prevPos;

    private const float dragSpeedMultiplier = 50.0f;

    void Start()
    {
        grabbed = false;
    }

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
        if (target == null)
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
        }

        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");

        if (target.GetComponent<ShipProperties>() != null)
        {
            if (MatchDatas.getCurrentPhase() == MatchDatas.phases.SQUADRON_PLACEMENT && shipCanBeMoved())
            {
                Vector3 newPos = new Vector3(transform.position.x + (moveX * dragSpeedMultiplier), transform.position.y, transform.position.z + (moveY * dragSpeedMultiplier));

                transform.position = newPos;

                /*Prototype to highlight setup field boundaries when ship is not inside them. POLISH!!!!*/
                if (!GameObject.Find("Player1SetupField").GetComponent<Collider>().bounds.Contains(target.transform.position))
                {
                    GameObject.Find("Player1SetupField/New Sprite").gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.31f);
                } else
                {
                    GameObject.Find("Player1SetupField/New Sprite").gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.31f);
                }
                /*Prototype to highlight setup field boundaries when ship is not inside them. POLISH!!!!*/
            }
        }

        if (target.GetComponent<AsteroidProperties>() != null)
        {
            if (MatchDatas.getCurrentPhase() == MatchDatas.phases.ASTEROIDS_PLACEMENT && asteroidCanBeMoved())
            {
                Vector3 newPos = new Vector3(transform.position.x + (moveX * dragSpeedMultiplier), transform.position.y, transform.position.z + (moveY * dragSpeedMultiplier));

                transform.position = newPos;
            }
        }
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);

            if (target != null)
            {
                if (!grabbed)
                {
                    if ((MatchDatas.getActiveShip() == null || MatchDatas.getActiveShip() != target) && target.GetComponent<ShipProperties>() != null)
                    {
                        MatchHandlerUtil.hideActiveShipHighlighters();
                        MatchHandlerUtil.hideFiringArcs();

                        LoadedShip activeShip = new LoadedShip();
                        ShipProperties sp = target.GetComponent<ShipProperties>();

                        activeShip = sp.getLoadedShip();

                        MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setActiveShip(activeShip);
                        MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setSelectedShip(activeShip);

                        MatchHandlerUtil.setShipHighlighters(target, true);
                        MatchDatas.setActiveShip(target);
                    }
                }

                if (MatchDatas.getCurrentPhase() == MatchDatas.phases.SQUADRON_PLACEMENT || MatchDatas.getCurrentPhase() == MatchDatas.phases.ASTEROIDS_PLACEMENT)
                {
                    Cursor.visible = false;
                    grabbed = true;
                    prevPos = target.transform.position;
                } else if (MatchDatas.getCurrentPhase() == MatchDatas.phases.ACTIVATION && target.GetComponent<ShipProperties>() != null)
                {
                    foreach (LoadedShip ship in MatchHandler.getAvailableShips())
                    {
                        if (
                        target.transform.GetComponent<ShipProperties>().getLoadedShip().getShip().ShipId.Equals(ship.getShip().ShipId)
                        && target.transform.GetComponent<ShipProperties>().getLoadedShip().getPilotId() == ship.getPilotId()
                        && !target.transform.GetComponent<ShipProperties>().getLoadedShip().isHasBeenActivatedThisRound()
                    )
                        {
                            target.transform.GetComponent<ShipProperties>().getLoadedShip().setHasBeenActivatedThisRound(true);
                            StartCoroutine(GameObject.Find("ScriptHolder").GetComponent<CoroutineHandler>().MoveShipOverTime(target, target.transform.GetComponent<ShipProperties>().getLoadedShip().getPlannedManeuver()));
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && grabbed)
        {
            Cursor.visible = true;
            grabbed = false;

            if (target.GetComponent<ShipProperties>() != null)
            {
                GameObject shipCollection = GameObject.Find("ShipCollection1");
                GameObject setupField = GameObject.Find("Player1SetupField");

                // TODO check if this part can be simplyfied....
                if (!shipCollection.GetComponent<Collider>().bounds.Contains(target.transform.position) && !setupField.GetComponent<Collider>().bounds.Contains(target.transform.position))
                {
                    target.transform.position = prevPos;
                }
                else
                {
                    if (setupField.GetComponent<Collider>().bounds.Contains(target.transform.position) && shipCanBeMoved())
                    {
                        togglePositionConfirmButton(true);
                    }
                }

                if (!setupField.GetComponent<Collider>().bounds.Contains(target.transform.position))
                {
                    togglePositionConfirmButton(false);
                }
                // TODO check if this part can be simplyfied....
            }

            if (target.GetComponent<AsteroidProperties>() != null)
            {
                GameObject playField = GameObject.Find("Playfield");

                // TODO Check distance from playfield borders and other asteroids!!
                if (!playField.GetComponent<Collider>().bounds.Contains(target.transform.position) || isAsteroidTooClose())
                {
                    target.transform.position = prevPos;
                } else
                {
                    if (allAsteroidsAreInsidePlayfield())
                    {
                        togglePositionConfirmButton(true);
                    }
                }
            }
        }

        if (grabbed && MatchDatas.getCurrentPhase() == MatchDatas.phases.SQUADRON_PLACEMENT && shipCanBeMoved())
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

    private bool allAsteroidsAreInsidePlayfield()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int i = 0; i < asteroids.Length; i++)
        {
            if (!GameObject.Find("Playfield").GetComponent<Collider>().bounds.Contains(asteroids[i].transform.position))
            {
                return false;
            }
        }

        return true;
    }

    private bool isAsteroidTooClose()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int i=0; i < asteroids.Length; i++)
        {
            float distance = Vector3.Distance(target.transform.position, asteroids[i].transform.position);

            if (distance != 0 && distance < 2500)
            {
                return true;
            }
        }

        return false;
    }

    private void togglePositionConfirmButton(bool active)
    {
        GameObject button = GameObjectUtil.FindChildByName("Canvas", "LocationConfirmButton");

        if (button != null)
        {
            button.transform.GetComponent<ConfirmPositionEvent>().setTarget(target);
            button.SetActive(active);
        }
    }

    private bool shipCanBeMoved()
    {
        bool result = false;
        bool shipIsAvailable = false;

        if (MatchDatas.getActiveShip() != null)
        {
            foreach (LoadedShip ship in MatchHandler.getAvailableShips())
            {
                if (ship.getPilotId() == MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().getPilotId())
                {
                    shipIsAvailable = true;
                }
            }

            if (shipIsAvailable && isPlayersOwnShip())
            {
                if (
                    MatchDatas.getActiveShip().GetComponent<ShipProperties>().isMovable()
                    && !MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().isHasBeenActivatedThisRound()
                )
                {
                    result = true;
                }
            }
        }

        return result;
    }

    private bool asteroidCanBeMoved()
    {
        return target.GetComponent<AsteroidProperties>().isCanBeMoved();
    }

    // DUPLICATED FRAGMENT!!!!
    private bool isPlayersOwnShip()
    {
        foreach (LoadedShip ship in LocalDataWrapper.getPlayer().getSquadron())
        {
            if (ship.getPilotId() == MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().getPilotId())
            {
                return true;
            }
        }

        return false;
    }
}

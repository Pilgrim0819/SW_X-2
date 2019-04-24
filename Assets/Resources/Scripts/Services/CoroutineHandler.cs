using ShipsXMLCSharp;
using System;
using System.Collections;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    public void rollAttackDiceCallback()
    {
        StartCoroutine(RollAttackDice(MatchHandlerUtil.displayInitiativeChoser));
    }

    public IEnumerator TurnShipAround(GameObject objectToMove)
    {
        float elapsedTime = 0.0f;
        float maneuverDurationInSeconds = 0.5f;
        float rotationDegree = 180.0f;

        Vector3 startingPos = objectToMove.transform.position;
        Vector3 center = startingPos + objectToMove.transform.up * 500.0f;

        while (elapsedTime < maneuverDurationInSeconds)
        {
            objectToMove.transform.RotateAround(center, -objectToMove.transform.right, (rotationDegree / maneuverDurationInSeconds) * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Vector3 angles = objectToMove.transform.rotation.eulerAngles;
        angles.z = rotationDegree;
        objectToMove.transform.rotation = Quaternion.Euler(angles);

        StartCoroutine(MoveAndRollShip(objectToMove, 180.0f, objectToMove.transform.up * 1000.0f));
    }

    public IEnumerator MoveAndRollShip(GameObject objectToMove, float rollDegree, Vector3 distance)
    {
        float elapsedTime = 0.0f;
        float maneuverDurationInSeconds = 0.5f;

        Vector3 startingPos = objectToMove.transform.position;
        Vector3 end = startingPos + distance;
        Vector3 angles = objectToMove.transform.rotation.eulerAngles;
        float endAngleZ = angles.z + rollDegree;

        while (elapsedTime < maneuverDurationInSeconds)
        {
            angles = objectToMove.transform.rotation.eulerAngles;
            angles.z += rollDegree * Time.deltaTime * 2;
            objectToMove.transform.rotation = Quaternion.Euler(angles);
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / maneuverDurationInSeconds));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //Not sure if needed...
        end.y = -500.0f;
        //--
        angles.x = 0.0f;
        angles.z = endAngleZ;

        objectToMove.transform.rotation = Quaternion.Euler(angles);
        objectToMove.transform.position = end;
    }

    public IEnumerator MoveShipOverTime(GameObject objectToMove, Maneuver maneuver)
    {
        /*if (isMoving)
        {
            yield break; ///exit if this is still running
        }*/

        float maneuverDurationInSeconds = 1.0f;
        float elapsedTime = 0.0f;
        int speed = Int32.Parse(maneuver.Speed == null ? "0" : maneuver.Speed);
        Vector3 startingPos = objectToMove.transform.position;

        switch (maneuver.Bearing)
        {
            case "koiogran":
            case "straight":
                Vector3 end = startingPos + objectToMove.transform.forward * (speed + 1) * 500;

                while (elapsedTime < maneuverDurationInSeconds)
                {
                    objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / maneuverDurationInSeconds));
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                objectToMove.transform.position = end;

                if (maneuver.Bearing.Equals("koiogran"))
                {
                    StartCoroutine(this.TurnShipAround(objectToMove));
                }

                break;
            case "bank_left":
            case "bank_right":
            case "turn_left":
            case "turn_right":
                float turnDegree = 90.0f;
                float turnMultiplier = 0.875f;
                float turnStep = 0.7f;

                if (maneuver.Bearing.Contains("bank"))
                {
                    turnDegree = 45.0f;
                    turnMultiplier = 2.0f;
                    turnStep = 1.25f;
                }

                if (maneuver.Bearing.Contains("left"))
                {
                    turnDegree = turnDegree * (-1);
                    turnMultiplier = turnMultiplier * (-1);
                }

                StartCoroutine(this.MoveAndRollShip(objectToMove, -turnDegree, objectToMove.transform.forward * 250.0f));

                float distance = 500 * (turnMultiplier + ((speed - 1) * turnStep));
                Vector3 center = startingPos + objectToMove.transform.right * distance;
                float finalRotation = objectToMove.transform.eulerAngles.y + turnDegree;

                if (finalRotation >= 360.0f)
                {
                    finalRotation = 0.0f;
                }

                while (elapsedTime < maneuverDurationInSeconds)
                {
                    objectToMove.transform.RotateAround(center, Vector3.up, turnDegree * Time.deltaTime);
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                Vector3 angles = objectToMove.transform.rotation.eulerAngles;
                angles.y = finalRotation;
                objectToMove.transform.rotation = Quaternion.Euler(angles);

                StartCoroutine(this.MoveAndRollShip(objectToMove, turnDegree, objectToMove.transform.forward * 250.0f));

                break;
        }
    }

    public IEnumerator RollAttackDice(System.Action<Player> callBack)
    {
        DiceRollerBase.showDiceArea(1, true);
        Rigidbody[] GOS = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
        bool allSleeping = false;

        while (!allSleeping)
        {
            allSleeping = true;

            foreach (Rigidbody GO in GOS)
            {
                if (!GO.IsSleeping())
                {
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }

        }

        DiceRollerBase.getDiceResults(GOS);

        Player result = null;

        if (LocalDataWrapper.getPlayer().getLastDiceResults()[0] == DiceRollerBase.DICE_RESULT_HIT_OR_EVADE || LocalDataWrapper.getPlayer().getLastDiceResults()[0] == DiceRollerBase.DICE_RESULT_CRIT)
        {
            result = MatchDatas.getPlayers()[0];
        }
        else
        {
            result = MatchDatas.getPlayers()[1];
        }

        callBack(result);
    }
}

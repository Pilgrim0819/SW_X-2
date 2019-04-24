using ShipsXMLCSharp;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManeuverSelectionEvent : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Maneuver selected = null;
        string maneuverName = transform.parent.name;
        string[] parts = maneuverName.Split(new[] { '_' }, 2);

        string speed = parts[0].Substring(parts[0].Length - 1);
        string bearing = parts[1];

        if (parts[0].Contains("-"))
        {
            speed = parts[0].Substring(parts[0].Length - 2);
        }

        foreach (Maneuver maneuver in MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().getShip().Maneuvers.Maneuver)
        {
            if (maneuver.Speed.Equals(speed) && maneuver.Bearing.Equals(bearing))
            {
                selected = maneuver;
                break;
            }
        }

        MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().setPlannedManeuver(selected);

        if (MatchHandlerUtil.maneuversPlanned())
        {
            Debug.Log("All maneuvers are planned, ready for next phase...");
        }
    }
}

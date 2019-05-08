using ShipsXMLCSharp;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManeuverSelectionEvent : MonoBehaviour, IPointerClickHandler
{

    public Maneuver maneuver;

    public void setManeuver(Maneuver maneuver)
    {
        this.maneuver = maneuver;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Selected maneuver: " + maneuver.Speed + " - " + maneuver.Bearing);
        MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().setPlannedManeuver(this.maneuver);
        MatchHandler.handleManeuverSelection(this.maneuver);
    }
}

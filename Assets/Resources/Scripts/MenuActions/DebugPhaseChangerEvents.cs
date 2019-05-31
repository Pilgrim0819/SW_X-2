using UnityEngine;
using UnityEngine.EventSystems;

public class DebugPhaseChangerEvents : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PhaseHandlerService.nextPhase();
    }
}

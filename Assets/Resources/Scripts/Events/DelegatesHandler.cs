using UnityEngine;
using System.Collections;

public class DelegatesHandler : MonoBehaviour {

    //Every event method must have delegate's argument list!!
    public delegate void PhaseEventHandler();

    //Define events that can be triggered and other objects can subscribe for.
    //IN THIS ORDER!!!
    public static event PhaseEventHandler onBeforeSetup;
    public static event PhaseEventHandler onBeforeAttack;   //Before rolling attack dice (decreasing enemy agility)
    public static event PhaseEventHandler onAttack;         //After rolling attack dice (using focus token, etc...)
    public static event PhaseEventHandler onBeforeDefense;  //Before rolling defense dice (adding 1 more dye to roll)
    public static event PhaseEventHandler onAfterDefense;   //After rolling defense dice (using an evade token)
    public static event PhaseEventHandler onAfterAttack;    //After resolving every effects (attacking again/getting a free evade token/etc...)
    public static event PhaseEventHandler onNormalHit;
    public static event PhaseEventHandler onCriticalHit;

    public static event PhaseEventHandler onBeforeManeuverSelection;
    public static event PhaseEventHandler onAfterManeuverSelection;

    //Define the methods with which the events can be triggered. MUST follow delegate's signature!!
    public static void EventBeforeManeuverSelection()
    {
        if (onBeforeManeuverSelection != null)
        {
            onBeforeManeuverSelection();
        }
    }

    public static void EventAfterManeuverSelection()
    {
        if (onAfterManeuverSelection != null)
        {
            onAfterManeuverSelection();
        }
    }

}

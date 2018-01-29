using UnityEngine;
using System.Collections;

public class PlayerDatas {
    private static string chosenSide;

    public static void setChosenSide(string side)
    {
        chosenSide = side;
    }

    public static string getChosenSide()
    {
        return chosenSide;
    }

}

using UnityEngine;
using System.Collections;

public class PlayerDatas {
    private static string chosenSide;
    private static string chosenShip;

    public static void setChosenSide(string side)
    {
        chosenSide = side;
    }

    public static string getChosenSide()
    {
        return chosenSide;
    }

    public static void setChosenShip(string ship)
    {
        chosenShip = ship;
    }

    public static string getChosenShip()
    {
        return chosenShip;
    }

}

using EDBG.Rules;
using System;

public static class UtilityFunctions
{
    private static Random _random;

    static UtilityFunctions()
    {
        _random = new Random();
    }

    public static Colors GetRandomComponentColor()
    {
        Colors color=Colors.White;
        switch (UnityEngine.Random.Range(0,4))
        {
            case 0:
                color= Colors.White;
                break;
            case 1:
                color= Colors.Blue;
                break;
            case 2:
                color= Colors.Green;
                break;
            case 3:
                color= Colors.Red;
                break;
            default:
               break;
        }
        return color;
    }


}

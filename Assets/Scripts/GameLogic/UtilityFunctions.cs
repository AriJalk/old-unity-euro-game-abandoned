using EDBG.Rules;
using System;

public static class UtilityFunctions
{
    private static Random _random;

    static UtilityFunctions()
    {
        _random = new Random();
    }

    public static ComponentColor GetRandomComponentColor()
    {
        ComponentColor color=ComponentColor.White;
        switch (_random.Next(0,4))
        {
            case 0:
                color= ComponentColor.White;
                break;
            case 1:
                color= ComponentColor.Blue;
                break;
            case 2:
                color= ComponentColor.Green;
                break;
            case 3:
                color= ComponentColor.Red;
                break;
            default:
               break;
        }
        return color;
    }


}

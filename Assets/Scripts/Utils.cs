using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils
{

    public static bool hasFlag(int p_Flags, int p_Flag)
    {
        return (p_Flags & p_Flag) != 0;
    }
}

using UnityEngine;
using UnityEditor;

public class Enums : ScriptableObject
{
    public enum CollectibleType : int
    {
        HEAL        = 0x01,
        COLLECTIBLE = 0x02
    }

    public enum TilesType : int
    {
        NORMAL = 0x01,
        SLOW   = 0x02
    }

    public enum MovementDirection : int
    {
        NONE    = 0x00,
        RIGHT   = 0x01,
        LEFT    = 0x02,
        BOTTOM  = 0x04,
        TOP     = 0x08
    }

    /// <summary>
    /// T = Top, R = Right, B = Bottom, L = Left
    /// yeah, it's clockwise
    /// </summary>
    public enum DoorsPosition : int
    {
        NONE = 0x00,
        T    = 0x01,
        R    = 0x02,
        B    = 0x04,
        L    = 0x08,
        TR   = 0x03,
        RB   = 0x06,
        BL   = 0x0C,
        LT   = 0x09,
        RL   = 0x0A,
        TB   = 0x05,
        TRB  = 0x07,
        RBL  = 0x0E,
        BLT  = 0x0D,
        LTR  = 0x0B,
        TRBL = 0x0F
    }
}
using UnityEngine;
using UnityEditor;

public class Enums : ScriptableObject
{
    public enum CollectibleType : int
    {
        HEAL = 0x01
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
}
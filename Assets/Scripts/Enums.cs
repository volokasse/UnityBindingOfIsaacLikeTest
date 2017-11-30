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
}
using UnityEngine;
using UnityEditor;

public class UnitInfos : MonoBehaviour
{
    /// Base Stats
    public int coeffFreinage = 4;
    public float baseVitesse = 3;

    /// Unit actual stats
    public int health           = 3;
    public int damage           = 1;
    public float vitesse        = 0f;

    ///  Modifiers
    public float vitesseChanger = 0f;


    private void Update()
    {
        if (vitesseChanger > 0)
            vitesse = baseVitesse - (baseVitesse * (vitesseChanger / 100));
        else
            vitesse = baseVitesse;
    }

    void Awake()
    {

    }

    public void ChangeSPeed(int SlowPercent)
    {
        vitesseChanger += (float)SlowPercent;
    }
}
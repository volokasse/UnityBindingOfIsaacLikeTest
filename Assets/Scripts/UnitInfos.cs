using UnityEngine;
using UnityEditor;

public class UnitInfos : MonoBehaviour
{
    /// Base Stats
    public int coeffFreinage           = 4;
    public float baseVitesse           = 3f;
    public float baseProjectileVitesse = 4f;
    public float baseTimerProjectile   = 1f;

    /// Unit actual stats
    public int health              = 3;
    public int damage              = 1;
    public float vitesse           = 0f;
    public float projectileVitesse = 0f;
    public float timerProjectile   = 0f;

    ///  Modifiers
    public float vitesseModifier = 0f;
    public float projectileVitesseModifier = 0f;
    public float timerProjectileModifier = 0f;


    private void Update()
    {
        if (vitesseModifier > 0 && vitesseModifier < 100)
            vitesse = baseVitesse - (baseVitesse * (vitesseModifier / 100));
        else if (vitesseModifier > 100)
            vitesse = baseVitesse + (baseVitesse * (vitesseModifier / 100));
        else
            vitesse = baseVitesse;

        if (projectileVitesseModifier > 0 && projectileVitesseModifier < 100)
            projectileVitesse = baseProjectileVitesse - (baseProjectileVitesse * (projectileVitesseModifier / 100));
        else if (projectileVitesseModifier > 100)
            projectileVitesse = baseProjectileVitesse + (baseProjectileVitesse * (projectileVitesseModifier / 100));
        else
            projectileVitesse = baseProjectileVitesse;

        if (timerProjectileModifier > 0 && timerProjectileModifier < 100)
            timerProjectile = baseTimerProjectile - (baseTimerProjectile * (timerProjectileModifier / 100));
        else if (timerProjectileModifier > 100)
            timerProjectile = baseTimerProjectile + (baseTimerProjectile * (timerProjectileModifier / 100));
        else
            timerProjectile = baseTimerProjectile;
    }

    public void ChangeSpeed(int percentModifier)
    {
        vitesseModifier += (float)percentModifier;
    }

    public void ChangeProjectileSpeed(int percentModifier)
    {
        projectileVitesseModifier += (float)percentModifier;
    }
}
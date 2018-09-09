using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UnitInfos : MonoBehaviour
{
    /// Base Stats
    public int coeffFreinage           = 4;
    public float baseVitesse           = 3f;
    public float baseProjectileVitesse = 4f;
    public float baseTimerProjectile   = .5f;
    public float baseProjectileRange   = 1f;

    /// Unit actual stats
    public int health               = 3;
    public int damage               = 1;
    private float vitesse           = 0f;
    private float projectileVitesse = 0f;
    private float timerProjectile   = 0f;
    private float projectileRange   = 0f;

    ///  Modifiers
    public float vitesseModifier           = 0f;
    public float projectileVitesseModifier = 0f;
    public float timerProjectileModifier   = 0f;
    public float projectileRangeModifier   = 0f;

    public Dictionary<uint, Bonus> collectibles = new Dictionary<uint, Bonus>();

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

        if (projectileRangeModifier > 0 && projectileRangeModifier < 100)
            projectileRange = baseProjectileRange - (baseProjectileRange * (projectileRangeModifier / 100));
        else if (projectileRangeModifier > 100)
            projectileRange = baseProjectileRange + (baseProjectileRange * (projectileRangeModifier / 100));
        else
            projectileRange = baseProjectileRange;
    }

    public void ChangeSpeed(int percentModifier)
    {
        vitesseModifier += (float)percentModifier;
    }

    public void ChangeProjectileSpeed(int percentModifier)
    {
        projectileVitesseModifier += (float)percentModifier;
    }

    public float getVitesse()
    {
        return vitesse;
    }

    public float getProjectileVitesse()
    {
        return projectileVitesse;
    }

    public float getTimerProjectile()
    {
        return timerProjectile;
    }

    public float getProjectileRange()
    {
        return projectileRange;
    }
}
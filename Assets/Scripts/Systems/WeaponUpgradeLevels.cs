﻿using System;
using UnityEngine;

public class WeaponUpgradeLevels
{
    public int FireRateLevel = 0;
    public int SpeedLevel = 0;
    public int ProjectileMultiplierLevel = 0;
    public int DamageLevel = 0;
    public int ProjectileSizeLevel = 0;
    public int AutoAimLevel = 0;
    public int FragmentationLevel = 0;
    public int PenetrationLevel = 0;
    public int SpreadLevel = 0;
    public int ExplosionLevel = 0;
    public int PuddleLevel = 0;
    public int IceLevel = 0;
    public int FireLevel = 0;


    public int LightningLevel = 0;


    public int VampireLevel = 0;

    public float GetShootFrequencyFromLevel()
    {
        return 1f / ((float)FireRateLevel + 1) * 2f;
    }

    public float GetProjectileSpeedFromLevel()
    {
        return 10f + SpeedLevel * 3f;
    }

    public float GetProjectileDamageFromLevel()
    {
        return 40 + DamageLevel * 10;
    }

    public float GetProjectileSizeFromLevel()
    {
        return 1f + (ProjectileSizeLevel + 1) * 0.2f;
    }

    public float GetThrowbackForceFromLevel()
    {
        return (GetProjectileSpeedFromLevel() * GetProjectileSizeFromLevel()) / 3f;
    }

    internal float GetProjectileSpreadFromLevel(int level)
    {
        return UnityEngine.Random.Range(-90f, 90f) * (level / 30f);
    }

    internal float GetProjectileSpreadFromLevel()
    {
        return UnityEngine.Random.Range(-90f, 90f) * (SpreadLevel / 30f);
    }


    internal int GetProjectileMultiplierFromLevel()
    {
        return ProjectileMultiplierLevel + 1;
    }

    internal int GetProjectileFragmentationFromLevel()
    {
        return FragmentationLevel * 3;
    }

    internal int GetProjectileAutoAimLevel()
    {
        return AutoAimLevel;
    }

    internal float GetProjectileAutoAimViewAngleFromLevel()
    {
        return Mathf.LerpUnclamped(0, 90f, AutoAimLevel / 3f);
    }

    internal float GetProjectileAutoAimRotateSpeedFromLevel()
    {
        return Mathf.LerpUnclamped(0, 180f, AutoAimLevel / 3f);
    }

    internal float GetFireDamagePerSecFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 100f, FireLevel / 4f);
    }

    internal float GetFireTimerFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 8f, FireLevel / 4f);
    }

    internal float GetFireCatchRadiusFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 2f, FireLevel / 4f);
    }

    internal float GetProjectileExplosionRangeFromLevel()
    {
        return ExplosionLevel * 0.5f;
    }

    internal float GetProjectileExplosionFxScaleFromLevel()
    {
        return ExplosionLevel;
    }

    internal float GetProjectileExplosionDamageFromLevel()
    {
        return (ExplosionLevel + DamageLevel) * 20f;
    }

    internal float GetPuddleEfficiencyFromLevel()
    {
        return Mathf.LerpUnclamped(1f, 0.25f, PuddleLevel / 3f);
    }

    internal float GetPuddleLifeTimeFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 10f, PuddleLevel / 3f);
    }

    internal float GetPuddleRadiusFromLevel()
    {
        return Mathf.LerpUnclamped(0.5f, 1.5f, ProjectileSizeLevel / 3f);    
    }

    internal int GetLevelsSum()
    {
        return FireRateLevel
            + SpeedLevel
            + ProjectileMultiplierLevel
            + DamageLevel
            + ProjectileSizeLevel
            + AutoAimLevel
            + FragmentationLevel
            + PenetrationLevel
            + SpreadLevel
            + ExplosionLevel
            + IceLevel
            + FireLevel;
    }

}


using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponUpgradeLevels
{
    public DamageLevel DamageLevel = new DamageLevel();
    public FireRateLevel FireRateLevel = new FireRateLevel();
    public ProjectileSpeedLevel SpeedLevel = new ProjectileSpeedLevel();
    public ProjectileMultiplierLevel ProjectileMultiplierLevel = new ProjectileMultiplierLevel();
    public ProjectileSizeLevel ProjectileSizeLevel = new ProjectileSizeLevel();
    public ProjectileAutoAimLevel AutoAimLevel = new ProjectileAutoAimLevel();
    public ProjectileFragmentationLevel FragmentationLevel = new ProjectileFragmentationLevel();
    public ProjectilePenetrationLevel PenetrationLevel = new ProjectilePenetrationLevel();
    public ProjectileSpreadLevel SpreadLevel = new ProjectileSpreadLevel();
    public ProjectileExplosionLevel ExplosionLevel = new ProjectileExplosionLevel();
    public ProjectilePuddleLevel PuddleLevel = new ProjectilePuddleLevel();
    public ProjectileIceLevel IceLevel = new ProjectileIceLevel();
    public ProjectileFireLevel FireLevel = new ProjectileFireLevel();
    public ProjectileLightningLevel LightningLevel = new ProjectileLightningLevel();
    public ProjectileVampirismLevel VampirismLevel = new ProjectileVampirismLevel();

    public List<WeaponLevel> WeaponLevels = new List<WeaponLevel>();

    public WeaponUpgradeLevels()
    {
        WeaponLevels.Add(DamageLevel);
        WeaponLevels.Add(FireRateLevel);
        WeaponLevels.Add(SpeedLevel);
        WeaponLevels.Add(ProjectileMultiplierLevel);
        WeaponLevels.Add(ProjectileSizeLevel);
        WeaponLevels.Add(AutoAimLevel);
        WeaponLevels.Add(FragmentationLevel);
        WeaponLevels.Add(PenetrationLevel);
        WeaponLevels.Add(SpreadLevel);
        WeaponLevels.Add(ExplosionLevel);
        WeaponLevels.Add(PuddleLevel);
        WeaponLevels.Add(IceLevel);
        WeaponLevels.Add(FireLevel);
        WeaponLevels.Add(LightningLevel);
        WeaponLevels.Add(VampirismLevel);
    }
    public float GetShootFrequencyFromLevel()
    {
        return 1f / (FireRateLevel + 1) * 2f;
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

    internal float GetProjectileSpreadFromLevel()
    {
        return UnityEngine.Random.Range(-90f, 90f) * (SpreadLevel / 30f);
    }
    internal int GetProjectileMultiplierFromLevel()
    {
        return ProjectileMultiplierLevel + 1;
    }

    internal float GetLightningHitDamageFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 50f, LightningLevel / 4f);
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
        return Mathf.Lerp(1f, 0.25f, PuddleLevel / 5f);
    }

    internal float GetPuddleLifeTimeFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 3, PuddleLevel / 3f);
    }

    internal float GetPuddleRadiusFromLevel()
    {
        return Mathf.LerpUnclamped(0.5f, 1.5f, ProjectileSizeLevel / 3f);
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

    internal float GetIceTimerFromLevel()
    {
        return Mathf.LerpUnclamped(0f, 8f, IceLevel / 4f);
    }

    internal float GetIceDamageMultiplierFromLevel()
    {
        return Mathf.LerpUnclamped(1f, 2f, IceLevel / 4f);
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


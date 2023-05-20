using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponUpgradeLevels
{

    private const int DAMAGE_LEVEL_INDEX = 0;
    private const int FIRERATE_LEVEL_INDEX = 1;
    private const int SPEED_LEVEL_INDEX = 2;
    private const int MULTIPLIER_LEVEL_INDEX = 3;
    private const int SIZE_LEVEL_INDEX = 4;
    private const int AIM_LEVEL_INDEX = 5;
    private const int FRAGMENTATION_LEVEL_INDEX = 6;
    private const int PENETRATION_LEVEL_INDEX = 7;
    private const int SPREAD_LEVEL_INDEX = 8;
    private const int EXPLOSION_LEVEL_INDEX = 9;
    private const int PUDDLE_LEVEL_INDEX = 10;
    private const int ICE_LEVEL_INDEX = 11;
    private const int FIRE_LEVEL_INDEX = 12;
    private const int LIGHTNING_LEVEL_INDEX = 13;
    private const int VAMPIRISM_LEVEL_INDEX = 14;

    public WeaponLevel DamageLevel => WeaponLevels[DAMAGE_LEVEL_INDEX];
    public WeaponLevel FireRateLevel => WeaponLevels[FIRERATE_LEVEL_INDEX];
    public WeaponLevel SpeedLevel => WeaponLevels[SPEED_LEVEL_INDEX];
    public WeaponLevel ProjectileMultiplierLevel => WeaponLevels[MULTIPLIER_LEVEL_INDEX];
    public WeaponLevel ProjectileSizeLevel => WeaponLevels[SIZE_LEVEL_INDEX];
    public WeaponLevel AutoAimLevel => WeaponLevels[AIM_LEVEL_INDEX];
    public WeaponLevel FragmentationLevel => WeaponLevels[FRAGMENTATION_LEVEL_INDEX];
    public WeaponLevel PenetrationLevel => WeaponLevels[PENETRATION_LEVEL_INDEX];
    public WeaponLevel SpreadLevel => WeaponLevels[SPREAD_LEVEL_INDEX];
    public WeaponLevel ExplosionLevel => WeaponLevels[EXPLOSION_LEVEL_INDEX];
    public WeaponLevel PuddleLevel => WeaponLevels[PUDDLE_LEVEL_INDEX];
    public WeaponLevel IceLevel => WeaponLevels[ICE_LEVEL_INDEX];
    public WeaponLevel FireLevel => WeaponLevels[FIRE_LEVEL_INDEX];
    public WeaponLevel LightningLevel => WeaponLevels[LIGHTNING_LEVEL_INDEX];
    public WeaponLevel VampirismLevel => WeaponLevels[VAMPIRISM_LEVEL_INDEX];

    public List<WeaponLevel> WeaponLevels = new List<WeaponLevel>();

    public WeaponUpgradeLevels(WeaponsLevelsSettings weaponsSettings)
    {
        for (int i = 0; i < weaponsSettings.AllSettings.Length; i++)
        {
            WeaponLevels.Add(new WeaponLevel(weaponsSettings.AllSettings[i]));
        }
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


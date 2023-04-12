using System;

public class WeaponUpgradeLevels
{
    public int BurstLevel = 0;
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
    public int IceLevel = 0;
    public int FireLevel = 0;
    public int LightningLevel = 0;
    public int VampireLevel = 0;

    public float GetShootFrequencyFromLevel()
    {
        return 1f/ ((float) FireRateLevel + 1)*2f;
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
        return 1f + (ProjectileSizeLevel+1) * 0.2f;
    }

    public float GetThrowbackForceFromLevel()
    {
        return (GetProjectileSpeedFromLevel() * GetProjectileSizeFromLevel())/3f;
    }

    internal float GetProjectileSpreadFromLevel()
    {
        return UnityEngine.Random.Range(-90f, 90f) * (SpreadLevel / 30f);
    }
}

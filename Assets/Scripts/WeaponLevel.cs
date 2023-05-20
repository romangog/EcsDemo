using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class WeaponLevel
{
    private Settings _settings;
    public WeaponLevel(Settings settings)
    {
        _settings = settings;
    }
    public int Level;

    public abstract string Name { get; }

    public abstract string GetDescription();

    public Sprite Sprite;

    public static implicit operator int(WeaponLevel l) => l.Level;
    public static explicit operator float(WeaponLevel l) => l.Level;
    public static WeaponLevel operator ++(WeaponLevel l)
    {
        l.Level++;
        return l;
    }
    internal void Updgrade()
    {
        Level++;
    }
    public override string ToString()
    {
        return Level.ToString();
    }

    [Serializable]
    public class Settings
    {
        public Sprite IconSprite;
        public string Description;
        public string Name;
    }
}

public class DamageLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class FireRateLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }


}

public class ProjectileSpeedLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}

public class ProjectileMultiplierLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}


public class ProjectileSizeLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileAutoAimLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileFragmentationLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectilePenetrationLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileSpreadLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileExplosionLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectilePuddleLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileIceLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileFireLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileLightningLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}
public class ProjectileVampirismLevel : WeaponLevel
{
    public override string Name => "Damage";

    public override string GetDescription()
    {
        return "+Damage";
    }
}


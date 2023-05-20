using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponLevel
{
    private Settings _settings;
    public WeaponLevel(Settings settings)
    {
        _settings = settings;
    }
    public int Level;

    public string Name => _settings.Name;

    public string Description => _settings.Description;

    public Sprite Sprite => _settings.IconSprite;

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
using UnityEngine;
using System;

public enum WeaponTypes { Machinegun, Shotgun, Flamethrower, Plasmagun };

[Serializable]
public struct WeaponAmmo
{
    public WeaponTypes type;
    public int ammo;
}

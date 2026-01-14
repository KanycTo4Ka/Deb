using UnityEngine;
using System;

public enum WeaponTypes { Sword, Pistol, Machinegun, Shotgun, RocketLauncher};

[Serializable]
public struct WeaponAmmo
{
    public WeaponTypes type;
    public int ammo;
}

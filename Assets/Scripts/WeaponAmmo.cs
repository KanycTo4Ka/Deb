using UnityEngine;
using System;

public enum WeaponTypes {Pistol, Machinegun, Shotgun};

[Serializable]
public struct WeaponAmmo
{
    public WeaponTypes type;
    public int ammo;
}

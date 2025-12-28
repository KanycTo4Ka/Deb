using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ammunition : MonoBehaviour
{
    [SerializeField] public List<WeaponAmmo> ammoList;
    public Dictionary<WeaponTypes, int> ammoDictionary;

    [SerializeField] WeaponSelector weaponSelector;

    public UnityEvent onAmmoChange;

    public void listToDictionary()
    {
        ammoDictionary = new Dictionary<WeaponTypes, int>();

        foreach (var ammo in ammoList)
            if (ammoDictionary.ContainsKey(ammo.type) == false)
                ammoDictionary.Add(ammo.type, ammo.ammo);
    }

    void Awake()
    {
        listToDictionary();
        onAmmoChange?.Invoke();
    }

    public bool checkAmmo(WeaponTypes type)
    {
        if (ammoDictionary.ContainsKey(type) == false)
            return false;
        if (ammoDictionary[type] < 1)
            return false;

        return true;
    }

    public bool getAmmo(WeaponTypes type)
    {
        if (ammoDictionary.ContainsKey(type) == false)
            return false;
        if (ammoDictionary[type] < 1)
            return false;

        ammoDictionary[type]--;
        onAmmoChange?.Invoke();

        return true;
    }

    public bool addAmmo(WeaponTypes type, int amount)
    {
        if (ammoDictionary.ContainsKey(type) == false)
            return false;

        ammoDictionary[type] += amount;
        onAmmoChange?.Invoke();

        return true;
    }

    public int getAmmoCount(WeaponTypes type)
    {
        if (!ammoDictionary.ContainsKey(type)) 
            return 0;
        return ammoDictionary[type];
    }

    public bool setAmmo(WeaponTypes type, int amount)
    {
        if (ammoDictionary.ContainsKey(type) == false)
            return false;

        ammoDictionary[type] = amount;
        onAmmoChange?.Invoke();

        return true;
    }
}

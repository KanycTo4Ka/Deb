using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ammunition : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)] public int ammoCount = 50;

    [SerializeField] public List<WeaponAmmo> ammoList;
    public Dictionary<WeaponTypes, int> ammoDictionary;

    public UnityEvent onAmmoChange;

    public void listToDictionary()
    {
        ammoDictionary = new Dictionary<WeaponTypes, int>();

        foreach (var ammo in ammoList)
            if (ammoDictionary.ContainsKey(ammo.type) == false)
                ammoDictionary.Add(ammo.type, ammo.ammo);
    }

    void Start()
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

    public bool addAmmo(int amount)
    {
        ammoCount += amount;
        onAmmoChange?.Invoke();

        return true;
    }
}

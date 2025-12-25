using System.IO;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public Transform weaponHolder;
    public WeaponScript weaponScript;

    int selectedWeaponIndex = 0;

    void Start()
    {
        weaponScript.setWeapon(selectWeaponByIndex(0));
    }

    public CWeapon selectNextWeapon()
    {
        foreach(Transform child in weaponHolder)
            child.gameObject.SetActive(false);

        selectedWeaponIndex++;

        if (selectedWeaponIndex > weaponHolder.childCount - 1)
            selectedWeaponIndex = 0;

        weaponHolder.GetChild(selectedWeaponIndex).gameObject.SetActive(true);
        return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
    }

    public CWeapon selectPrevWeapon()
    {
        foreach (Transform child in weaponHolder)
            child.gameObject.SetActive(false);

        selectedWeaponIndex--;

        if (selectedWeaponIndex < 0)
            selectedWeaponIndex = weaponHolder.childCount - 1;

        weaponHolder.GetChild(selectedWeaponIndex).gameObject.SetActive(true);
        return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
    }

    public CWeapon selectWeaponByIndex(int ind)
    {
        foreach (Transform child in weaponHolder)
            child.gameObject.SetActive(false);

        if (ind > -1 && ind <= weaponHolder.childCount)
        {
            selectedWeaponIndex = ind;
            weaponHolder.GetChild(selectedWeaponIndex).gameObject.SetActive(true);
        }
        return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
    }

    public CWeapon getSelectedWeapon()
    {
        return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
    }   
}

using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSelector : MonoBehaviour
{
    public Transform weaponHolder;
    public WeaponScript weaponScript;

    int selectedWeaponIndex = 0;
    int secondWeaponIndex = 0;
    bool swordIsActive = false;

    public UnityEvent weaponChange;

    public CWeapon changeWeapon()
    {
        if (secondWeaponIndex != 0 && swordIsActive)
        {
            foreach (Transform child in weaponHolder)
                child.gameObject.SetActive(false);

            if (selectedWeaponIndex == 0)
            {
                weaponChange.Invoke();
                selectedWeaponIndex = secondWeaponIndex;
            }
            else
                selectedWeaponIndex = 0;

            

            weaponHolder.GetChild(selectedWeaponIndex).gameObject.SetActive(true);
            return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
        }
        return null;
    }

    public CWeapon selectWeaponByIndex(int ind)
    {
        if (swordIsActive)
        {
            foreach (Transform child in weaponHolder)
                child.gameObject.SetActive(false);

            if (ind == 0)
            {
                selectedWeaponIndex = 0;
                weaponHolder.GetChild(selectedWeaponIndex).gameObject.SetActive(true);
                weaponChange.Invoke();
            }
            if (ind == 1 && secondWeaponIndex != 0)
            {
                selectedWeaponIndex = secondWeaponIndex;
                weaponHolder.GetChild(selectedWeaponIndex).gameObject.SetActive(true);
                weaponChange.Invoke();
            }
            return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
        }
        return null;
    }

    public void setActiveSword()
    {
        swordIsActive = true;
        selectWeaponByIndex(0);
    }

    public CWeapon getSelectedWeapon()
    {
        if (!swordIsActive)
            return null;
        return weaponHolder.GetChild(selectedWeaponIndex).gameObject.GetComponent<CWeapon>();
    }

    public void changeSecondWeapon(int weaponIndex)
    {
        secondWeaponIndex = weaponIndex;
    }
}

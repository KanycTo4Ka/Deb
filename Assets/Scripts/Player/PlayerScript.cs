using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public WeaponSelector weaponSelector;
    public WeaponScript weaponScript;
    public InteractorScript interactorScript;

    public int numberOfWeapons = 2;

    private void OnScrollWheel(InputValue value)
    {
        weaponScript.setWeapon(weaponSelector.changeWeapon());
    }

    private void OnAttack(InputValue value)
    {
        if (weaponSelector.getSelectedWeapon() != null)
        {
            if (value.isPressed)
                weaponScript.fireStart();
            else
                weaponScript.fireEnd();
        }
    }

    private void OnNext(InputValue value)
    {
        weaponScript.setWeapon(weaponSelector.selectWeaponByIndex(1));
    }

    private void OnPrevious(InputValue value)
    {
        weaponScript.setWeapon(weaponSelector.selectWeaponByIndex(0));
    }

    private void OnInteract(InputValue value)
    {
        if (interactorScript.interactable != null)
        {
            if ((interactorScript.interactable as UnityEngine.Object) == false)
                interactorScript.interactable = null;
            else
                interactorScript.interactable.interact(GetComponent<PlayerScript>());
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector]
    public float soulCount;
    float defaultModifier = 1;
    float soulModifier;

    public WeaponSelector weaponSelector;
    public WeaponScript weaponScript;
    public InteractorScript interactorScript;

    public int numberOfWeapons = 2;

    public UnityEvent soulCountChange;

    public InputAction attackAction;

    private void Start()
    {
        soulModifier = defaultModifier;
        attackAction.started += ctx => weaponScript.fireStart();
        attackAction.canceled += ctx => weaponScript.fireEnd();
    }

    private void OnEnable()
    {
        attackAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
    }

    private void OnScrollWheel(InputValue value)
    {
        weaponScript.setWeapon(weaponSelector.changeWeapon());
    }

    private void OnAttack()
    {
        if (weaponSelector.getSelectedWeapon() != null)
            weaponScript.fireStart();
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

    public void addSoul()
    {
        soulCount += soulModifier;
        soulCountChange.Invoke();
    }

    public bool removeSoul(int amount)
    {
        soulCount -= amount;
        if (soulCount < 0)
            return false;
        soulCountChange.Invoke();
        return true;
    }

    public void modifySoulModifier(float amount)
    {
        soulModifier *= amount;
    }

    public void setDefault()
    {
        soulModifier = defaultModifier;
    }

    public float getSoul()
    {
        return soulCount;
    }
}

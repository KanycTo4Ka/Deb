using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public WeaponSelector weaponSelector;
    public WeaponScript weaponScript;

    public int numberOfWeapons = 2;

    void Update()
    {
        float mouseWheelDelta = Input.GetAxisRaw("Mouse ScrollWheel");
        if (mouseWheelDelta > 0) weaponScript.setWeapon(weaponSelector.changeWeapon());
        if (mouseWheelDelta < 0) weaponScript.setWeapon(weaponSelector.changeWeapon());
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            int k = (int)(e.keyCode - KeyCode.Alpha0);
            if (!(k > 0 && k <= numberOfWeapons))
            {
                k = (int)(e.keyCode - KeyCode.Keypad0);

                if (!(k > 0 && k <= numberOfWeapons))
                    return;
            }
            weaponScript.setWeapon(weaponSelector.selectWeaponByIndex(k - 1));
        }

        if (Input.GetMouseButtonDown(0))
            weaponScript.fireStart();
        if (Input.GetMouseButtonUp(0))
            weaponScript.fireEnd();
    }
}

using NUnit.Framework;
using TMPro;
using UnityEngine;

public class WeaponsSlotMachine : AbstractSlotMachine
{
    [SerializeField] WeaponSelector weaponSelector;

    [SerializeField] TMP_Text ammoCountText;
    [SerializeField] Ammunition ammunition;
    int ammoCount;

    public override void interact(PlayerScript player)
    {
        if (player.getSoul() >= 1)
        {
            player.removeSoul(1);
            animator.SetTrigger("pulled");
            startSpin();
            StartCoroutine(StopAfterDelay(1f));
        }
    }

    public override void stopEvent()
    {
        switch (results[0])
        {
            case 0:
                weaponSelector.changeSecondWeapon(1);
                ammoCount = Random.Range(0, 201);
                break;
            case 1:
                weaponSelector.changeSecondWeapon(4);
                ammoCount = Random.Range(0, 51);
                break;
            case 2:
                weaponSelector.changeSecondWeapon(3);
                ammoCount = Random.Range(10, 101);
                break;
            case 3:
                weaponSelector.changeSecondWeapon(2);
                ammoCount = Random.Range(10, 301);
                break;
            case 4:
                weaponSelector.changeSecondWeapon(1);
                ammoCount = Random.Range(0, 201);
                break;
            case 5:
                weaponSelector.changeSecondWeapon(4);
                ammoCount = Random.Range(0, 50);
                break;
            case 6:
                weaponSelector.changeSecondWeapon(3);
                ammoCount = Random.Range(10, 101);
                break;
            case 7:
                weaponSelector.changeSecondWeapon(2);
                ammoCount = Random.Range(10, 301);
                break;

        }
        weaponSelector.selectWeaponByIndex(0);
    }
}

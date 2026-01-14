using UnityEngine;

[RequireComponent(typeof(SwordLogic))]
public class CSword : CWeapon
{
    SwordLogic swordLogic;

    protected override void Start()
    {
        base.Start();
        swordLogic = GetComponent<SwordLogic>();
        weaponEffect = null; 
    }

    public override void fire(Ammunition ammunition)
    {
        if (!canFire) return;

        canFire = false;
        StartCoroutine(coolDown());

        swordLogic.swing(curDamage);

        if (weaponEffect != null)
            weaponEffect.Play();
    }

    public override WeaponTypes getWeaponType()
    {
        return WeaponTypes.Sword;
    }
}

using System.Collections;
using UnityEngine;

public abstract class CWeapon : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public float fireRate;
    public float defaultDamage;
    [HideInInspector]
    public float curDamage;
    [HideInInspector]
    public bool canFire = true;
    public ParticleSystem weaponEffect;

    private void Start()
    {
        curDamage = defaultDamage;
        if (weaponEffect == null) return;

        var main = weaponEffect.main;
        main.duration = 1 / fireRate;
    }

    public virtual void fire(Ammunition ammunition)
    {
        if (ammunition.getAmmo(getWeaponType()) == false) return;
        canFire = false;
        StartCoroutine(coolDown());
    }

    public abstract WeaponTypes getWeaponType();

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(1/fireRate);
        canFire = true;
    }

    private void OnEnable()
    {
        if (canFire == false)
            StartCoroutine(coolDown());
    }

    public void setToDefault()
    {
        curDamage = defaultDamage;
    }

    public void modifyDamage(float amount)
    {
        curDamage = amount * defaultDamage;
    }
}

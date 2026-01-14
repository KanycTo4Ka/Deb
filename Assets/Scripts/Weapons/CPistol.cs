using UnityEngine;

[RequireComponent(typeof(TracerSystem))]
[RequireComponent(typeof(PistolLogic))]
public class CPistol : CWeapon
{
    TracerSystem tracerSystem;
    PistolLogic pistolLogic;

    protected override void Start()
    {
        base.Start();
        tracerSystem = GetComponent<TracerSystem>();
        pistolLogic = GetComponent<PistolLogic>();
    }

    public override void fire(Ammunition ammunition)
    {
        base.fire(ammunition);

        tracerSystem.createTracer(firePoint.position, firePoint.forward);
        pistolLogic.shot(firePoint, curDamage);
    }

    public override WeaponTypes getWeaponType()
    {
        return WeaponTypes.Pistol;
    }
}

using UnityEngine;

[RequireComponent(typeof(TracerSystem))]
[RequireComponent(typeof(MachineGunLogic))]
public class CMachineGun : CWeapon
{
    TracerSystem tracerSystem;
    MachineGunLogic machinegunLogic;

    void Start()
    {
        tracerSystem = GetComponent<TracerSystem>();
        machinegunLogic = GetComponent<MachineGunLogic>();
    }

    public override void fire(Ammunition ammunition)
    {
        base.fire(ammunition);

        tracerSystem.createTracer(firePoint.position, firePoint.forward);
        machinegunLogic.shot(firePoint, damage); 
    }

    public override WeaponTypes getWeaponType()
    {
        return WeaponTypes.Machinegun;
    }
}

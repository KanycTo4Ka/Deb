using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(TracerSystem))]
[RequireComponent(typeof(MachineGunLogic))]
public class CMachineGun : CWeapon
{
    TracerSystem tracerSystem;
    MachineGunLogic machinegunLogic;
    

    protected override void Start()
    {
        base.Start();
        tracerSystem = GetComponent<TracerSystem>();
        machinegunLogic = GetComponent<MachineGunLogic>();
    }

    public override void fire(Ammunition ammunition)
    {
        base.fire(ammunition);

        tracerSystem.createTracer(firePoint.position, firePoint.forward);
        machinegunLogic.shot(firePoint, curDamage); 
    }

    public override WeaponTypes getWeaponType()
    {
        return WeaponTypes.Machinegun;
    }
}

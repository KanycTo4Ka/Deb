using UnityEngine;

[RequireComponent(typeof(RocketLauncherLogic))]
public class CRocketLauncher : CWeapon
{
    RocketLauncherLogic rocketLauncherLogic;

    protected override void Start()
    {
        base.Start();
        rocketLauncherLogic = GetComponent<RocketLauncherLogic>();
    }

    public override void fire(Ammunition ammunition)
    {
        base.fire(ammunition);

        rocketLauncherLogic.shot(firePoint, curDamage);
    }

    public override WeaponTypes getWeaponType()
    {
        return WeaponTypes.RocketLauncher;
    }
}

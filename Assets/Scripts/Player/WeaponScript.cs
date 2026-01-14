using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    CWeapon currentWeapon;
    bool isFiring = false;
    Ammunition ammunition;

    private void Start() => ammunition = GetComponent<Ammunition>();

    public void setWeapon(CWeapon selectedWeapon)
    {
        if (selectedWeapon != null)
        {
            fireEnd();
            currentWeapon = selectedWeapon;
            if (currentWeapon.weaponEffect != null)
                currentWeapon.weaponEffect.Stop();
        }
    }

    public void fireStart()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.GetComponent<Animator>().SetBool("isFire", true);
        isFiring = true;
        if (currentWeapon.weaponEffect != null && currentWeapon.canFire)
            currentWeapon.weaponEffect.Play();
    }

    public void fireEnd()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.GetComponent<Animator>().SetBool("isFire", false);
        isFiring = false;
        if (currentWeapon != null)
            if (currentWeapon.weaponEffect != null)
                currentWeapon.weaponEffect.Stop();
    }

    private void Update()
    {
        if (currentWeapon != null)
            if (isFiring)
                if (ammunition.checkAmmo(currentWeapon.getWeaponType()))
                {
                    if (currentWeapon.canFire)
                    {
                        currentWeapon.fire(ammunition);

                        if (currentWeapon.weaponEffect != null)
                            if (currentWeapon.weaponEffect.isPlaying == false)
                                currentWeapon.weaponEffect.Play();
                    }
                }
                else
                if (currentWeapon != null)
                    if (currentWeapon.weaponEffect != null)
                        currentWeapon.weaponEffect.Stop();
    }
}

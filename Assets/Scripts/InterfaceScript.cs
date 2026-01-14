using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] PlayerMovement speed;
    [SerializeField] WeaponSelector weaponSelector;
    [SerializeField] Ammunition ammunition;
    [SerializeField] PlayerScript playerScript;

    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text armorText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text soulModifierText;
    [SerializeField] TMP_Text soulCountText;
    [SerializeField] TMP_Text ammoCountText;

    void Start()
    {
        
    }

    public void updateHealth()
    {
        healthText.text = health.getCurrentHealth().ToString() + "/" + health.getMaxHealth().ToString();
    }

    public void updateSpeed()
    {
        speedText.text = speed.getMoveSpeed().ToString();
    }

    public void updateArmor()
    {
        armorText.text = health.getArmor().ToString();
    }

    public void updateDamage()
    {
        damageText.text = weaponSelector.getSelectedWeapon().getDamage().ToString();
    }

    public void updateSoulModifier()
    {
        soulModifierText.text = playerScript.getSoulModifier().ToString();
    }

    public void updateSoul()
    {
        soulCountText.text = playerScript.getSoul().ToString();
    }

    public void updateAmmo()
    {
        ammoCountText.text = ammunition.getAmmoCount(weaponSelector.getSelectedWeapon().getWeaponType()).ToString();
    }
}

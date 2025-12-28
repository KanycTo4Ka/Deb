using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Transform enemySpawner;

    Health health;
    PlayerMovement speed;
    WeaponSelector weaponSelector;
    Ammunition ammunition;
    PlayerScript playerScript;

    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text armorText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text soulModifierText;
    [SerializeField] TMP_Text soulCountText;
    [SerializeField] TMP_Text ammoCountText;

    void Start()
    {
        health = Player.GetComponent<Health>();
        speed = Player.GetComponent<PlayerMovement>();
        weaponSelector = Player.GetComponent<WeaponSelector>();
        ammunition = Player.GetComponent<Ammunition>();
        playerScript = Player.GetComponent<PlayerScript>();

        healthText.text = health.getCurrentHealth().ToString() + "/" + health.getMaxHealth().ToString() + " + 0";
        speedText.text = speed.getMoveSpeed() + " + 0";
        armorText.text = "0";
        damageText.text = "0";
        soulModifierText.text = "0";
        soulCountText.text = "0";
        ammoCountText.text = "0";
    }

    public void updateHealth()
    {
        healthText.text = health.getCurrentHealth().ToString() + "/" + health.getMaxHealth().ToString() + " + 0";
    }

    public void updateSpeed()
    {

    }

    public void updateArmor()
    {

    }

    public void updateDamage()
    {
        damageText.text = weaponSelector.getSelectedWeapon().getDamage().ToString();
    }

    public void updateSoulModifier()
    {
        soulCountText.text = playerScript.getSoul().ToString();
    }

    public void updateSoul()
    {

    }
    public void updateAmmo()
    {
        ammoCountText.text = ammunition.getAmmoCount(weaponSelector.getSelectedWeapon().getWeaponType()).ToString();
    }
}

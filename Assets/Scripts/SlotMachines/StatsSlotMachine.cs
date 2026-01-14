using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class StatsSlotMachine : AbstractSlotMachine
{
    [SerializeField] Health health;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] List<CWeapon> weapons;

    public override void startSpin()
    {
        base.startSpin();

        playerScript.setToDefaultSoulModifier();
        playerMovement.setToDefault();
        health.setToDefaultMaxHealth();
        health.setToDefaultArmor();
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].setToDefault();
        }
    }

    public override void stopEvent()
    {
        if (results[0] == results[1] && results[1] == results[2])
        {
            switch (results[1])
            {
                case 0:                 //увеличение брони
                    health.modifyArmor(2.1f);
                    break;
                case 1:                 //получение души
                    playerScript.addSoul(5);
                    break;
                case 2:                 //уменьшение брони
                    health.modifyArmor(0.2f);
                    break;
                case 3:                 //уменьшение урона
                    for (int j = 0; j < weapons.Count; j++)
                        weapons[j].modifyDamage(0.3f);
                    break;
                case 4:                 //увеличение здоровья
                    health.modifyMaxHealth(2.25f);
                    break;
                case 5:                 //увеличение выпадения душ с врагов
                    playerScript.modifySoulModifier(2f);
                    break;
                case 6:                 //увеличение скорости
                    playerMovement.modifyMoveSpeed(2f);
                    break;
                case 7:                 //потеря души
                    playerScript.removeSoul(5);
                    break;
                case 8:                 //уменьшение здоровья              
                    health.modifyMaxHealth(0.2f);
                    break;
                case 9:                 //увеличение урона
                    for (int j = 0; j < weapons.Count; j++)
                        weapons[j].modifyDamage(2f);
                    break;
                case 10:                 //уменьшение скорости
                    playerMovement.modifyMoveSpeed(0.2f);
                    break;
                case 11:                 //уменьшение выпадения душ с врагов
                    playerScript.modifySoulModifier(0.25f);
                    break;
            }
        }
        else
        {
            for (int i = 0; i < slots.Count; i++)
            {
                switch (results[i])
                {
                    case 0:                 //увеличение брони
                        health.modifyArmor(1.3f);
                        break;
                    case 1:                 //получение души
                        playerScript.addSoul(1);
                        break;
                    case 2:                 //уменьшение брони
                        health.modifyArmor(0.7f);
                        break;
                    case 3:                 //уменьшение урона
                        for (int j = 0; j < weapons.Count; j++)
                            weapons[j].modifyDamage(0.75f);
                        break;
                    case 4:                 //увеличение здоровья
                        health.modifyMaxHealth(1.25f);
                        break;
                    case 5:                 //увеличение выпадения душ с врагов
                        playerScript.modifySoulModifier(1.3f);
                        break;
                    case 6:                 //увеличение скорости
                        playerMovement.modifyMoveSpeed(1.25f);
                        break;
                    case 7:                 //потеря души
                        playerScript.removeSoul(1);
                        break;
                    case 8:                 //уменьшение здоровья              
                        health.modifyMaxHealth(0.75f);
                        break;
                    case 9:                 //увеличение урона
                        for (int j = 0; j < weapons.Count; j++)
                            weapons[j].modifyDamage(1.25f);
                        break;
                    case 10:                 //уменьшение скорости
                        playerMovement.modifyMoveSpeed(0.75f);
                        break;
                    case 11:                 //уменьшение выпадения душ с врагов
                        playerScript.modifySoulModifier(0.7f);
                        break;
                }
            }
        }
    }
}

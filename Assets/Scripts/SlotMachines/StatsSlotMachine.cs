using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class StatsSlotMachine : AbstractSlotMachine
{
    [SerializeField] Health health;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform EnemySpawner;
    [SerializeField] List<CWeapon> weapons;

    public override void startSpin()
    {
        base.startSpin();

        //health.setToDefault();
        //for (int i = 0; i < weapons.Count; i++)
        //{
        //    weapons[i].setToDefault();
        //}
    }

    public override void stopEvent()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            switch (results[i])
            {
                case 0:
                    print("0bronya+");
                    break;
                case 1:
                    print("1dushi+");
                    break;
                case 2:
                    print("2bronya-");
                    break;
                case 3:
                    print("3uron-");
                    //for (int j = 0; j < weapons.Count; j++)
                    //    weapons[j].modifyDamage(0.75f);
                    break;
                case 4:
                    print("4hp+");
                    health.modifyMaxHealth(1.25f);
                    break;
                case 5:
                    print("5vipadenie_dush-");
                    break;
                case 6:
                    print("6scorost+");
                    playerMovement.modifyMoveSpeed(1.25f);
                    break;
                case 7:
                    print("7dushi-");
                    break;
                case 8:                   
                    print("8hp-");
                    health.modifyMaxHealth(0.75f);
                    break;
                case 9:
                    print("9uron+");
                    //for (int j = 0; j < weapons.Count; j++)
                    //    weapons[j].modifyDamage(1.25f);
                    break;
                case 10:
                    print("10scorost-");
                    playerMovement.modifyMoveSpeed(0.75f);
                    break;
                case 11:
                    print("11vipadenie_dush-");
                    break;
            }
        }
    }
}

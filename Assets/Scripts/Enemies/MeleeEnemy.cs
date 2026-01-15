using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class MeleeEnemy : AbstractEnemy
{
    [Range(0.1f, 30)]
    public float attackRange = 25;
    [Range(1, 100)]
    public float defaultDamage;
    float curDamage;

    RunTo runState;
    Attack attackState;
    Stunned stunnedState;
    RotateTo rotateState;

    protected override void Start()
    {
        base.Start();

        curDamage = defaultDamage;


        runState = new RunTo(this);
        attackState = new Attack(this);
        stunnedState = new Stunned(this);
        rotateState = new RotateTo(this);

        stateMachine.startingState(runState);
    }

    public override void updateState()
    {

        if (dead == true)
            return;
        

        if (stunned == true)
            stateMachine?.setState(stunnedState);
        else
        {
            if (Vector3.Angle(transform.forward, player.position - transform.position) > 20)
                stateMachine?.setState(rotateState);
            else
            {
                if (Vector3.Distance(transform.position, player.position) >= attackRange)
                    stateMachine?.setState(runState);
                else
                {
                    if (Vector3.Angle(transform.forward, player.position - transform.position) > 20)
                        stateMachine?.setState(rotateState);
                    else
                        stateMachine?.setState(attackState);
                }
            }      
        }

        stateMachine?.update();
    }

    public void dealDamage()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange + 2)
        {
            Health playerHP = player.GetComponentInParent<Health>();

            if (playerHP != null)
            {
                playerHP.hpDecrease(curDamage);
            }
        }
    }
}

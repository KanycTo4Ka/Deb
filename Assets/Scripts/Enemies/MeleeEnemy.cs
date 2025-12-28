using UnityEngine;

public class MeleeEnemy : AbstractEnemy
{
    [Range(0.1f, 10)]
    public float attackRange = 2;
    [Range(1, 100)]
    public float defaultDamage;
    float curDamage;

    RunTo runState;
    Attack attackState;
    Stunned stunnedState;
    RotateTo rotateState;
    Defeat defeatState;

    protected override void Start()
    {
        base.Start();

        curDamage = defaultDamage;

        runState = new RunTo(this);
        attackState = new Attack(this);
        stunnedState = new Stunned(this);
        rotateState = new RotateTo(this);
        defeatState = new Defeat(this);

        stateMachine.startingState(runState);
    }

    public override void updateState()
    {
        if (isDefeat)
        {
            stateMachine?.setState(defeatState);
            return;
        }

        if (dead == true) return;

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
                    if (Vector3.Angle(transform.forward, player.position - transform.position) > 10)
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

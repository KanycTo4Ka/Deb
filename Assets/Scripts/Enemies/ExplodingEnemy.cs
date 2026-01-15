using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : AbstractEnemy
{
    [Range(0.1f, 30)]
    public float attackRange = 10;
    [Range(1, 100)]
    public int damage;

    [SerializeField] ParticleSystem explosionEffect;

    [SerializeField] LayerMask targetLayer;

    RunTo runState;
    Attack attackState;
    Stunned stunnedState;
    RotateTo rotateState;

    private new void Start()
    {
        base.Start();
        runState = new RunTo(this);
        attackState = new Attack(this);
        stunnedState = new Stunned(this);
        rotateState = new RotateTo(this);

        explosionEffect.Pause();

        stateMachine.startingState(runState);
    }

    public override void updateState()
    {

        if (dead == true)
            return;

        if (stunned)
            stateMachine?.setState(stunnedState);
        else
        {
            if (Vector3.Angle(transform.forward, player.position - transform.position) > 20)
                stateMachine?.setState(rotateState);
            else
            {
                if (Vector3.Distance(transform.position, player.position) > attackRange)
                    stateMachine?.setState(runState);
                else
                {
                    if (Vector3.Angle(transform.forward, player.position - transform.position) > 20)
                        stateMachine?.setState(rotateState);
                    else
                    {
                        stateMachine?.setState(attackState);
                    }
                }
            }
        }

        stateMachine?.update();
    }

    public void explosion()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            explosionEffect.Play();

            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetLayer);

            foreach (Collider collider in colliders)
            {
                Health enemyHP = collider.gameObject.GetComponent<Health>();
                if (enemyHP != null)
                {
                    enemyHP.hpDecrease(damage);
                }
            }

            death();
        }
    }
}

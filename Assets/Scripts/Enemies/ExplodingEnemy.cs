using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : AbstractEnemy
{
    [Range(0.1f, 10)]
    public float attackRange = 10;
    [Range(1, 100)]
    public int damage;

    List<GameObject> objectsInRadius = new List<GameObject>();

    public ParticleSystem explosionEffect;

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

        explosionEffect.Stop();

        stateMachine.startingState(runState);
    }

    public override void updateState()
    {
        if (isDefeat)
        {
            return;
        }

        if (dead) return;

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
                    if (Vector3.Angle(transform.forward, player.position - transform.position) > 10)
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

    private void OnTriggerEnter(Collider other)
    {
        objectsInRadius.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInRadius.Remove(other.gameObject);
    }

    public void explosion()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            explosionEffect.Play();

            foreach (var obj in objectsInRadius)
            {
                if (obj != null)
                {
                    Health HP = obj.GetComponent<Health>();
                    if (HP != null)
                        HP.hpDecrease(damage);
                }
            }

            death();
        }
    }
}

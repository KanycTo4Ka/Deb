using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public abstract class AbstractEnemy : MonoBehaviour, IEnemy
{
    protected Transform player;
    public Health enemyHP;

    [Range(1, 60)]
    public float updatesPerSecond = 10;
    [Range(1, 360)]
    public float rotationSpeed = 120;

    NavMeshAgent agent;
    Animator animator;

    [SerializeField] GameObject soulPrefab;

    protected bool stunned = false;
    protected bool dead = false;
    protected bool isDefeat = false;

    protected StateMachine stateMachine;

    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }

    public Health EnemyHP { get { return enemyHP; }}

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        stateMachine = new StateMachine();

        StartCoroutine(updateCall());
    }

    IEnumerator updateCall()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / updatesPerSecond);

            updateState();

            if (dead) break;
        }
    }

    public abstract void updateState();

    public virtual void moveTo(Vector3 point)
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(point);
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
    }

    public virtual void rotateTo(Vector3 point)
    {
        Vector3 dir = point - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed / updatesPerSecond);
    }

    public virtual void attack(bool state)
    {
        animator.SetBool("attack", state);
    }

    public virtual void stunBegin()
    {
        stunned = true;
        animator.SetTrigger("getHit");
    }

    public virtual void stunEnd()
    {
        stunned = false;
    }

    public virtual void stop(bool state)
    {
        if (agent.isOnNavMesh)
            agent.isStopped = state;
    }

    public void positionAndRotation(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }

    public virtual void death()
    {
        dead = true;
        animator.SetBool("death", true);
        stop(true);

        StartCoroutine(despawn());
    }

    public virtual void defeat()
    {
        isDefeat = true;
        animator.SetTrigger("victory");
    }

    public void spawnSoul()
    {
        GameObject soul = Instantiate(soulPrefab, transform.position, Quaternion.identity);
    }

    public IEnumerator despawn()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}

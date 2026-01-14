using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    [SerializeField] LayerMask enemy;

    [Range(0.1f, 3f)]
    [SerializeField] float attackRange = 2f;

    public void swing(float damage)
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, attackRange, enemy);

        foreach (var hit in hits)
        {
            Health enemyHP = hit.GetComponent<Health>();
            if (enemyHP != null)
            {
                enemyHP.hpDecrease(damage);
            }
        }
    }
}

using UnityEngine;

public class PistolLogic : MonoBehaviour
{
    [SerializeField] LayerMask enemy;

    public void shot(Transform firePoint, float damage)
    {
        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 1000f, enemy))
        {
            Health enemyHP = hit.transform.GetComponent<Health>();
            if (enemyHP != null)
            {
                enemyHP.hpDecrease(damage);
            }
        }
        
    }
}

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunLogic : MonoBehaviour
{
    [SerializeField] LayerMask enemy;
    [SerializeField] int buckshots = 8;
    [SerializeField] float spread = 40.0f;

    public List<Vector3> shot(Transform firePoint, float damage)
    {
        List<Vector3> directions = new List<Vector3>();

        for (int i = 0; i < buckshots; i++)
        {
            var angle = Random.Range(-spread/2, spread/2);
            var quaternion = Quaternion.Euler(0, angle, 0);
            var newDirection = quaternion * firePoint.forward;

            directions.Add(newDirection);
        }

        foreach (var direction in directions)
        {
            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, direction, out hit, 1000f, enemy))
            {
                Health enemyHP = hit.transform.GetComponent<Health>();
                if (enemyHP != null)
                {
                    enemyHP.hpDecrease(damage);
                }
            }
        }

        return directions;
    }
}

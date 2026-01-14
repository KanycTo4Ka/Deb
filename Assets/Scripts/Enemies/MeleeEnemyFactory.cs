using UnityEngine;

public class MeleeEnemyFactory : EnemyFactory
{
    [SerializeField] GameObject meleeEnemyPrefab;

    public override IEnemy getEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject meleeEnemy = Instantiate(meleeEnemyPrefab, position, rotation);

        return meleeEnemy.GetComponent<MeleeEnemy>();
    }
}

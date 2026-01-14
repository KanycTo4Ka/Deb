using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public abstract IEnemy getEnemy(Vector3 position, Quaternion rotation);
}

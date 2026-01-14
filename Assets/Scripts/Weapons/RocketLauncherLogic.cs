using UnityEngine;

public class RocketLauncherLogic : MonoBehaviour
{
    [SerializeField] LayerMask enemy;
    [SerializeField] GameObject rocketPrefab;

    public void shot(Transform firePoint, float damage)
    {
        GameObject rocketObj = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

        RocketScript rocket = rocketObj.GetComponent<RocketScript>();
        rocket.Init(this.gameObject.GetComponent<CWeapon>());
    }
}

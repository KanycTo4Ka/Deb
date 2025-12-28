using Unity.VisualScripting;
using UnityEngine;

public class SwordPickUp : MonoBehaviour
{
    [SerializeField] WeaponSelector weaponSelector;

    private void OnTriggerEnter(Collider other)
    {
        weaponSelector.setActiveSword();
        Destroy(gameObject);
    }
}

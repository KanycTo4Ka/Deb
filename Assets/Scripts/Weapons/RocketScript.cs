using System.Collections;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)] float lifetime = 3f;

    [SerializeField]
    [Range(1f, 100f)] float acceleration = 80f;

    [SerializeField] ParticleSystem explosionEffect;

    [SerializeField]
    [Range(1f, 50f)] float explosionRadius = 10f;

    [SerializeField] LayerMask targetLayer;

    Rigidbody rb;

    CWeapon rocketLauncher;

    public void Init(CWeapon weapon)
    {
        rocketLauncher = weapon;
    }

    void Start()
    {
        explosionEffect.Pause();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);

        lifetime -= Time.deltaTime;
        if (lifetime < 0f)
            explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        dealDamage();
        explode();
    }

    void dealDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, targetLayer);

        foreach (Collider collider in colliders)
        {
            Health enemyHP = collider.gameObject.GetComponent<Health>();
            if (enemyHP != null)
            {
                enemyHP.hpDecrease(rocketLauncher.getDamage());
            }
        }
    }

    void explode()
    {
        explosionEffect.Play();
        Destroy(gameObject, 1.0f);
    }
}

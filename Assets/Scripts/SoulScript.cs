using UnityEngine;

public class SoulScript : MonoBehaviour
{
    PlayerScript playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerScript.pickUpSoul();
        Destroy(gameObject);
    }
}

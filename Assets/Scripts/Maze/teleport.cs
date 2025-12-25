using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] GameObject Point;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(-17, 14, -6);
            print("Deb");
        }
    }    
}

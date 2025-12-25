using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform Point;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameObject.Find("deb_point"))
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = GameObject.Find("deb_point").transform.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }    
}

using UnityEngine;

public class enter_teleport : MonoBehaviour
{
    public Transform Point;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameObject.Find("enter_point"))
                other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = GameObject.Find("enter_point").transform.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }    
}

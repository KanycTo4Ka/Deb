using UnityEngine;

public class ScriptLocker : MonoBehaviour
{
    public void lockScripts()
    {
        foreach (var component in transform.GetComponents<MonoBehaviour>())
            if (component != this)
                component.enabled = false;
    }

    public void unlockScripts()
    {
        foreach (var component in transform.GetComponents<MonoBehaviour>())
            if (component != this)
                component.enabled = true;
    }
} 

using UnityEngine;

public interface IInteractable 
{
    public void interact(PlayerScript player);
    public string getDescription();
}

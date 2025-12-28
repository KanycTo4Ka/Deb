using System.Collections;
using UnityEngine;

public interface ISlotMachine
{
    public void startSpin();
    public void stopSpin();
    public void switchToSymbol(Transform slot, int number);
    IEnumerator stopSlots();
    IEnumerator StopAfterDelay(float delay);
    public void stopEvent();
}

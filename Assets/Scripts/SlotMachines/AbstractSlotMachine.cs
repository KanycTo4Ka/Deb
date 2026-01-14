using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractSlotMachine : MonoBehaviour, ISlotMachine, IInteractable
{
    [TextArea]
    [SerializeField] string description;

    [SerializeField] public List<Transform> slots;
    List<bool> spinned = new List<bool>();

    [SerializeField] int symbolsCount = 12;

    [HideInInspector]
    public List<int> results = new List<int>();

    [HideInInspector]
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < slots.Count; i++)
            spinned.Add(false);
    }

    void Update()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (spinned[i])
            {
                slots[i].Rotate(Vector3.down * 10000 * Time.deltaTime);
            }
        }
    }

    public string getDescription()
    {
        return description;
    }

    public virtual void interact(PlayerScript player)
    {
        if (player.getSoul() >= 1)
        {
            player.removeSoul(1);
            animator.SetTrigger("pulled");
            startSpin();
            StartCoroutine(StopAfterDelay(3f));
        }
    }

    public virtual void startSpin()
    {
        results.Clear();
        for (int i = 0; i < slots.Count; i++)
            spinned[i] = true;
    }

    public IEnumerator stopSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            int symbol = Random.Range(0, symbolsCount);
            spinned[i] = false;
            switchToSymbol(slots[i], symbol);
            results.Add(symbol);
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        stopSpin();
        yield return new WaitForSeconds(delay);
        stopEvent();
    }

    public void stopSpin()
    {
        StartCoroutine(stopSlots());
    }

    public void switchToSymbol(Transform slot, int number)
    {
        int angle = number * 360 / symbolsCount;

        slot.localEulerAngles = new Vector3(slot.localEulerAngles.x, angle, slot.localEulerAngles.z);
    }

    public abstract void stopEvent();
}

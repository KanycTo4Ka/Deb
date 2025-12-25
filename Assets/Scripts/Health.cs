using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int maxHealth;
    [Range(1, 100)]
    [SerializeField] int curHealth;

    public UnityEvent<int, int> onHealthChange;

    public UnityEvent<Vector3> spawnOnDeath;
    public UnityEvent onDeath;
    public UnityEvent onHitTaken;

    [SerializeField] GameObject totalScorePanel;
    [SerializeField] TMP_Text totalScoreText;
    //[SerializeField] ScoreScript scoreScript;
    
    //ScriptLocker scriptLocker;

    Animator animator;

    void Start()
    {
        onHealthChange?.Invoke(curHealth, maxHealth);
        //scriptLocker = GetComponent<ScriptLocker>();
        animator = GetComponent<Animator>();
    }

    public bool changeHealth(int amount)
    {
        if (curHealth == maxHealth)
            return false;

        curHealth += amount;
            
        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (curHealth < 0)
            curHealth = 0;

        onHealthChange?.Invoke(curHealth, maxHealth);

        return true;
    }

    public void hpDecrease(float amount)
    {
        if (curHealth <= 0) return;

        onHitTaken?.Invoke();

        curHealth = Mathf.FloorToInt(curHealth - amount);

        if (curHealth < 0)
            curHealth = 0;

        onHealthChange?.Invoke(curHealth, maxHealth);
        if (curHealth <= 0)
        {
            onDeath?.Invoke();
            spawnOnDeath?.Invoke(transform.position);
        }
    }

    public void death()
    {
        animator.SetTrigger("dead");
        //scriptLocker.lockScripts();
        //totalScorePanel.SetActive(true);
        //totalScoreText.text = "»тоговый счЄт: " + scoreScript.getScore().ToString();
    }

    public void getHit()
    {
        animator.SetTrigger("damaged");
    }
}

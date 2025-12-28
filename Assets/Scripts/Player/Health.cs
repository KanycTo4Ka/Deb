using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Range(1f, 100f)]
    [SerializeField] float defaulMaxHealth;
    float curMaxHealth;
    float curHealth;

    public UnityEvent<float, float> onHealthChange;

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
        curMaxHealth = defaulMaxHealth;
        curHealth = curMaxHealth;
        onHealthChange?.Invoke(curHealth, curMaxHealth);
        //scriptLocker = GetComponent<ScriptLocker>();
        animator = GetComponent<Animator>();
    }

    public bool changeHealth(int amount)
    {
        if (curHealth == curMaxHealth)
            return false;

        curHealth += amount;
            
        if (curHealth > curMaxHealth)
            curHealth = curMaxHealth;

        if (curHealth < 0)
            curHealth = 0;

        onHealthChange?.Invoke(curHealth, curMaxHealth);

        return true;
    }

    public void hpDecrease(float amount)
    {
        if (curHealth <= 0) return;

        onHitTaken?.Invoke();

        curHealth = Mathf.FloorToInt(curHealth - amount);

        if (curHealth < 0)
            curHealth = 0;

        onHealthChange?.Invoke(curHealth, curMaxHealth);
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

    public void setToDefault()
    {
        curMaxHealth = defaulMaxHealth;
    }

    public void modifyMaxHealth(float amount)
    {
        curMaxHealth = amount * curMaxHealth;
    }

    public float getCurrentHealth()
    {
        return curHealth;
    }

    public float getMaxHealth()
    {
        return curMaxHealth;
    }
}

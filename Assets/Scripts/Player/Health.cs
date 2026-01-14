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

    float armor = 0;

    public UnityEvent<float, float> onHealthChange;
    public UnityEvent onArmorChange;

    public UnityEvent<Vector3> spawnOnDeath;
    public UnityEvent onDeath;
    public UnityEvent onHitTaken;

    [SerializeField] GameObject gameOverPanel;

    ScriptLocker scriptLocker;

    Animator animator;

    void Start()
    {
        curMaxHealth = defaulMaxHealth;
        curHealth = curMaxHealth;
        onHealthChange?.Invoke(curHealth, curMaxHealth);
        onArmorChange.Invoke();

        scriptLocker = GetComponent<ScriptLocker>();
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
        if (gameObject.CompareTag("Player"))
            curHealth = Mathf.FloorToInt(curHealth - amount * (1 - armor));
        else
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
        scriptLocker.lockScripts();
        gameOverPanel.SetActive(true);
    }

    public void getHit()
    {
        animator.SetTrigger("damaged");
    }

    public void setToDefaultMaxHealth()
    {
        curMaxHealth = defaulMaxHealth;
        onHealthChange?.Invoke(curHealth, curMaxHealth);
    }

    public void modifyMaxHealth(float amount)
    {
        curMaxHealth *= amount;
        onHealthChange?.Invoke(curHealth, curMaxHealth);
    }

    public float getCurrentHealth()
    {
        return curHealth;
    }

    public float getMaxHealth()
    {
        return curMaxHealth;
    }

    public void setToDefaultArmor()
    {
        armor = 0;
        onArmorChange.Invoke();
    }

    public void modifyArmor(float amount)
    {
        armor += amount;
        onArmorChange.Invoke();
    }

    public float getArmor()
    {
        return armor;
    }
}

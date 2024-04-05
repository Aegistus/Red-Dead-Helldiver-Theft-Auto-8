using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static Action OnDeath;

    public static float maxHealth = 100;
    
    public static float currentHealth;
	
	int wastedSoundID;

	bool isDead = false;

	void Awake()
	{
        currentHealth = maxHealth;
		Time.timeScale = 1f;
	}

	void Start()
	{
		wastedSoundID = SoundManager.Instance.GetSoundID("Wasted_Sound");
	}

    public void Damage(float damage)
	{
		currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
	}

	public void Heal(float heal)
	{
		currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	public void Die()
	{
		if (!isDead)
		{
			PlayerMovement playerMove = GetComponent<PlayerMovement>();
			SoundManager.Instance.PlaySoundGlobal(wastedSoundID);
			if (playerMove != null)
			{
				playerMove.enabled = false;
			}
			CarMovement car = GetComponent<CarMovement>();
			if (car != null)
			{
				car.enabled = false;
			}
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.freezeRotation = false;
			isDead = true;
			Time.timeScale = .5f;

			OnDeath?.Invoke();
		}

	}
}
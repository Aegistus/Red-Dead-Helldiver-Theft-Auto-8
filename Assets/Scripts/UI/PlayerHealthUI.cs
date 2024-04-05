using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] RectTransform healthBar;
	float startingWidth;
	void Awake()
	{
		startingWidth = healthBar.sizeDelta.x;
	}
	
	void Update()
	{
		Vector2 sizeDelta = healthBar.sizeDelta;
		sizeDelta.x = Mathf.Lerp(0, startingWidth, PlayerHealth.currentHealth / PlayerHealth.maxHealth);
		healthBar.sizeDelta = sizeDelta;
	}
}

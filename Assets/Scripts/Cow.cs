using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public float MilkMax => milkMax;
    public float MilkRemaining => milkRemaining;

    [SerializeField] MeshRenderer rend;
    [SerializeField] float milkMax;
    [SerializeField] float milkSpeed;

    float milkRemaining;

    private void Start()
    {
        milkRemaining = milkMax;
    }

    public void Milk()
    {
        float amount = Time.deltaTime * milkSpeed;
        if (amount >= milkRemaining)
        {
            amount = milkRemaining;
        }
        GameManager.Instance.ReduceMilkDebt(amount);
        milkRemaining -= amount;
        Color color = rend.material.color;
        color.r = milkRemaining / milkMax;
        color.g = milkRemaining / milkMax;
        color.b = milkRemaining / milkMax;
        rend.material.color = color;
    }
}

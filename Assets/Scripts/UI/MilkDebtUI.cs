using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MilkDebtUI : MonoBehaviour
{
    [SerializeField] TMP_Text value;

    float lastVal;
    GameManager game;

    private void Start()
    {
        game = GameManager.Instance;
    }

    private void Update()
    {
        if (lastVal != game.MilkDebt)
        {
            value.text = ((int)game.MilkDebt).ToString();
        }
        lastVal = game.MilkDebt;
    }
}

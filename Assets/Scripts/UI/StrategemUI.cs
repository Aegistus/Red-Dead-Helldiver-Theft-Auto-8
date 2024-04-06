using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrategemUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] List<ArrowUI> arrows;

    Strategem strat;

    public void SetStratagem(Strategem strat)
    {
        this.strat = strat;
        nameText.text = strat.name;
        int i = 0;
        for (;i < strat.ddrCode.Count; i++)
        {
            arrows[i].SetArrow(strat.ddrCode[i]);
        }
        for (; i < arrows.Count; i++)
        {
            arrows[i].gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRInputUI : MonoBehaviour
{
    [SerializeField] List<ArrowUI> arrows;

    PlayerStrategems playerStrategems;

    private void Start()
    {
        playerStrategems = FindObjectOfType<PlayerStrategems>();
        playerStrategems.OnInputChange += UpdateArrows;
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].gameObject.SetActive(false);
        }
    }

    private void UpdateArrows()
    {
        int i = 0;
        for (; i < playerStrategems.currentInput.Count && i < arrows.Count; i++)
        {
            arrows[i].gameObject.SetActive(true);
            arrows[i].SetArrow(playerStrategems.currentInput[i]);
        }
        for (; i < arrows.Count; i++)
        {
            arrows[i].gameObject.SetActive(false);
        }
    }
}

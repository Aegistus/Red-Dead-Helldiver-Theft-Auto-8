using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategemsManagerUI : MonoBehaviour
{
    [SerializeField] List<StrategemUI> stratagems;

    PlayerStrategems playerStratagems;

    private void Start()
    {
        playerStratagems = FindObjectOfType<PlayerStrategems>();
        playerStratagems.OnEnterStratagemMode += ShowStratagems;
        playerStratagems.OnExitStratagemMode += HideStratagems;
        playerStratagems.OnReadyToThrow += HideStratagems;
        HideStratagems();
    }

    void HideStratagems()
    {
        for (int i = 0; i < stratagems.Count; i++)
        {
            stratagems[i].gameObject.SetActive(false);
        }
    }

    void ShowStratagems()
    {
        int i = 0;
        for (; i < playerStratagems.unlockedStrategems.Count; i++)
        {
            stratagems[i].gameObject.SetActive(true);
            stratagems[i].SetStratagem(playerStratagems.unlockedStrategems[i]);
        }
        for (; i < stratagems.Count; i++)
        {
            stratagems[i].gameObject.SetActive(false);
        }
    }
}

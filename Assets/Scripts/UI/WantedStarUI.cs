using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantedStarUI : MonoBehaviour
{
    public GameObject[] stars;

    void Start()
    {
        GameManager.Instance.OnWantedLevelChange += UpdateStars;
        UpdateStars(0);
    }

    void UpdateStars(int numOfStars)
    {
        int i = 0;
        for (; i < numOfStars; i++)
        {
            stars[i].SetActive(true);
        }
        for (; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
    }
}

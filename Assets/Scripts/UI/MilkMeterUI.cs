using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkMeterUI : MonoBehaviour
{
    [SerializeField] RectTransform milkBar;
    float startingWidth;

    Cow cow;

    void Awake()
    {
        startingWidth = milkBar.sizeDelta.x;
        cow = GetComponentInParent<Cow>();
    }

    void Update()
    {
        Vector2 sizeDelta = milkBar.sizeDelta;
        sizeDelta.x = Mathf.Lerp(0, startingWidth, cow.MilkRemaining / cow.MilkMax);
        milkBar.sizeDelta = sizeDelta;
    }
}

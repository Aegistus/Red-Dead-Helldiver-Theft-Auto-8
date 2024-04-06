using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] GameObject upArrow;
    [SerializeField] GameObject downArrow;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    private void Awake()
    {
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    public void SetArrow(DDR_Direction direction)
    {
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        if (direction == DDR_Direction.UP)
        {
            upArrow.SetActive(true);
        }
        else if (direction == DDR_Direction.DOWN)
        {
            downArrow.SetActive(true);
        }
        else if (direction == DDR_Direction.LEFT)
        {
            leftArrow.SetActive(true);
        }
        else if (direction == DDR_Direction.RIGHT)
        {
            rightArrow.SetActive(true);
        }
    }
}

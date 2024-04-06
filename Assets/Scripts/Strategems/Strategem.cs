using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStrategem", menuName = "Strategem")]
public class Strategem : ScriptableObject
{
    public List<DDR_Direction> ddrCode;
    public GameObject strategemBallPrefab;
}

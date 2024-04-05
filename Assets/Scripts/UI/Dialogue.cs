using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class Frame
    { 
        public string characterName;
        public string text;
    }

    public Frame[] frames;

}

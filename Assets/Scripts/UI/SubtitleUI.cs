using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SubtitleUI : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text text;
    [SerializeField] Dialogue dialogue;
    [SerializeField] string nextSceneName;

    int frameIndex = -1;

    private void Start()
    {
        NextFrame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextFrame();
        }
    }

    public void NextFrame()
    {
        frameIndex++;
        if (frameIndex >= dialogue.frames.Length)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            characterName.text = dialogue.frames[frameIndex].characterName + ":";
            text.text = dialogue.frames[frameIndex].text;
        }
    }
}

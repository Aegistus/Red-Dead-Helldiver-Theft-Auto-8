using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianDialogue : MonoBehaviour
{
    [SerializeField] float minSpeakInterval = 10f;
    [SerializeField] float maxSpeakInterval = 60f;

    int voiceLineID;

    void Start()
    {
        voiceLineID = SoundManager.Instance.GetSoundID("Voice_Lines");
        StartCoroutine(IntervalDialogue());
    }

    IEnumerator IntervalDialogue()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpeakInterval, maxSpeakInterval);
            yield return new WaitForSeconds(waitTime);
            SoundManager.Instance.PlaySoundAtPosition(voiceLineID, transform.position, transform);
        }
    }
}

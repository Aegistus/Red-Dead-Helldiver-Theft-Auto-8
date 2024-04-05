using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WastedUI : MonoBehaviour
{
    [SerializeField] float screenDelay = 2f;

    [SerializeField] GameObject[] startElements;
    [SerializeField] GameObject[] endElements;


    void Start()
    {
        PlayerHealth.OnDeath += StartUI;
        for (int i = 0; i < startElements.Length; i++)
        {
            startElements[i].SetActive(false);
        }
        for (int i = 0; i < endElements.Length; i++)
        {
            endElements[i].SetActive(false);
        }
    }

    void StartUI()
    {
        for (int i = 0; i < startElements.Length; i++)
        {
            startElements[i].SetActive(true);
        }
        StartCoroutine(EndElements());
    }

    IEnumerator EndElements()
    {
        yield return new WaitForSeconds(screenDelay);
        for (int i = 0; i < endElements.Length; i++)
        {
            endElements[i].SetActive(true);
        }
        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        PlayerHealth.OnDeath -= StartUI;
    }
}

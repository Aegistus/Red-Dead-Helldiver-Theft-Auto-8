using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] string nextScene = "IntroScene";


    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

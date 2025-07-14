using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void OnStartMenuClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void OnQuitMenuClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

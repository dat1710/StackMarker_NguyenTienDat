using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //private void Start()
    //{
    //    //int sceneIndex = PlayerPrefs.GetInt("currentScene", 0);
    //    //SceneManager.LoadScene(sceneIndex);
    //}
    public void LoadGame()
    {
        //PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
        //PlayerPrefs.Save();
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("An2");
    }
}

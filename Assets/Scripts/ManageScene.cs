using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageScene : MonoBehaviour
{
    int currentScene;
    int nextScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + 1;
    }

    public void changeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    
    public void goToTutorial()
    {
        SceneManager.LoadScene(2);
    }
    
}

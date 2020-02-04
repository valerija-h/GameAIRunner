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


    // load level 1 without agent
    public void levelOne()
    {
        SceneManager.LoadScene(1);
        SceneStats.agentOption = false;
    }

    // load level 1 with agent
    public void levelOneAgent()
    {
        SceneManager.LoadScene(2);
        SceneStats.agentOption = true;
    }

    public void removeFade()
    {
        this.gameObject.SetActive(false);
    }

    // Remove tutorial scene for now
    public void goToTutorial()
    {
        //SceneManager.LoadScene(2);
    }

}

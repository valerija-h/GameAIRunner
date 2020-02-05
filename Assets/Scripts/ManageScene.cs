using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageScene : MonoBehaviour
{
    int currentScene;
    int nextScene;

    public GameObject difficulty;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + 1;
    }

    public void changeScene()
    {
        FindObjectOfType<SoundManager>().Play("button");

        SceneManager.LoadScene(nextScene);
    }

    // open difficulty menu
    public void openDifficulty() {
   
       FindObjectOfType<SoundManager>().Play("button");
        
        difficulty.SetActive(true);
    }

    // close difficulty menu
    public void closeDifficulty()
    {
        FindObjectOfType<SoundManager>().Play("button");
        difficulty.SetActive(false);
    }

    // load level 1 without agent hard
    public void levelOneHard()
    {
        FindObjectOfType<SoundManager>().Play("button");
        SceneManager.LoadScene(1);
        SceneStats.agentOption = false;
        SceneStats.difficulty = "hard";
    }

    public void loadLevelOne()
    {
        if (SceneStats.agentOption == true)
        {
            levelOneAgent();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void loadLevelTwo() {
        if (SceneStats.agentOption == true)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }

    public void loadMainMenu() {

        SceneManager.LoadScene(0);
    }

    // load level 1 without agent easy
    public void levelOneEasy()
    {
        SceneManager.LoadScene(1);
        SceneStats.agentOption = false;
        SceneStats.difficulty = "easy";
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
        FindObjectOfType<SoundManager>().Play("button");
        SceneManager.LoadScene(11);
        //SceneManager.LoadScene(2);
    }

    public void quit()
    {
        Application.Quit();
    }

}

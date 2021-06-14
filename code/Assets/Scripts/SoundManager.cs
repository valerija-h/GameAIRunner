using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager instance;

    //Scene currentScene = SceneManager.GetActiveScene();

    // Start is called before the first frame update
    //FindObjectOfType<SoundManager>().Play("Theme");
    private void Awake()
    {

        //if (currentScene.name == "KartPlayer" || currentScene.name == "KartPlayerAgent" || currentScene.name == "TennisIL" || currentScene.name == "TennisILAgent")
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        DontDestroyOnLoad(gameObject);
        // }
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        Debug.Log("Im here");
        Play("Theme1");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "not found!");
            return;
        }

        s.source.Play();
    }

    public void stopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "not found!");
            return;
        }

        s.source.Stop();
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPartLoader : MonoBehaviour
{
    public Transform player;

    public bool isLoaded;
    public bool shouldLoad;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.sceneCount > 0)
        {
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }

        try 
        {
            player = GameObject.Find("Player").transform;
        }
        catch(Exception e){}  
    }

    // Update is called once per frame
    // void Update()
    // {
    //     TriggerCheck();
    // }

    // void TriggerCheck()
    // {

    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UnloadScene();
        }
    }

    void LoadScene()
    {
        if(!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void UnloadScene()
    {
        if(isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
}

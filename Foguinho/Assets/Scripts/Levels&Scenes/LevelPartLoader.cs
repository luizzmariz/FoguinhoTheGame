using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPartLoader : MonoBehaviour
{
    [Header("Level Transition")]
    // public bool isLoaded;
    // public bool shouldLoad;
    public string transitionName;
    public bool isUsable = false;
    public bool needActivation = false;
    // public LevelPartLoader referenceLevelPartLoader;
    public string transitionTo;

    [Header("Player")]
    public Vector3 startPosition;

    void Start()
    {
        // if(SceneManager.sceneCount > 0)
        // {
        //     for(int i = 0; i < SceneManager.sceneCount; i++)
        //     {
        //         Scene scene = SceneManager.GetSceneAt(i);
        //         if(scene.name == gameObject.name)
        //         {
        //             isLoaded = true;
        //             // LevelManager.instance.currentLevelPart = this.GetComponent<LevelPart>();
        //             StartCoroutine(SendLevelManagerInfo());
        //         }
        //     }
        // }

        isUsable = false;
        // if(referenceLevelPartLoader == this)
        // {
            startPosition = transform.GetChild(0).position;
            // StartCoroutine(ContactLevelPart());
        // }
    }

    public void LoadData(List<LevelTransitionData> levelPartLoaderData)
    {
        // NPCData npcData = SaveSystem.LoadNPC(this);

        foreach(LevelTransitionData data in levelPartLoaderData)
        {
            if(data.transitionName == this.transitionName)
            {
                this.needActivation = data.needActivation;
            }
        }
    } 

    // IEnumerator ContactLevelPart()
    // {
    //     yield return new WaitForSeconds(0.01f);

    //     bool hasLoadedBefore = false;
    //     LevelPartLoader switcher = null;
    //     LevelPart levelPart = GameObject.Find("LevelPart").GetComponent<LevelPart>();

    //     foreach(LevelPartLoader levelPartLoader in levelPart.levelTransitions)
    //     {
    //         if(levelPartLoader.transitionTo == this.transitionTo && levelPartLoader.partLoaderName == this.partLoaderName)
    //         {
    //             hasLoadedBefore = true;
    //             switcher = levelPartLoader;
    //             levelPartLoader.referenceLevelPartLoader = this;
    //         }
    //     }

    //     if(hasLoadedBefore)
    //     {
    //         this.needActivation = switcher.needActivation;
    //         //Debug.Log("HEELLEN" + " at " + Time.time);
    //     }
    //     else
    //     {
    //         levelPart.AddLevelPartLoader(this);
    //         //Debug.Log("JULHA" + " at " + Time.time);
    //     }
    // }

    // public void ActiveTrigger()
    // {
    //     if(!needActivation)
    //     {
    //         isUsable = true;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isUsable)
            {
                LevelManager.instance.LoadLevelPart(transitionTo);
            }
        }
    }

    // IEnumerator CallLevelTransition()
    // {
    //     yield return new WaitForSeconds(0.01f);
    //     // LevelManager.instance.currentLevelPart = this.GetComponent<LevelPart>();
        
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.CompareTag("Player"))
    //     {
    //         // UnloadScene();
    //     }
    // }

    // void LoadScene()
    // {
    //     if(!isLoaded)
    //     {
    //         SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
    //         isLoaded = true;
    //     }
    // }

    // void UnloadScene()
    // {
    //     if(isLoaded)
    //     {
    //         SceneManager.UnloadSceneAsync(gameObject.name);
    //         isLoaded = false;
    //     }
    // }
}

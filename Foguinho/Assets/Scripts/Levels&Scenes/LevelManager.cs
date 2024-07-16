using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Manager")]
    public static LevelManager instance = null;
    public string levelName;
    public string levelNumber;
    public Animator animator;
    public bool loadingIsReady = false;

    [Header("Level Parts")]
    public LevelPart currentLevelPart = null;
    public bool currentLevelPartIsReady = false;

    void Start()
    {
        if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

        // StartCoroutine(ContactGameManager());
        StartCoroutine(LoadLevelData());
    }

    // IEnumerator ContactGameManager()
    // {
    //     yield return new WaitForSeconds(0.01f);

    //     bool hasLoadedBefore = false;
    //     LevelManager switcher = null;

    //     foreach(LevelManager levelManager in GameManager.instance.levelManagers)
    //     {
    //         if(levelManager.levelNumber == this.levelNumber)
    //         {
    //             hasLoadedBefore = true;
    //             switcher = levelManager;
    //         }
    //     }

    //     if(hasLoadedBefore)
    //     {
    //         this.levelParts = null;
    //         this.levelParts = switcher.levelParts;
    //         this.currentLevelPart = switcher.currentLevelPart;
    //     }
    //     else
    //     {
    //         GameManager.instance.AddLevelManager(this);

    //         //MUDAR ISSO AQUI ABAIXO, O INÍCIO TEM UMA CUTSCENE E DEVE SER UM EVENTO. MEDIDA PROVISÓRIA PRA INICIAR             
    //         StartCoroutine(StartLevelPartLoader("Level01Part03"));
    //     }

    //     GameManager.instance.currentLevel = this;
    // }

    IEnumerator LoadLevelData()
    {
        loadingIsReady = false;

        LevelData levelData = SaveSystem.LoadLevel(this);
        
        if(levelData.currentLevelPart.levelPartName == null || levelData.currentLevelPart.levelPartName == "")
        {
            StartCoroutine(StartLevelPartLoader("Level01Part01"));
        }         
        else
        {
            StartCoroutine(StartLevelPartLoader(levelData.currentLevelPart.levelPartName));
        }

        GameManager.instance.currentLevel = this;
        // StartCoroutine(GameManager.instance.SaveTheGame());
        GameManager.instance.SaveTheGame();

        yield return new WaitUntil(() => loadingIsReady);

        Debug.Log("2 player position -" + GameObject.Find("Player").transform.position);

        GameManager.instance.currentLevelIsReady = true;
    }

    // public void AddLevelPart(LevelPart levelPart)
    // {
    //     LevelPart newLevelPart = this.gameObject.AddComponent<LevelPart>();

    //     newLevelPart.levelPartName = levelPart.levelPartName;
    //     newLevelPart.npcs = levelPart.npcs;
    //     newLevelPart.timeSpentInLevel = levelPart.timeSpentInLevel;
    //     newLevelPart.levelTransitions = levelPart.levelTransitions;

    //     levelParts.Add(newLevelPart);
    //     //currentLevelPart = levelPart;
    // }

    public void LoadLevelPart(string levelPartToLoad)
    {
        animator.SetBool("LoadingScreenIsOpen", true);

        currentLevelPartIsReady = false;

        if(levelPartToLoad == "nextLevel")
        {
            Debug.Log("Next Level");
        }
        else
        {
            StartCoroutine(NextLevelPartLoader(levelPartToLoad));
        }
    }

    IEnumerator StartLevelPartLoader(string levelPartToLoad)
    {
        currentLevelPartIsReady = false;

        SceneManager.LoadSceneAsync(levelPartToLoad, LoadSceneMode.Additive);

        yield return new WaitUntil(() => currentLevelPartIsReady);

        GameObject.Find("Main Camera").transform.position = currentLevelPart.spawnPosition;
        Debug.Log(currentLevelPart.spawnPosition);
        GameObject.Find("Player").transform.position = currentLevelPart.spawnPosition;
        Debug.Log("1 player position -" + GameObject.Find("Player").transform.position);

        foreach(LevelPartLoader levelPartLoader in currentLevelPart.levelTransitions)
        {
            if(!levelPartLoader.needActivation)
            {
                levelPartLoader.isUsable = true;
            }
        }

        loadingIsReady = true;
    }

    IEnumerator NextLevelPartLoader(string levelPartToLoad)
    {
        // Debug.Log("levelPartToLoad = " + levelPartToLoad);
        string lastScene = null;

        if(currentLevelPart != null)
        {
            lastScene = currentLevelPart.levelPartName;
            SceneManager.UnloadSceneAsync(currentLevelPart.levelPartName);
        }
        SceneManager.LoadSceneAsync(levelPartToLoad, LoadSceneMode.Additive);

        yield return new WaitUntil(() => currentLevelPartIsReady);

        animator.SetBool("LoadingScreenIsOpen", false);
        
        StartCoroutine(WalkInAnimation(lastScene));
    }

    IEnumerator WalkInAnimation(string lastScene)
    {
        yield return new WaitForSeconds(0.1f);

        foreach(LevelPartLoader levelPartLoader in currentLevelPart.levelTransitions)
        {
            if(
                //APAGAR ESSA LINHA DEBAIXO DPS E DEIXAR SÓ UM PARAMETRO NO IF - ISSO AQUI É TEMPORÁRIO
                lastScene != null && 
            levelPartLoader.transitionTo == lastScene)
            {
                GameObject.Find("Player").transform.position = levelPartLoader.startPosition;
            }
            if(!levelPartLoader.needActivation)
            {
                levelPartLoader.isUsable = true;
            }
        }
    }

    public void StartSpeakerDialogue(string speakerName, int actionCost)
    {
        foreach(NPC speaker in currentLevelPart.npcs)
        {
            if(speaker.npcName == speakerName)
            {
                currentLevelPart.GetSpeakerDialogue(speakerName, actionCost);
            }
        }
    }
}

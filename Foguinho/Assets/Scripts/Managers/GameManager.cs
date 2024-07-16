using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
	public static GameManager instance = null;
    // public AudioSource source;
    // public AudioClip[] clip;

    [Header("Menu")]
    public bool optionsMenuIsOpen = false;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject loadingProgressBar;
    // [SerializeField] InputAction saveAction;
    // [SerializeField] InputAction loadAction;
    [SerializeField] InputAction openMenu;

    [Header("Scenes")]
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>(); 
    
    [Header("Levels")]
    public List<LevelManager> levelManagers = new List<LevelManager>();
    public LevelManager currentLevel;
    public bool currentLevelIsReady = false;
    public bool loadingIsReady = false;

    void Start() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

        if(optionsMenu == null)
        {
            optionsMenu = transform.GetChild(0).gameObject;
        }

        openMenu.Enable();
        openMenu.performed += context => OnOptions();

        if(SaveSystem.LoadGame() == null)
        {
            GameObject.Find("LoadButton").GetComponent<Button>().interactable = false;
        }
    }

    // void Update() {
    //     if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name == "MenuScene")) {
    //         OnOptions();
    //     }
    // }

    public void OnOptions()
    {
        optionsMenu.SetActive(!optionsMenuIsOpen);
        optionsMenuIsOpen = !optionsMenuIsOpen;
    }

    public void ButtonFunction(string button) {
        switch(button) 
        {
            case "start":
            mainMenu.SetActive(false);
            loadingProgressBar.SetActive(true);
            CreateNewGame();
            break;

            case "load":
            mainMenu.SetActive(false);
            loadingProgressBar.SetActive(true);
            LoadGame();
            break;

            case "options":
            OnOptions();
            break;

            case "exit":
            QuitGame();
            break;

            default:
            break;
        }
    }

    public void CreateNewGame()
    {
        SaveSystem.DeleteSave();
        SaveSystem.CreateSave();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("LayoutLevel01", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    public void LoadGame()
    {
        loadingIsReady = false;

        WorldData data = SaveSystem.LoadGame();

        if(data != null)
        {
            if(data.currentLevel.levelNumber == "")
            {
                loadingIsReady = true;
                CreateNewGame();
            }
            else
            {
                scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
                scenesToLoad.Add(SceneManager.LoadSceneAsync("LayoutLevel" + data.currentLevel.levelNumber, LoadSceneMode.Additive));
                StartCoroutine(LoadingScreen());
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }
    
    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        currentLevelIsReady = false;

        float progressNeeded = scenesToLoad.Count;
        for(int i = 0; i < scenesToLoad.Count; i++)
        {
            while(!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                if(loadingProgressBar != null)
                {
                    loadingProgressBar.GetComponentInChildren<Image>().fillAmount = totalProgress / progressNeeded;
                }
                yield return null;
            }
        }

        // yield return new WaitUntil(() => currentLevelIsReady && loadingIsReady);
        yield return new WaitUntil(() => currentLevelIsReady);

        Debug.Log("3 player position -" + GameObject.Find("Player").transform.position);
    }

    public void SaveTheGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadAction()
    {
        // if(currentLevel != null)
        // {
        //     currentLevel.currentLevelPart.LoadAllNPCs();
        // }
    }

    // public void ButtonFunction(string button) {
    //     switch(button) {
    //         case "start":
    //         SceneManager.LoadScene("Level 1");
    //         SetMusic(1);
    //         level = 1;
    //         break;

    //         case "menu":
    //         if(pauseIsOpen) {
    //             ButtonFunction("pause");
    //         }
    //         SceneManager.LoadScene("Menu");
    //         SetMusic(0);
    //         break;

    //         case "quit":
    //         QuitGame();
    //         break;

    //         case "pause":
    //         if (pauseIsOpen) {
    //             SetMusic(1);
    //             Destroy(this.transform.GetChild(0).gameObject);
    //             pauseIsOpen = false;
    //         } else {
    //             SetMusic(0);
    //             Instantiate(pauseMenu, transform);
    //             pauseIsOpen = true;
    //         }
    //         break;
            
    //         case "nextLevel":
    //         level += 1;
    //         if (level <= 8) {
    //             SceneManager.LoadScene("Level " + level);
    //         } else {
    //             SceneManager.LoadScene("Menu");
    //             level = 1;
    //             //ScreneManager.LoadScene("EndScreen");
    //         }
    //         break;

    //         case "gameOver":
    //         SceneManager.LoadScene("GameOver");
    //         SetMusic(2);
    //         break;

    //         case "tryAgain":
    //         SceneManager.LoadScene("Level " + level);
    //         SetMusic(1);
    //         break;

    //         default:
    //         break;
    //     }
    // }

    //0- Menus; 1- Combat; 2- Death;
    // public void SetMusic(int songVal) {
    //     source.clip = clip[songVal];
    //     source.Play(0);
    // }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
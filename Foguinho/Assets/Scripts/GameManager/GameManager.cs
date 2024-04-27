using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
    // public AudioSource source;
    // public AudioClip[] clip;

    public bool optionsMenuIsOpen = false;
    public GameObject optionsMenu;
    public GameObject playerGameObject;
    // private int level;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    // void Update() {
    //     if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().buildIndex != 0)) {
    //         ButtonFunction("pause");
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
            SceneManager.LoadScene("Teste");
            break;

            case "options":
            Debug.Log("wtf");
            OnOptions();
            break;

            case "exit":
            QuitGame();
            break;

            default:
            break;
        }
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
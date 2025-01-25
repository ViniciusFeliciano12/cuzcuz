using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public Canvas menuUI;
    private bool isPaused = false;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
           if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ContinueGame(){
        var audio = menuUI.GetComponents<AudioSource>();
        audio[1].Play();
        menuUI.enabled = false; 
        Time.timeScale = 1f;    
        isPaused = false;
        
        GameController.Instance.playerActive = true;
    }

    public void ExitGame(){
        Time.timeScale = 1f;
        GameController.Instance.SaveGame();
        SceneManager.LoadScene(0);
    }

    public void ReloadGame(int actualScene){
        GameController.Instance.ResetGame();
        ContinueGame();
        SceneManager.LoadScene(actualScene);
    }

    private void PauseGame()
    {
        var audio = menuUI.GetComponents<AudioSource>();
        audio[0].Play();
        menuUI.enabled = true; 
        Time.timeScale = 0.00000001f;
        isPaused = true;
        
        GameController.Instance.playerActive = false;
        
    }
}

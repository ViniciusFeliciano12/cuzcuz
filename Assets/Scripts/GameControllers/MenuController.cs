using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameData gameData;
    public Button continueButton;

    void Start(){
        continueButton.interactable = gameData.useSaveData;
    }

    public void StartNewGame(){
        if (gameData.useSaveData){
            gameData.ResetData();
        }
        
        SceneManager.LoadScene(1);
    }

    public void ContinueGame(){
        if(gameData.useSaveData){
            SceneManager.LoadScene(1);
        }
    }

    public void ExitGame(){
        Application.Quit();
    }
}

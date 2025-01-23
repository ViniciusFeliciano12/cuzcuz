using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameData gameData;
    public GameObject textGameObject;
    public bool canAttack = true;
    public bool playerActive = true;
    private TextMeshProUGUI Text;

    void Start(){
        gameData.useSaveData = true;
        
        Text = textGameObject.GetComponent<TextMeshProUGUI>();
        Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
    }

    public void catchWand(){
        gameData.Player.getSpaceWand = true;
    }

    public int DecreaseCounter(){
        gameData.Player.lifesRemaining--;
        Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
        
        if (gameData.Player.lifesRemaining == 0){
            GameOver();
        }

        return gameData.Player.lifesRemaining;
    }

    public int GoToMaxLife(){
        gameData.Player.lifesRemaining = 5;
        Text.text = "5/5";

        return gameData.Player.lifesRemaining;
    }

    public void GameOver(){
        gameData.ResetData();
        SceneManager.LoadScene(0);
    }
}

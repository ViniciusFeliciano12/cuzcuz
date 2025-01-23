using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameData;

public class GameController : MonoBehaviour
{
    public GameObject textGameObject;
    public bool canAttack = true;
    
    [SerializeField]
    private GameData gameData;
    public bool playerActive = true;
    private TextMeshProUGUI Text;
    private PauseController pauseController;
    private DialogueController dialogueController;
    private EventManager eventManager;

    void Start(){
        pauseController = FindObjectOfType<PauseController>();
        dialogueController = FindObjectOfType<DialogueController>();
        eventManager = FindObjectOfType<EventManager>();
        Text = textGameObject.GetComponent<TextMeshProUGUI>();

        LoadGame();
    }

    public void InvokeEvent(string key){
        if (dialogueController != null){
            eventManager.InvokeEvent(key);
        }
    }

    public void GameOver(){
        gameData.ResetData();
        SceneManager.LoadScene(0);
    }


    #region DatabaseState

    private void LoadGame(){
        gameData.useSaveData = true;

        Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
    }

    public void ResetGame(){
        gameData.ResetData();
    }

    public void UpdateDatabaseFlag(GameFlags flag, bool state){

        switch(flag){
            case GameFlags.SpaceWand: gameData.Player.getSpaceWand = state; break;
            default: break;
        }
    }

    #endregion

    #region VerifyFlags

    public bool VerifyFlag(GameFlags flag){
        return flag switch
        {
            GameFlags.SpaceWand => gameData.Player.getSpaceWand,
            GameFlags.FirstBarrage => gameData.NpcsDialogues.FirstOrDefault(dialogue => dialogue.NpcName == "Sarah").NpcDialogues.FirstOrDefault().AlreadyUsed,
            _ => false,
        };
    }

    public bool VerifyBossKilled(Bosses boss){

        return boss switch
        {
            Bosses.FirstGolemBoss => gameData.Player.getSpaceWand,
            _ => false,
        };
    }

    #endregion

    #region PlayerPosition
    public void SavePlayerPosition(Transform playerTransform){
        gameData.Player.posX = playerTransform.position.x;
        gameData.Player.posY = playerTransform.position.y;
    }

    public Vector2 GetPlayerPosition(){
        return new Vector2(x: gameData.Player.posX, y: gameData.Player.posY);
    }

    #endregion

    #region Life Management

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

    #endregion

    #region DialoguesManagement
    
    public NpcDialogue[] GetDialogueData(){
        return gameData.NpcsDialogues;
    }

    #endregion
}

public enum GameFlags
{
    SpaceWand,
    FirstBarrage,
}

public enum Bosses{
    FirstGolemBoss,
}

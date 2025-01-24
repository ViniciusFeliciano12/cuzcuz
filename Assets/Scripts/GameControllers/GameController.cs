using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameData;

public class GameController : MonoBehaviour
{
    private string savePath;
    public GameObject textGameObject;
    public bool canAttack = true;
    [SerializeField]
    private GameData gameData;
    public bool playerActive = true;
    private TextMeshProUGUI Text;
    private EventManager eventManager;

    void Start(){
        gameData = Resources.Load<GameData>("GameData");
        eventManager = FindObjectOfType<EventManager>();
        Text = textGameObject.GetComponent<TextMeshProUGUI>();
        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        LoadGame();
    }

    public void InvokeEvent(string key){
        if (eventManager != null){
            eventManager.InvokeEvent(key);
        }
    }

    public void GameOver(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }

        gameData.ResetData();
        SceneManager.LoadScene(0);
    }


    #region DatabaseState

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Jogo salvo em: " + savePath);
    }

    private void LoadGame(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }

        gameData.useSaveData = true;

        Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
    }

    public void ResetGame(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
        gameData.ResetData();
    }

    public void UpdateDatabaseFlag(GameFlags flag, bool state){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }

        switch(flag){
            case GameFlags.SpaceWand: gameData.Player.getSpaceWand = state; break;
            default: break;
        }
    }

    #endregion

    #region VerifyFlags

    public bool VerifyFlag(GameFlags flag){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }

        return flag switch
        {
            GameFlags.SpaceWand => gameData.Player.getSpaceWand,
            GameFlags.FirstBarrage => gameData.NpcsDialogues.FirstOrDefault(dialogue => dialogue.NpcName == "Sarah").NpcDialogues.FirstOrDefault().AlreadyUsed,
            _ => false,
        };
    }

    public bool VerifyBossKilled(Bosses boss){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
        return boss switch
        {
            Bosses.FirstGolemBoss => gameData.Player.getSpaceWand,
            _ => false,
        };
    }

    #endregion

    #region PlayerPosition
    public void SavePlayerPosition(Transform playerTransform){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
        gameData.Player.posX = playerTransform.position.x;
        gameData.Player.posY = playerTransform.position.y;
    }

    public Vector2 GetPlayerPosition(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
        return new Vector2(x: gameData.Player.posX, y: gameData.Player.posY);
    }

    #endregion

    #region Life Management

    public int DecreaseCounter(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
        gameData.Player.lifesRemaining--;
        Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
        
        if (gameData.Player.lifesRemaining == 0){
            GameOver();
        }

        return gameData.Player.lifesRemaining;
    }

    public int GoToMaxLife(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
        gameData.Player.lifesRemaining = 5;
        Text.text = "5/5";

        return gameData.Player.lifesRemaining;
    }

    #endregion

    #region DialoguesManagement
    
    public NpcDialogue[] GetDialogueData(){
        if (gameData == null){
            gameData = Resources.Load<GameData>("GameData");
        }
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

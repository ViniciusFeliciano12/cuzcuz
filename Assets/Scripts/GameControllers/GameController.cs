using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameData;

public class GameController : MonoBehaviour
{
    private string savePath;
    public bool canAttack = true;
    private TextMeshProUGUI Text;
    private TextMeshProUGUI CoinsText;
    public static GameController Instance { get; private set; }

    [SerializeField]
    private GameData gameData;
    public bool playerActive = true;
    private EventManager eventManager;
    private DialogueController dialogueController;

    void Awake(){

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameData = Resources.Load<GameData>("GameData");

        eventManager = FindObjectOfType<EventManager>();
        dialogueController = FindObjectOfType<DialogueController>();
        Text = GameObject.Find("Life").GetComponent<TextMeshProUGUI>();
        CoinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();


        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        if (gameData == null){
            Debug.Log("game data null");
        }

        if (eventManager == null){
            Debug.Log("event manager null");
        }

        if (dialogueController == null){
            Debug.Log("dialogue controller nulo");
        }

        LoadGame();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void InvokeEvent(string key){
        if (eventManager != null){
            eventManager.InvokeEvent(key);
        }
    }

    public void GameOver(){
        ResetGame();
        SaveGame();
        SceneManager.LoadScene(0);
    }


    #region DatabaseState

    public void SaveGame()
    {
        if (gameData != null){
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(savePath, json);
            Debug.Log("Jogo salvo em: " + savePath);
        }
    }

    private void LoadGame(){
        if (gameData != null && Text != null){
            gameData.useSaveData = true;
            Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
            CoinsText.text = gameData.Player.coins.ToString();
        }
    }

    public void ResetGame(){
        if (gameData != null){
            gameData.ResetData();
        }
    }

    public void UpdateDatabaseFlag(GameFlags flag, bool state){
        if (gameData != null){
            switch(flag){
                case GameFlags.SpaceWand: gameData.Player.getSpaceWand = state; break;
                case GameFlags.Coins: gameData.Player.coins++; CoinsText.text = gameData.Player.coins.ToString(); break;
                case GameFlags.Lantern: gameData.Player.lanternGet = state; break;
                default: break;
            }
        }
    }

    #endregion

    #region VerifyFlags

    public bool VerifyFlag(GameFlags flag, int? quantity = null){
        if (gameData != null){
            return flag switch
            {
                GameFlags.SpaceWand => gameData.Player.getSpaceWand,
                GameFlags.Coins => gameData.Player.coins >= quantity,
                GameFlags.FirstBarrage => gameData.NpcsDialogues.FirstOrDefault(dialogue => dialogue.NpcName == "Sarah").NpcDialogues.FirstOrDefault().AlreadyUsed,
                GameFlags.Lantern => gameData.Player.lanternGet,
                _ => false,
            };        
        }
        return false;
    }

    public bool VerifyBossKilled(Enemies boss){
        if (gameData != null){
            return boss switch
            {
                Enemies.FirstGolemBoss => gameData.Player.getSpaceWand,
                _ => false,
            };
        }
        return false;
    }

    #endregion

    #region PlayerPosition
    public void SavePlayerPosition(Transform playerTransform){
        if (gameData != null){
            gameData.Player.posX = playerTransform.position.x;
            gameData.Player.posY = playerTransform.position.y;
        }
    }

    public Vector2 GetPlayerPosition(){
        if (gameData != null){
            return new Vector2(x: gameData.Player.posX, y: gameData.Player.posY);
        }

        return new Vector2();
    }

    #endregion

    #region Life Management

    public int DecreaseCounter(){
        if (gameData != null && Text != null){
            gameData.Player.lifesRemaining--;
            Text.text = gameData.Player.lifesRemaining.ToString() + "/5";
            if (gameData.Player.lifesRemaining == 0){
                GameOver();
            }
            return gameData.Player.lifesRemaining;
        }
        return 0;
    }

    public void GoToMaxLife(){
        if (gameData != null && Text != null){
            gameData.Player.lifesRemaining = 5;
            Text.text = "5/5";
        }
    }

    #endregion

    #region DialoguesManagement
    
    public void PlayDialogue(string key, string specificDialogue = null){
                
        if (dialogueController != null){
            dialogueController.PlayDialogue(key, specificDialogue);
        }
    }

    public NpcDialogue[] GetDialogueData(){
        if (gameData != null){
            return gameData.NpcsDialogues;
        }
        return null;
    }

    #endregion
}

public enum GameFlags
{
    Coins,
    SpaceWand,
    FirstBarrage,
    Lantern,
}

public enum Enemies{
    NoBoss,
    FirstGolemBoss,
}

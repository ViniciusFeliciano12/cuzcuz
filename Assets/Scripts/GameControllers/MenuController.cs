using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour
{
    public GameData gameData;
    public Button continueButton;

    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(savePath))
        {
            LoadGame();
            continueButton.interactable = true;  
        }
        else
        {
            continueButton.interactable = false; 
        }
    }

    public void StartNewGame()
    {
        gameData.ResetData();

        SceneManager.LoadScene(1);

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }

    public void ContinueGame()
    {
        if (File.Exists(savePath))
        {
            LoadGame();
            SceneManager.LoadScene(1);
        }
    }

    public void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }

    private void SaveGame()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Jogo salvo em: " + savePath);
    }

    private void LoadGame()
    {
        string json = File.ReadAllText(savePath);
        JsonUtility.FromJsonOverwrite(json, gameData);
        Debug.Log("Jogo carregado de: " + savePath);
    }
}

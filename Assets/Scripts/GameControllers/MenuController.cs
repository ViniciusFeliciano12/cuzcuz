using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartNewGame(){
        SceneManager.LoadScene(1);
    }

    public void ContinueGame(){

    }

    public void ExitGame(){
        Application.Quit();
    }
}

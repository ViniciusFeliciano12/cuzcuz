using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject textGameObject;
    private TextMeshProUGUI Text;
    private int lifesRemaining = 5;
    public int DecreaseCounter(){
        lifesRemaining--;
        Text.text = lifesRemaining.ToString() + "/5";

        if (lifesRemaining == 0){
            GameOver();
        }

        return lifesRemaining;
    }

    public void GameOver(){
        // SceneManager.LoadScene(0);
    }

    void Start()
    {
        Text = textGameObject.GetComponent<TextMeshProUGUI>();
        Text.text = "5/5";
    }
}

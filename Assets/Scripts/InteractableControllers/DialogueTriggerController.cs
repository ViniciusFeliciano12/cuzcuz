using UnityEngine;

public class DialogueTriggerController : MonoBehaviour
{
    public string dialogueKey;
    public string specificDialogue;
    public GameFlags flagToDestroy;
    private GameController gameController;

    protected virtual void Start()
    {
       gameController = FindAnyObjectByType<GameController>();

        VerifyToDestroy();
    }

    private void VerifyToDestroy(){
        if (gameController != null){
            if(gameController.VerifyFlag(flagToDestroy)){
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameController != null && col is not CircleCollider2D && col.CompareTag("Player")){
            gameController.PlayDialogue(dialogueKey, specificDialogue);
        }
    }
}

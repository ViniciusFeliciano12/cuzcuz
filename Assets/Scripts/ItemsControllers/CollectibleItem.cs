using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private GameController gameController;
    private bool alreadyGet = false;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && gameController != null && !alreadyGet){
            Destroy(gameObject, 1f);
            alreadyGet = true;
            gameController.wandCatched = true;
        }
    }
}

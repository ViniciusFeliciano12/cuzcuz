using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameFlags gameFlag;
    private bool alreadyGet = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && GameController.Instance != null && !alreadyGet){
            GameController.Instance.UpdateDatabaseFlag(gameFlag, true);
            alreadyGet = true;
            Destroy(gameObject, 0f);
        }
    }
}

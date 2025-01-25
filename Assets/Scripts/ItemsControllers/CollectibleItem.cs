using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private bool alreadyGet = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && GameController.Instance != null && !alreadyGet){
            Destroy(gameObject, 1f);
            alreadyGet = true;
            GameController.Instance.UpdateDatabaseFlag(GameFlags.SpaceWand, true);
        }
    }
}

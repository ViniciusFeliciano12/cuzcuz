using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private bool alreadyGet = false;
    private AudioSource audioSource;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !alreadyGet){
            alreadyGet = true;

            if (GameController.Instance != null){
                GameController.Instance?.GoToMaxLife();
            }

            if (audioSource != null){
                audioSource.Play();
            }
            
            Destroy(gameObject, 1.5f);
        }
    }
}

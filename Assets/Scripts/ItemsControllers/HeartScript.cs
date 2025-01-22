using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private bool alreadyGet = false;
    private GameController gameController;
    private AudioSource audioSource;
    void Start(){
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !alreadyGet){
            alreadyGet = true;

            if (gameController != null){
                gameController.GoToMaxLife();
            }

            if (audioSource != null){
                audioSource.Play();
            }
            
            Destroy(gameObject, 1.5f);
        }
    }
}

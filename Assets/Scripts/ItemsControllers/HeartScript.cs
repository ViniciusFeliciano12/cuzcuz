using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    private bool get = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !get){
            get = true;
            HeroKnight heroKnight = other.GetComponent<HeroKnight>();
            if (heroKnight != null)
            {
                heroKnight.RegenLife();
            }
            var audioSources = GetComponents<AudioSource>();
            audioSources[0].Play();
            Destroy(gameObject, 1.5f);
        }
    }
}

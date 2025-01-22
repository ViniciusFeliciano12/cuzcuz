using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            HeroKnight heroKnight = other.GetComponent<HeroKnight>();
            if (heroKnight != null)
            {
                heroKnight.GetHit();
            }
        }
    }
}

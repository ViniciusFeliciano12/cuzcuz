using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossController : GolemController
{
    private GameController gameController;

    void Start(){
        gameController = FindAnyObjectByType<GameController>();

        if (gameController.VerifyBossKilled(Bosses.FirstGolemBoss)){
            Destroy(gameObject);
            return;
        }

        attackColiderR = transform.Find("Enemy_SensorR").GetComponent<Collider2D>();
        attackColiderL = transform.Find("Enemy_SensorL").GetComponent<Collider2D>();
        audioSources = GetComponents<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        dropSomething = GolemBossDropsWandAndHeart;
    }
    
    public void GolemBossDropsWandAndHeart(){
        GameObject heartPrefab = Resources.Load<GameObject>("Heart");
        Instantiate(heartPrefab, transform.position, Quaternion.identity);

        GameObject spatialHandPrefab = Resources.Load<GameObject>("SpacialWand");
        Instantiate(spatialHandPrefab, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossController : GolemController
{
    private GameController gameController;

    void Start(){
        gameController = FindAnyObjectByType<GameController>();
        attackColiderR = transform.Find("Enemy_SensorR").GetComponent<Collider2D>();
        attackColiderL = transform.Find("Enemy_SensorL").GetComponent<Collider2D>();
        audioSources = GetComponents<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        dropSomething = GolemBossDropsWandAndHeart;
        StartCoroutine(ExecuteAfterFrame());
    }

    private System.Collections.IEnumerator ExecuteAfterFrame()
    {
        yield return new WaitForEndOfFrame(); 
        if (gameController.VerifyBossKilled(Bosses.FirstGolemBoss)){
            Destroy(gameObject);
        }
    }

    public void GolemBossDropsWandAndHeart(){
        GameObject heartPrefab = Resources.Load<GameObject>("Heart");
        Instantiate(heartPrefab, transform.position, Quaternion.identity);

        GameObject spatialHandPrefab = Resources.Load<GameObject>("SpacialWand");
        Instantiate(spatialHandPrefab, transform.position, Quaternion.identity);
    }
}

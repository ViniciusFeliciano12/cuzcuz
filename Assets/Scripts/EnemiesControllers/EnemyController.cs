using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int lifes;
    public string[] droppables;
    public Enemies enemyFlag;
    protected Transform player; 
    protected Animator animator;
    protected AudioSource[] audioSources;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 randomDirection;
    protected Collider2D attackColiderR;
    protected Collider2D attackColiderL;
    protected bool facingLeft;
    protected float changeDirectionTime = 3f;
    protected float directionTimer;
    protected bool isPlayerDetected = false; 
    protected bool isDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        attackColiderR = transform.Find("Enemy_SensorR").GetComponent<Collider2D>();
        attackColiderL = transform.Find("Enemy_SensorL").GetComponent<Collider2D>();
        audioSources = GetComponents<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        VerifyBossKilled();
    }

    private void VerifyBossKilled(){
        if (enemyFlag != Enemies.NoBoss){
            GameController gameController = FindObjectOfType<GameController>();
            if(gameController.VerifyBossKilled(enemyFlag)){
                Destroy(gameObject, 0f);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isDeath){
            if (isPlayerDetected)
            {
                MoveTowardsPlayerAndAttack();
            }
            else{
                MoveAround();
            }
        }
    }
    public void TakenHit(){
        if (!isDeath){
            lifes--;

            switch(lifes){
                case 0: Die(); break;
                default: TakeDamage(); break;
            }
        }   
    }

    private void DropItem(){
        foreach(var item in droppables){
            GameObject heartPrefab = Resources.Load<GameObject>(item);
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Die(){
        isDeath = true;
        animator.SetTrigger("Hit");
        animator.SetBool("Death", true);  
        audioSources[1].Play(); 

        DropItem();

        Destroy(gameObject, 1f); 
    }

    private void TakeDamage(){
        animator.SetTrigger("Hit");
        audioSources[2].Play(); 
    }

    private void MoveAround()
    {
        directionTimer -= Time.deltaTime;

        if (directionTimer <= 0)
        {
            randomDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), 0).normalized;

            directionTimer = changeDirectionTime;
        }

        transform.position += (Vector3)(randomDirection * 2f * Time.deltaTime);

        if (spriteRenderer != null)
        {
            facingLeft = randomDirection.x < 0;
            spriteRenderer.flipX = facingLeft;
        }

        if (animator != null)
        {
            animator.SetBool("Run", true);
        }
    }

    private void MoveTowardsPlayerAndAttack()
    {
        if (player != null){
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer > (enemyFlag != Enemies.NoBoss ? 3F : 2F))
            {
                if (animator != null){
                    animator.SetBool("Run", true);
                }

                Vector3 direction = (player.position - transform.position).normalized;
                if (spriteRenderer != null)
                {
                    facingLeft = direction.x < 0; 
                    spriteRenderer.flipX = facingLeft;
                }

                transform.position += direction * Time.deltaTime;
            }else{
                if (animator != null){
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            player = other.transform;

            Vector2 directionToPlayer = other.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer);

            if (hit)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    isPlayerDetected = true; 
                }
                else
                {
                    isPlayerDetected = false; 
                }
            }
        }
    }

    public void EnableHitbox()
    {
        audioSources[0].Play();

        if (facingLeft)
        {
            attackColiderL.enabled = true;
        }
        else{
            attackColiderR.enabled = true;
        }
    }

    // Function to disable the hitbox
    public void DisableHitbox()
    {
        attackColiderR.enabled = false;
        attackColiderL.enabled = false;
    }
}
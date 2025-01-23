using System.Collections;
using UnityEngine;

public class TeleportController : ClickableController
{
    private HeroKnight player; 
    private ParticleSystem  teleportParticles; 
    private AudioSource audioSource;
    private float floatAmplitude = 0.2f; 
    private float floatSpeed = 1f; 
    private Vector3 initialPosition; 

    protected override void Start()
    {
        base.Start();

        initialPosition = transform.position;
        player = FindObjectOfType<HeroKnight>();
        teleportParticles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        FloatEffect();

        clickable = gameController.VerifyFlag(GameFlags.SpaceWand);
    }

    void OnMouseDown()
    {
        if (clickable)
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        if (player != null)
        {
            player.transform.position = transform.position;
            player.GetComponent<ParticleSystem>().Play();
        }
        
        if (teleportParticles != null)
        {
            teleportParticles.Play();
        }

        if (audioSource != null){
            audioSource.Play();
        }
    }

    void FloatEffect()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}

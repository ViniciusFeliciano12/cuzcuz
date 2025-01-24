using System;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EventManager : MonoBehaviour
{
    private Tilemap tilemap; 
    private Dictionary<string, UnityEngine.Events.UnityAction> eventMappings;
    private GameController gameController;
    private CinemachineVirtualCamera cinemachineCamera; 
    void Start(){
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        tilemap = FindObjectOfType<Tilemap>();
        gameController = FindObjectOfType<GameController>();

        InstantiateMappings();
        VerifySavedGame();
    }

    private void VerifySavedGame(){
        if(gameController.VerifyFlag(GameFlags.FirstBarrage)){
            RemoveFirstSeal();
        }
    }

    //eventMappings do dictionary

    private void InstantiateMappings(){
        eventMappings = new Dictionary<string, UnityEngine.Events.UnityAction>
        {
            { "OpenPath", SarahDialogueOneAction },
        };
    }

    public void InvokeEvent(string eventKey){
        if (eventMappings.ContainsKey(eventKey))
        {
            eventMappings[eventKey].Invoke();
        }
    }

    private System.Collections.IEnumerator HandleDialogueCameraSequence()
    {
        gameController.playerActive = false;

        var lookAtHereTransform = GameObject.Find("LookAtHere");
        AudioSource looktAtHereAudioSource = null; 

        if (lookAtHereTransform != null)
        {
            cinemachineCamera.Follow = lookAtHereTransform.transform;
            looktAtHereAudioSource = lookAtHereTransform.GetComponent<AudioSource>();
        }

        yield return new WaitForSeconds(2.0f);

        RemoveFirstSeal(looktAtHereAudioSource);

        yield return new WaitForSeconds(2.0f);

        HeroKnight player = FindObjectOfType<HeroKnight>();
        if (player != null)
        {
            cinemachineCamera.Follow = player.transform;
        }

        gameController.playerActive = true;
    }

    private void SarahDialogueOneAction(){
        StartCoroutine(HandleDialogueCameraSequence());
    }

    //eventos padrões
    private void RemoveFirstSeal(AudioSource audioSource = null){
        if (tilemap != null)
        {
            tilemap.SetTile(new(){x = 4, y = -3, z = 0}, null);
            tilemap.SetTile(new(){x = 5, y = -3, z = 0}, null);
            tilemap.SetTile(new(){x = 6, y = -3, z = 0}, null);
            tilemap.SetTile(new(){x = 7, y = -3, z = 0}, null);
            tilemap.SetTile(new(){x = 4, y = -4, z = 0}, null);
            tilemap.SetTile(new(){x = 5, y = -4, z = 0}, null);
            tilemap.SetTile(new(){x = 6, y = -4, z = 0}, null);

            tilemap.RefreshAllTiles();

            if (audioSource !=  null){
                audioSource.Play();
            }

            Debug.Log($"Tiles removidos");
        }
        else
        {
            Debug.LogError("Tilemap não atribuído!");
        }
    }
}

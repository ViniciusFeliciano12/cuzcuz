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
    private CinemachineVirtualCamera cinemachineCamera; 
    void Start(){
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        tilemap = FindObjectOfType<Tilemap>();

        InstantiateMappings();
        VerifySavedGame();
    }

    private void VerifySavedGame(){
        if(GameController.Instance.VerifyFlag(GameFlags.FirstBarrage)){
            RemoveFirstSeal();
        }
    }

    //eventMappings do dictionary

    private void InstantiateMappings(){
        eventMappings = new Dictionary<string, UnityEngine.Events.UnityAction>
        {
            { "OpenPath", SarahDialogueOneAction },
            { "GiveLantern", SarahGiveYouLantern },
            { "InitialDialogue", InitialDialogue }
        };
    }

    public void InvokeEvent(string eventKey){
        Debug.Log("invoke event");
        if (eventMappings.ContainsKey(eventKey))
        {
            eventMappings[eventKey].Invoke();
        }
    }

    private System.Collections.IEnumerator HandleDialogueCameraSequence()
    {
        Debug.Log("Player not active");
        GameController.Instance.nextDialogueEnabled = false;
        GameController.Instance.playerActive = false;

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

        GameController.Instance.playerActive = true;
        GameController.Instance.nextDialogueEnabled = true;
    }

    private void SarahDialogueOneAction(){
        StartCoroutine(HandleDialogueCameraSequence());
    }

    private void InitialDialogue(){
        Debug.Log("iniciou");
        StartCoroutine(handleCamera());
    }
    private System.Collections.IEnumerator handleCamera(){
        Debug.Log("handleCamera");
        GameController.Instance.nextDialogueEnabled = false;
        GameController.Instance.playerActive = false;

        var lookAtHereTransform = GameObject.Find("LookAtHere2");

        if (lookAtHereTransform != null)
        {
            cinemachineCamera.Follow = lookAtHereTransform.transform;
        }

        yield return new WaitForSeconds(2.0f);

        HeroKnight player = FindObjectOfType<HeroKnight>();
        if (player != null)
        {
            cinemachineCamera.Follow = player.transform;
        }

        GameController.Instance.playerActive = true;
        GameController.Instance.nextDialogueEnabled = true;
    }

    private void SarahGiveYouLantern(){
        GameController.Instance.DecreaseCoins(10);

        GameObject lanternPrefab = Resources.Load<GameObject>("IlluminateRing");

        Vector3 position = new()
        {
            x = 18.4f,
            y = 4.5f,
            z = 0f
        };

        Instantiate(lanternPrefab, position, Quaternion.identity);
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

            if (audioSource != null){
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

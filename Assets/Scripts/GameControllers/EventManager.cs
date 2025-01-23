using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EventManager : MonoBehaviour
{
    private Tilemap tilemap; 
    private Dictionary<string, UnityEngine.Events.UnityAction> eventMappings;
    private GameController gameController;
    void Start(){
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

    //eventMappings do dictionary
    private void SarahDialogueOneAction(){
        //mira a câmera nos tilemaps

        //remove tilemaps específicos
        RemoveFirstSeal();
    }


    //eventos padrões
    private void RemoveFirstSeal(){
        if (tilemap != null)
        {
            Vector3Int position1 = new(){x = 4, y = -3, z = 0};
            Vector3Int position2 = new(){x = 5, y = -3, z = 0};
            Vector3Int position3 = new(){x = 6, y = -3, z = 0};
            Vector3Int position4 = new(){x = 7, y = -3, z = 0};
            Vector3Int position5 = new(){x = 4, y = -4, z = 0};
            Vector3Int position6 = new(){x = 5, y = -4, z = 0};
            Vector3Int position7 = new(){x = 6, y = -4, z = 0};

            tilemap.SetTile(position1, null);
            tilemap.SetTile(position2, null);
            tilemap.SetTile(position3, null);
            tilemap.SetTile(position4, null);
            tilemap.SetTile(position5, null);
            tilemap.SetTile(position6, null);
            tilemap.SetTile(position7, null);

            TilemapCollider2D collider = tilemap.GetComponent<TilemapCollider2D>();
            if (collider != null)
            {
                tilemap.RefreshTile(position1);
                tilemap.RefreshTile(position2);
                tilemap.RefreshTile(position3);
                tilemap.RefreshTile(position4);
                tilemap.RefreshTile(position5);
                tilemap.RefreshTile(position6);
                tilemap.RefreshTile(position7);
            }

            Debug.Log($"Tiles removidos");
        }
        else
        {
            Debug.LogError("Tilemap não atribuído!");
        }
    }
}

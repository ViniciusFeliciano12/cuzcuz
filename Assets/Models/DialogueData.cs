using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class GameData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string Name;
        [TextArea] public string Text;
        public string EventKey;
    }
    
    [System.Serializable]
    public class Dialogues{
        public string DialogueName;
        public DialogueLine[] lines;
        public bool AlreadyUsed = false;
    }

    [System.Serializable]
    public class NpcDialogue{
        public string NpcName;
        public Dialogues[] NpcDialogues;
    }

    [System.Serializable]
    public class PlayerData{
        public float posX = -19.09f;
        public float posY = 0.47f;
        public int lifesRemaining = 5;
        public int coins = 0;
        public bool getSpaceWand = false;
        public bool lanternGet = false;
    }

    public void ResetData()
    {
        useSaveData = false;
        
        Player.posX = -19.09f;
        Player.posY = 0.47f;
        Player.getSpaceWand = false;
        Player.lanternGet = false;
        Player.lifesRemaining = 5;
        Player.coins = 0;

        foreach (var npc in NpcsDialogues)
        {
            foreach (var dialogue in npc.NpcDialogues)
            {
                dialogue.AlreadyUsed = false;
            }
        }
    }

    public bool useSaveData = false;
    public PlayerData Player;
    public NpcDialogue[] NpcsDialogues;
}
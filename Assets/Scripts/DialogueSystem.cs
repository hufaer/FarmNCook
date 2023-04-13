using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour, IDataPersistence
{
    [SerializeField] TMPro.TextMeshProUGUI dialogueReplics;
    [SerializeField] List<string> linesForGreeting;
    [SerializeField] List<string> linesForCompletedQuest;
    [SerializeField] List<string> commonLines;
    [SerializeField] Merchant merchant;
    [SerializeField] Item wood;

    int currLine = 0;
    public int currDialogue = 0;

    public bool isCompleted = false;

    List<string>[] dialogues = new List<string>[3];

    private void Awake()
    {
        dialogues[0] = linesForGreeting;
        dialogues[1] = linesForCompletedQuest;
        dialogues[2] = commonLines;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !PauseGame.isPaused)
        {
            NextLine();
        }
    }

    private bool isGreetingCompleted = false;
    public void Init()
    {
        if (currDialogue == 0 && isGreetingCompleted)
        {
            if (CheckQuest())
            {
                currDialogue++;
            }
        }
        GameManager.instance.toolbar.gameObject.SetActive(false);
        gameObject.SetActive(true);
        dialogueReplics.text = dialogues[currDialogue][0];
        isCompleted = false;
    }

    private void NextLine()
    {
        int count = dialogues[currDialogue].Count;

        if (currLine + 1 >= count)
        {
            currLine = 0;
            gameObject.SetActive(false);
            isCompleted = true;

            if (currDialogue != 0)
            {
                currDialogue++;
                if (currDialogue > 2)
                {
                    currDialogue = 2;
                }
                merchant.isInteracting = true;
                merchant.StartTrade();
            } else
            {
                isGreetingCompleted = true;
                GameManager.instance.toolbar.gameObject.SetActive(true);
            }
        } else
        {
            currLine++;
            string text = dialogues[currDialogue][currLine];
            dialogueReplics.text = text;
        }
    }

    public bool CheckQuest()
    {
        var woodSlots = GameManager.instance.inventory.slots.Find(x => x.item == wood);
        if (woodSlots != null && woodSlots.count >= 3)
        {
            GameManager.instance.inventory.Remove(wood, 3);
            return true;
        }
        return false;
    }

    public void LoadData(GameData gameData)
    {
        currDialogue = gameData.dialogueId;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.dialogueId = currDialogue;
    }
}

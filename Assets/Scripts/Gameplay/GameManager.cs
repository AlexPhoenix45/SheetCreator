using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private GameObject contentPos;

    private CompleteSheet currentSheet = new CompleteSheet();
    private List<CompleteSentence> sentencesData = new List<CompleteSentence>();
    private List<CompleteNote> notesData = new List<CompleteNote>();
    private Notes currentKey;
    private string songName;

    private int currentSentenceIndex = 0;
    private int currentNoteIndex = 0;
    
    public static bool editMode = false; //0 = Sentence, 1 = Note

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetMode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameData.changeModeKey))
        {
            ChangeMode();
        }

        if (!editMode)
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(GameData.newKey))
            {
                CreateSentence();
            }

            if (Input.GetKeyDown(GameData.deleteKey))
            {
                DeleteSentence();
            }

            if (Input.GetKeyDown(GameData.upKey))
            {
                SentenceMoveUp();
            }

            if (Input.GetKeyDown(GameData.downKey))
            {
                SentenceMoveDown();
            }
        }
        else
        {
        }
    }

    //Sentence
    private void CreateSentence()
    {
        sentencesData.Add(new CompleteSentence());
        currentSentenceIndex = sentencesData.Count - 1;
        ContentUIManager.UpdateQuantitySentence(sentencesData);
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex);
    }

    private void DeleteSentence()
    {
        sentencesData.RemoveAt(currentSentenceIndex);
            
        if (sentencesData.Count == 0)
        {
            currentSentenceIndex = -1;
        }
        else if (sentencesData.Count == 1)
        {
            currentSentenceIndex = 0;
        }
        
        ContentUIManager.UpdateQuantitySentence(sentencesData);
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex);
    }

    private void SentenceMoveUp()
    {
        currentSentenceIndex = currentSentenceIndex - 1 < 0 ? sentencesData.Count - 1 : currentSentenceIndex - 1; 
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex);
    }

    private void SentenceMoveDown()
    {
        currentSentenceIndex = currentSentenceIndex + 1 >= sentencesData.Count ? 0 : currentSentenceIndex + 1; 
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex);
    }

    //Mode
    private void SetMode()
    {
        if (sentencesData.Count == 0)
        {
            editMode = false;
        }
    }

    private void ChangeMode()
    {
        if (sentencesData.Count == 0)
        {
            editMode = false;
        }
        else
        {
            if (!editMode)
            {
                editMode = true;
                
            }
            else
            {
                editMode = false;
            }
        }
    }
}
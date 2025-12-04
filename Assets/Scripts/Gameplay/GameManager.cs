using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private GameObject contentPos;

    private CompleteSheet currentSheet = new CompleteSheet();
    private List<CompleteSentence> sentences = new List<CompleteSentence>();
    private List<CompleteNote> notes = new List<CompleteNote>();
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
            if (Input.GetKeyDown(GameData.newKey))
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
        var tempSentence = new CompleteSentence();
        sentences.Add(tempSentence); 
        ContentUIManager.UpdateSentence(sentences);
        ContentUIManager.UpdateCurrentSentence(tempSentence);
    }

    private void DeleteSentence()
    {
        sentences.RemoveAt(sentences.Count - 1);
        var tempSentence = sentences[^1];
        ContentUIManager.UpdateSentence(sentences);
        ContentUIManager.UpdateCurrentSentence(tempSentence);
    }

    private void SentenceMoveUp()
    {
        // var index = sentences.IndexOf(currentSentence);
        // currentSentence.SetActive(false);
        // currentSentence = index - 1 >= 0 ? sentences[index - 1] : sentences[^1];
        // currentSentence.SetActive(true);
    }

    private void SentenceMoveDown()
    {
        // var index = sentences.IndexOf(currentSentence);
        // currentSentence.SetActive(false);
        // currentSentence = index + 1 < sentences.Count ? sentences[index + 1] : sentences[0];
        // currentSentence.SetActive(true);
    }

    //Mode
    private void SetMode()
    {
        if (sentences.Count == 0)
        {
            editMode = false;
        }
    }

    private void ChangeMode()
    {
        if (sentences.Count == 0)
        {
            editMode = false;
        }
        else
        {
            editMode = !editMode;
        }
    }
}
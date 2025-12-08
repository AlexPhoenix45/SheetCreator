using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
        if (sentencesData.Count > 0)
        {
            var debugText = sentencesData[currentSentenceIndex].lyric + "\n";
            debugText += sentencesData[currentSentenceIndex].notes.Aggregate("", (current, item) => current + (item.note + "" + item.octave + " "));
            DebugManager.AddDebugText("sentencesNotes", debugText);
        }
        
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
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(GameData.newKey))
            {
                CreateNote();
            }

            if (Input.GetKeyDown(GameData.deleteKey))
            {
                DeleteNote();
            }

            if (Input.GetKeyDown(GameData.rightKey))
            {
                NoteMoveRight();
            }

            if (Input.GetKeyDown(GameData.leftKey))
            {
                NoteMoveLeft();
            }

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                if (Input.GetKeyDown(GameData.cKey) || Input.GetKeyDown(GameData.cKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.dKey) || Input.GetKeyDown(GameData.dKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.eKey) || Input.GetKeyDown(GameData.eKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.fKey) || Input.GetKeyDown(GameData.fKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.gKey) || Input.GetKeyDown(GameData.gKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.aKey) || Input.GetKeyDown(GameData.aKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.bKey) || Input.GetKeyDown(GameData.bKeyAlt))
                {
                    
                }
            }
            else
            {
                if (Input.GetKeyDown(GameData.cKey) || Input.GetKeyDown(GameData.cKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.dKey) || Input.GetKeyDown(GameData.dKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.eKey) || Input.GetKeyDown(GameData.eKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.fKey) || Input.GetKeyDown(GameData.fKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.gKey) || Input.GetKeyDown(GameData.gKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.aKey) || Input.GetKeyDown(GameData.aKeyAlt))
                {
                    
                }

                if (Input.GetKeyDown(GameData.bKey) || Input.GetKeyDown(GameData.bKeyAlt))
                {
                    
                }
            }
        }
    }

    //Sentence
    private void CreateSentence()
    {
        sentencesData.Add(new CompleteSentence());
        currentSentenceIndex = sentencesData.Count - 1;
        ContentUIManager.UpdateQuantitySentence(sentencesData);
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, true);
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
        
        ContentUIManager.SaveSentence();
        ContentUIManager.UpdateQuantitySentence(sentencesData);
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, true);
    }

    private void SentenceMoveUp()
    {
        currentSentenceIndex = currentSentenceIndex - 1 < 0 ? sentencesData.Count - 1 : currentSentenceIndex - 1; 
        ContentUIManager.SaveSentence();
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, true);
    }

    private void SentenceMoveDown()
    {
        currentSentenceIndex = currentSentenceIndex + 1 >= sentencesData.Count ? 0 : currentSentenceIndex + 1; 
        ContentUIManager.SaveSentence();
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, true);
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
                
                notesData = sentencesData[currentSentenceIndex].notes.ToList();
                currentNoteIndex = notesData.Count - 1;
                ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, false);

                if (currentNoteIndex < 0) return;
                ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);

            }
            else
            {
                editMode = false;
                currentNoteIndex = 0;
                
                SaveNote();
                ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, true);
                ContentUIManager.UpdateCurrentNote(currentNoteIndex, false);
            }
        }
    }
    
    //Note
    private void CreateNote()
    {
        SaveNote();
        notesData = sentencesData[currentSentenceIndex].notes.ToList();
        notesData.Add(new CompleteNote());
        currentNoteIndex = notesData.Count - 1;
        ContentUIManager.UpdateQuantityNote(notesData);
        ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);
    }

    private void DeleteNote()
    {
        if (notesData.Count == 0) return;
        notesData.RemoveAt(currentNoteIndex);
        SaveNote();
        currentNoteIndex--;
        ContentUIManager.UpdateQuantityNote(notesData);
        ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);
    }

    private void NoteMoveRight()
    {
        SaveNote();
        currentNoteIndex = currentNoteIndex + 1 >= notesData.Count ? 0 : currentNoteIndex + 1;        
        ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);
    }

    private void NoteMoveLeft()
    {
        SaveNote();
        currentNoteIndex = currentNoteIndex - 1 < 0 ? notesData.Count - 1 : currentNoteIndex - 1;
        ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);
    }

    private void SaveNote()
    {
        sentencesData[currentSentenceIndex].notes = notesData.ToArray();
    }
}
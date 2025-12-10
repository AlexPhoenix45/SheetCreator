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
    
    private string lastKeyPress = "None";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Screen.fullScreenMode = FullScreenMode.Windowed;

        SetMode();
    }

    private void Update()
    {
        if (sentencesData.Count > 0)
        {
            var debugText = sentencesData[currentSentenceIndex].lyric + "\n";
            debugText += sentencesData[currentSentenceIndex].notes.Aggregate("", (current, item) => current + (item.note + "" + item.octave + " "));
            DebugManager.AddDebugText("sentencesNotes", debugText);
            DebugManager.AddDebugText("screenHeight", Screen.height.ToString());
            DebugManager.AddDebugText("lastKeyPressed", lastKeyPress);
        }
        
        if (Input.GetKeyDown(GameData.changeModeKey))
        {
            lastKeyPress = "changeModeKey";
            ChangeMode();
        }

        if (Input.GetKeyDown(GameData.sharpSwitchKey))
        {
            lastKeyPress = "sharpSwitchKey";
            SharpSwitch();
        }

        if (!GameData.editMode)
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(GameData.newKey))
            {
                lastKeyPress = "shift + newKey";
                CreateSentence();
            }

            if (Input.GetKeyDown(GameData.editLyricKey))
            {
                lastKeyPress = "editLyricKey";
                EditLyric();
            }

            if (Input.GetKeyDown(GameData.deleteKey))
            {
                lastKeyPress = "deleteKey";
                DeleteSentence();
            }

            if (Input.GetKeyDown(GameData.upKey))
            {
                lastKeyPress = "upKey";
                SentenceMoveUp();
            }

            if (Input.GetKeyDown(GameData.downKey))
            {
                lastKeyPress = "downKey";
                SentenceMoveDown();
            }
        }
        else
        {
            if ((Input.GetKey(GameData.addNewKey) || Input.GetKey(GameData.addNewKeyAlt)) && Input.GetKeyDown(GameData.newKey))
            {
                lastKeyPress = "shift + newKey";
                CreateNote();
            }

            if (Input.GetKeyDown(GameData.deleteKey))
            {
                lastKeyPress = "deleteKey";
                DeleteNote();
            }

            if (Input.GetKeyDown(GameData.rightKey))
            {
                lastKeyPress = "rightKey";
                NoteMoveRight();
            }

            if (Input.GetKeyDown(GameData.leftKey))
            {
                lastKeyPress = "leftKey";
                NoteMoveLeft();
            }

            if ((Input.GetKey(GameData.sharpKey) || Input.GetKey(GameData.sharpKeyAlt)))
            {
                if (Input.GetKeyDown(GameData.cKey) || Input.GetKeyDown(GameData.cKeyAlt))
                {
                    lastKeyPress = "shift + cKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.CSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.dKey) || Input.GetKeyDown(GameData.dKeyAlt))
                {
                    lastKeyPress = "shift + dKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.DSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.eKey) || Input.GetKeyDown(GameData.eKeyAlt))
                {
                    lastKeyPress = "shift + eKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.F,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.fKey) || Input.GetKeyDown(GameData.fKeyAlt))
                {
                    lastKeyPress = "shift + fKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.FSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.gKey) || Input.GetKeyDown(GameData.gKeyAlt))
                {
                    lastKeyPress = "shift + gKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.GSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.aKey) || Input.GetKeyDown(GameData.aKeyAlt))
                {
                    lastKeyPress = "shift + aKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.ASharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.bKey) || Input.GetKeyDown(GameData.bKeyAlt))
                {
                    lastKeyPress = "shift + bKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.C,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }
            }
            else if ((Input.GetKey(GameData.flatKey) || Input.GetKey(GameData.flatKeyAlt)))
            {
                if (Input.GetKeyDown(GameData.cKey) || Input.GetKeyDown(GameData.cKeyAlt))
                {
                    lastKeyPress = "ctrl + cKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.B,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.dKey) || Input.GetKeyDown(GameData.dKeyAlt))
                {
                    lastKeyPress = "ctrl + dKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.CSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.eKey) || Input.GetKeyDown(GameData.eKeyAlt))
                {
                    lastKeyPress = "ctrl + eKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.DSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.fKey) || Input.GetKeyDown(GameData.fKeyAlt))
                {
                    lastKeyPress = "ctrl + fKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.E,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.gKey) || Input.GetKeyDown(GameData.gKeyAlt))
                {
                    lastKeyPress = "ctrl + gKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.FSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.aKey) || Input.GetKeyDown(GameData.aKeyAlt))
                {
                    lastKeyPress = "ctrl + aKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.GSharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.bKey) || Input.GetKeyDown(GameData.bKeyAlt))
                {
                    lastKeyPress = "ctrl + bKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.ASharp,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }
            }
            else
            {
                if (Input.GetKeyDown(GameData.cKey) || Input.GetKeyDown(GameData.cKeyAlt))
                {
                    lastKeyPress = "cKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.C,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.dKey) || Input.GetKeyDown(GameData.dKeyAlt))
                {
                    lastKeyPress = "dKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.D,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.eKey) || Input.GetKeyDown(GameData.eKeyAlt))
                {
                    lastKeyPress = "eKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.E,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.fKey) || Input.GetKeyDown(GameData.fKeyAlt))
                {
                    lastKeyPress = "fKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.F,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.gKey) || Input.GetKeyDown(GameData.gKeyAlt))
                {
                    lastKeyPress = "gKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.G,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.aKey) || Input.GetKeyDown(GameData.aKeyAlt))
                {
                    lastKeyPress = "aKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.A,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote();
                }

                if (Input.GetKeyDown(GameData.bKey) || Input.GetKeyDown(GameData.bKeyAlt))
                {
                    lastKeyPress = "bKey";
                    var oldOctave = notesData[currentNoteIndex].octave;
                    notesData[currentNoteIndex] = new CompleteNote()
                    {
                        note = Notes.B,
                        octave = oldOctave,
                    };
                    ContentUIManager.EditNote(notesData[currentNoteIndex]);
                    SaveNote(); 
                }
            }

            if (Input.GetKeyDown(GameData.octaveUpKey))
            {
                lastKeyPress = "octaveUp";
                var oldOctave = notesData[currentNoteIndex].octave;
                var oldNote = notesData[currentNoteIndex].note;
                notesData[currentNoteIndex] = new CompleteNote()
                {
                    note = oldNote,
                    octave = oldOctave+1,
                };
                ContentUIManager.EditNote(notesData[currentNoteIndex]);
                SaveNote();
            }

            if (Input.GetKeyDown(GameData.octaveDownKey))
            {
                lastKeyPress = "octaveDown";
                var oldOctave = notesData[currentNoteIndex].octave;
                var oldNote = notesData[currentNoteIndex].note;
                notesData[currentNoteIndex] = new CompleteNote()
                {
                    note = oldNote,
                    octave = oldOctave-1,
                };
                ContentUIManager.EditNote(notesData[currentNoteIndex]);
                SaveNote();
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
        ContentUIManager.ContainerFollow(currentSentenceIndex);
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
        ContentUIManager.ContainerFollow(currentSentenceIndex);
    }

    private void SentenceMoveDown()
    {
        currentSentenceIndex = currentSentenceIndex + 1 >= sentencesData.Count ? 0 : currentSentenceIndex + 1; 
        ContentUIManager.SaveSentence();
        ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, true);
        ContentUIManager.ContainerFollow(currentSentenceIndex);
    }

    private void SharpSwitch()
    {
        GameData.isSharpNotes = !GameData.isSharpNotes;        
        ContentUIManager.SharpSwitch();
    }

    private void EditLyric()
    {
        ContentUIManager.EditLyric();
    }

    //Mode
    private void SetMode()
    {
        if (sentencesData.Count == 0)
        {
            GameData.editMode = false;
        }
    }

    private void ChangeMode()
    {
        if (sentencesData.Count == 0)
        {
            GameData.editMode = false;
        }
        else
        {
            if (!GameData.editMode)
            {
                GameData.editMode = true;
                
                notesData = sentencesData[currentSentenceIndex].notes.ToList();
                currentNoteIndex = 0;
                ContentUIManager.UpdateCurrentSentence(currentSentenceIndex, false);

                if (currentNoteIndex < 0) return;
                ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);

            }
            else
            {
                GameData.editMode = false;
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
        notesData = sentencesData[currentSentenceIndex].notes.ToList();
        notesData.Add(new CompleteNote());
        currentNoteIndex = notesData.Count - 1;
        SaveNote();
        ContentUIManager.UpdateQuantityNote(notesData);
        ContentUIManager.UpdateCurrentNote(currentNoteIndex, true);
    }

    private void DeleteNote()
    {
        if (notesData.Count == 0) return;
        notesData.RemoveAt(currentNoteIndex);
        currentNoteIndex--;
        SaveNote();
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

    //Save
    
    private void SaveNote()
    {
        sentencesData[currentSentenceIndex].notes = notesData.ToArray();
    }
}
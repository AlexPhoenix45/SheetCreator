using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SentenceUIItem : MonoBehaviour
{
    [SerializeField] private Transform notesContainer;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private TMP_InputField lyricIF;
    [SerializeField] private Image bgImage;
    
    private List<NoteUIItem> notesUI = new List<NoteUIItem>();
    private List<CompleteNote> notesData = new List<CompleteNote>();
    
    private CompleteSentence sentence;
    private int activeNoteIndex = 0;
    
    public void SetActiveSentence(bool value)
    {
        bgImage.color = value ? GameData.activeColor : GameData.normalColor; 
        if (value)
        {
            lyricIF.Select();
            lyricIF.ActivateInputField();
        }
        else
        {
            lyricIF.DeactivateInputField();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    
    public void Init(CompleteSentence sentence)
    {
        this.sentence = sentence;
        lyricIF.text = sentence.lyric;
        notesUI.Clear();
        
        foreach (Transform child in notesContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var t in sentence.notes)
        {
            var tempNoteUIItem = Instantiate(notePrefab, notesContainer).GetComponent<NoteUIItem>();
            notesUI.Add(tempNoteUIItem);
            tempNoteUIItem.UpdateNote(t);
        }
        
        lyricIF.onSubmit.AddListener(OnSubmit_LyricIF);
    }

    private void OnSubmit_LyricIF(string lyricText)
    {
        sentence.lyric = lyricText;
    }

    public void SaveData()
    {
        if (sentence == null) return;
        sentence.lyric = lyricIF.text;
    }
    
    public CompleteSentence GetSentence()
    {
        return sentence;
    }

    public void UpdateQuantityNote(List<CompleteNote> notes)
    {
        notesData = notes;
        notesUI.Clear();
        foreach (Transform child in notesContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var item in notesData)
        {
            var tempNoteUIItem = Instantiate(notePrefab, notesContainer).GetComponent<NoteUIItem>();
            notesUI.Add(tempNoteUIItem);
            tempNoteUIItem.UpdateNote(item);
        }
    }

    public void UpdatePositionNote(int index, bool activeValue)
    {
        notesUI[activeNoteIndex].SetActive(false);
        activeNoteIndex = index;
        notesUI[activeNoteIndex].SetActive(activeValue);
    }

    public void EditNote(CompleteNote note)
    {
        notesUI[activeNoteIndex].UpdateNote(note);
    }

    public void RefreshNote()
    {
        foreach (var item in notesUI)
        {
            item.UpdateNote();
        }
    }

    public void EditLyric()
    {
        lyricIF.Select();
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentenceUIItem : MonoBehaviour
{
    [SerializeField] private Transform notesContainer;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private TMP_InputField lyricIF;
    [SerializeField] private Image bgImage;
    
    private List<NoteUIItem> notesUI = new List<NoteUIItem>();
    private List<CompleteNote> notesData = new List<CompleteNote>();
    private CompleteNote currentNote = new CompleteNote();
    
    private CompleteSentence sentence;
    private int activeNoteIndex = 0;
    
    public void SetActive(bool value)
    {
        bgImage.color = value ? GameData.activeColor : GameData.normalColor;
        if (value) lyricIF.Select();
    }

    public void SetActiveNote()
    {
    }
    
    public void Init(CompleteSentence sentence)
    {
        this.sentence = sentence;
        
        lyricIF.text = sentence.lyric;
        
        foreach (Transform child in notesContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var t in sentence.notes)
        {
            var tempNoteUIItem = Instantiate(notePrefab, notesContainer).GetComponent<NoteUIItem>();
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
        sentence.lyric = lyricIF.text;
    }
    
    public CompleteSentence GetSentence()
    {
        return sentence;
    }

}

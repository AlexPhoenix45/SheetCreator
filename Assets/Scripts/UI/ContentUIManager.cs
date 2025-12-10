using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentUIManager : MonoBehaviour
{
    public static ContentUIManager Instance;
    [SerializeField] private SentenceUIItem sentencePrefab;
    [SerializeField] private RectTransform sentencesContainer;
    [SerializeField] private RectTransform viewport;

    private readonly float itemHeight = 60;
    private readonly float gap = 10;

    private List<SentenceUIItem> sentencesUI = new List<SentenceUIItem>();
    private List<CompleteSentence> sentencesData = new List<CompleteSentence>();
    private List<CompleteNote> notesData = new List<CompleteNote>();
    private int currentSentenceIndex = -1;
    private int currentNoteIndex = -1;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public static void UpdateQuantitySentence(List<CompleteSentence> sentences)
    {
        Instance._UpdateQuantitySentence(sentences);
    }

    private void _UpdateQuantitySentence(List<CompleteSentence> _sentences)
    {
        sentencesData = _sentences;

        foreach (Transform child in sentencesContainer)
        {
            Destroy(child.gameObject);
        }
        
        sentencesUI.Clear();

        var posY = gap;
        foreach (CompleteSentence item in sentencesData)
        {
            var tempSentence = Instantiate(sentencePrefab, sentencesContainer);
            tempSentence.Init(item);
            tempSentence.transform.localPosition = new Vector2(tempSentence.transform.localPosition.x, -posY);
            sentencesUI.Add(tempSentence);
            posY += itemHeight;
        }

        UpdateSentenceSize();
    }

    public static void UpdateCurrentSentence(int currentSentenceIndex, bool showSentence)
    {
        Instance._UpdateCurrentSentence(currentSentenceIndex, showSentence);
    }

    private void _UpdateCurrentSentence(int _currentSentenceIndex, bool _showSentence)
    {
        if (currentSentenceIndex >= 0) sentencesUI[currentSentenceIndex].SetActiveSentence(false);        
        currentSentenceIndex = _currentSentenceIndex;
        sentencesUI[currentSentenceIndex].SetActiveSentence(_showSentence);        
    }

    private void UpdateSentenceSize()
    {
        var size = sentencesContainer.sizeDelta;
        size.y = sentencesData.Count * itemHeight + gap/2;
        sentencesContainer.sizeDelta = size;
    }

    public static void ContainerFollow(int index)
    {
        Instance._ContainerFollow(index);
    }
    
    private void _ContainerFollow(int _index)
    {
        currentSentenceIndex = _index;
        
        var pos = (currentSentenceIndex * -itemHeight) - gap;
        var upperBound = Mathf.Abs(pos) - (gap/2);
        var lowerBound = Mathf.Max(0, Mathf.Abs(pos) - viewport.rect.height + itemHeight - (gap/2));

        if (sentencesContainer.localPosition.y > upperBound)
        {
            sentencesContainer.localPosition = new Vector2(sentencesContainer.localPosition.x, upperBound);
        }
        
        if (sentencesContainer.localPosition.y < lowerBound)
        {
            sentencesContainer.localPosition = new Vector2(sentencesContainer.localPosition.x, lowerBound);
        }
    }

    public static void UpdateCurrentNote(int noteIndex, bool activeValue) 
    {
        Instance._UpdateCurrentNote(noteIndex, activeValue);
    }
        
    
    private void _UpdateCurrentNote(int _noteIndex, bool _activeValue)
    {
        currentNoteIndex = _noteIndex;
        sentencesUI[currentSentenceIndex].UpdatePositionNote(currentNoteIndex, _activeValue);     
    }

    public static void UpdateQuantityNote(List<CompleteNote> notes)
    {
        Instance._UpdateQuantityNote(notes);
    }

    private void _UpdateQuantityNote(List<CompleteNote> _notes)
    {
        sentencesUI[currentSentenceIndex].UpdateQuantityNote(_notes);
    }

    public static void EditNote(CompleteNote note)
    {
        Instance._EditNote(note);
    }

    private void _EditNote(CompleteNote _note)
    {
        sentencesUI[currentSentenceIndex].EditNote(_note);
    }

    public static void SaveSentence()
    {
        Instance._SaveSentence();
    }

    private void _SaveSentence()
    {
        sentencesUI[currentSentenceIndex].SaveData();
    }

    public static void SharpSwitch()
    {
        Instance._SharpSwitch();
    }

    private void _SharpSwitch()
    {
        for (var i = 0; i < sentencesUI.Count; i++)
        {
            var item = sentencesUI[i];
            item.RefreshNote();
        }
    }

    public static void EditLyric()
    {
        Instance._EditLyric();
    }

    private void _EditLyric()
    {
        sentencesUI[currentSentenceIndex].EditLyric();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentUIManager : MonoBehaviour
{
    public static ContentUIManager Instance;
    [SerializeField] private SentenceUIItem[] sentencesUI;
    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform viewport;

    private int prevOffset = 0;
    private readonly float itemHeight = 60;
    private readonly float gap = 10;

    private List<CompleteSentence> sentencesData = new List<CompleteSentence>();
    private List<CompleteNote> notesData = new List<CompleteNote>();
    private int currentSentenceDataIndex = -1;
    private int currentNoteIndex = -1;
    private int sentencesUILength = 0;
    private int prevSentenceUILength = -1;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        UpdateQuantitySentence();
    }

    private void Update()
    {
        var offset = (int)Mathf.Max(0, container.localPosition.y / itemHeight);
        sentencesUILength = Mathf.CeilToInt(Screen.height / (itemHeight + gap));

        if (prevOffset == offset && prevSentenceUILength == sentencesUILength) return;
        UpdatePosition(offset);
        if (sentencesData.Count > 0) UpdateCurrentSentence(currentSentenceDataIndex, true);
        prevOffset = offset;
        prevSentenceUILength = sentencesUILength;
    }

    private void UpdatePosition(int offset)
    {
        if (sentencesData.Count > 0)
        {
            for (var i = 0; i < sentencesUILength; i++)
            {
                var offsetIndex = offset + i;

                if (offsetIndex < sentencesData.Count)
                {
                    sentencesUI[i].gameObject.SetActive(true);
                    sentencesUI[i].Init(sentencesData[offsetIndex]);

                    sentencesUI[i].transform.localPosition = new Vector2(0, (offsetIndex * -itemHeight) - gap);
                }
                else
                {
                    sentencesUI[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (var i = 0; i < sentencesUILength; i++)
            {
                var item = sentencesUI[i];
                item.gameObject.SetActive(false);
            }
        }
    }

    public static void UpdateQuantitySentence(List<CompleteSentence> sentences)
    {
        Instance._UpdateQuantitySentence(sentences);
    }

    private void _UpdateQuantitySentence(List<CompleteSentence> _sentences)
    {
        sentencesData = _sentences;

        container.anchoredPosition = Vector2.zero;
        var size = container.sizeDelta;
        size.y = sentencesData.Count * itemHeight + gap;
        container.sizeDelta = size;

        for (var i = 0; i < sentencesUILength; i++)
        {
            if (i < sentencesData.Count - 1)
            {
                sentencesUI[i].Init(sentencesData[i]);
            }
        }

        UpdatePosition(prevOffset);
    }

    private void UpdateQuantitySentence()
    {
        container.anchoredPosition = Vector2.zero;
        var size = container.sizeDelta;
        size.y = sentencesData.Count * itemHeight + gap; //20 px top bound
        container.sizeDelta = size;

        for (var i = 0; i < sentencesUILength; i++)
        {
            if (i < sentencesData.Count - 1)
            {
                sentencesUI[i].Init(sentencesData[i]);
            }
        }

        UpdatePosition(prevOffset);
    }

    public static void UpdateCurrentSentence(int index, bool activeValue)
    {
        Instance._UpdateCurrentSentence(index, activeValue);
    }

    private void _UpdateCurrentSentence(int index, bool _activeValue)
    {
        currentSentenceDataIndex = index;

        for (var i = 0; i < sentencesUILength; i++)
        {
            var item = sentencesUI[i];
            item.SetActiveSentence(false);
            if (item.GetSentence() == sentencesData[currentSentenceDataIndex])
            {
                item.SetActiveSentence(_activeValue);
            }
        }
    }

    public static void ContainerFollow(int index)
    {
        Instance._ContainerFollow(index);
    }
    
    private void _ContainerFollow(int _index)
    {
        currentSentenceDataIndex = _index;
        
        var pos = (currentSentenceDataIndex * -itemHeight) - gap;
        var upperBound = Mathf.Abs(pos) - (gap/2);
        var lowerBound = Mathf.Max(0, Mathf.Abs(pos) - viewport.rect.height + itemHeight - (gap/2));

        if (container.localPosition.y > upperBound)
        {
            container.localPosition = new Vector2(container.localPosition.x, upperBound);
        }
        
        if (container.localPosition.y < lowerBound)
        {
            container.localPosition = new Vector2(container.localPosition.x, lowerBound);
        }
    }

    public static void UpdateCurrentNote(int noteIndex, bool activeValue) 
    {
        Instance._UpdateCurrentNote(noteIndex, activeValue);
    }
        
    
    private void _UpdateCurrentNote(int _noteIndex, bool _activeValue)
    {
        currentNoteIndex = _noteIndex;
        sentencesUI[currentSentenceDataIndex - prevOffset].UpdatePositionNote(currentNoteIndex, _activeValue);     
    }

    public static void UpdateQuantityNote(List<CompleteNote> notes)
    {
        Instance._UpdateQuantityNote(notes);
    }

    private void _UpdateQuantityNote(List<CompleteNote> _notes)
    {
        sentencesUI[currentSentenceDataIndex - prevOffset].UpdateQuantityNote(_notes);
    }

    public static void EditNote(CompleteNote note)
    {
        Instance._EditNote(note);
    }

    private void _EditNote(CompleteNote _note)
    {
        sentencesUI[currentSentenceDataIndex - prevOffset].EditNote(_note);
    }

    public static void SaveSentence()
    {
        Instance._SaveSentence();
    }

    private void _SaveSentence()
    {
        sentencesUI[currentSentenceDataIndex - prevOffset].SaveData();
    }

    public static void SharpSwitch()
    {
        Instance._SharpSwitch();
    }

    private void _SharpSwitch()
    {
        for (var i = 0; i < sentencesUILength; i++)
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
        sentencesUI[currentSentenceDataIndex - prevOffset].EditLyric();
    }
}
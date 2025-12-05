using System;
using System.Collections.Generic;
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
    private int currentSentenceIndex = -1;
    private int currentNoteIndex = -1;    

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
        if (offset == prevOffset) return;
        UpdatePosition(offset);
        UpdateCurrentSentence(currentSentenceIndex);
        prevOffset = offset;
    }

    private void UpdatePosition(int offset)
    {
        if (sentencesData.Count > 0)
        {
            for (var i = 0; i < sentencesUI.Length; i++)
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
            foreach (var item in sentencesUI)
            {
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

        for (var i = 0; i < sentencesUI.Length; i++)
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

        for (var i = 0; i < sentencesUI.Length; i++)
        {
            if (i < sentencesData.Count - 1)
            {
                sentencesUI[i].Init(sentencesData[i]);
            }
        }

        UpdatePosition(prevOffset);
    }

    public static void UpdateCurrentSentence(int index)
    {
        Instance._UpdateCurrentSentence(index);
    }

    private void _UpdateCurrentSentence(int index)
    {
        currentSentenceIndex = index;

        foreach (var item in sentencesUI)
        {
            item.SetActive(false);
            item.SaveData();
            if (item.GetSentence() == sentencesData[currentSentenceIndex])
            {
                item.SetActive(true);
            }
        }

        ContainerFollow(currentSentenceIndex);
    }

    public static void ContainerFollow(int index)
    {
        Instance._ContainerFollow(index);
    }
    
    private void _ContainerFollow(int _index)
    {
        var pos = (_index * -itemHeight) - gap;
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

    // private void SetActiveNote(List<CompleteNote> notesData)
    // {
    //     note
    // }
}
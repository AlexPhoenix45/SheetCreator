using System;
using System.Collections.Generic;
using UnityEngine;

public class ContentUIManager : MonoBehaviour
{
    public static ContentUIManager Instance;
    [SerializeField] private SentenceUIItem[] sentencePrefab;
    [SerializeField] private RectTransform container;

    private int prevOffset = 0;
    private readonly float itemHeight = 60;
    private readonly float gap = 10;

    private List<CompleteSentence> sentences = new List<CompleteSentence>();
    private CompleteSentence currentSentence = new CompleteSentence();
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        
        UpdateSentence();
    }

    private void Update()
    {
        var offset = (int)Mathf.Max(0, container.localPosition.y / itemHeight);
        if (offset == prevOffset) return;
        Debug.Log(offset);
        UpdatePosition(offset);
        UpdateCurrentSentence();
        prevOffset = offset;
    }

    private void UpdatePosition(int offset)
    {
        if (sentences.Count > 0)
        {
            for (var i = 0; i < sentencePrefab.Length; i++)
            {
                var offsetIndex = offset + i;

                if (offsetIndex < sentences.Count)
                {
                    sentencePrefab[i].gameObject.SetActive(true);
                    sentencePrefab[i].SetValue(sentences[offsetIndex]);
                
                    sentencePrefab[i].transform.localPosition = offsetIndex == 0 ? new Vector2(0, -gap) : new Vector2(0, (offsetIndex * -itemHeight) - gap);
                }
                else
                {
                    sentencePrefab[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (var item in sentencePrefab)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
    
    public static void UpdateSentence(List<CompleteSentence> sentences)
    {
        Instance._UpdateSentence(sentences);
    }

    private void _UpdateSentence(List<CompleteSentence> _sentences)
    {
        sentences = _sentences;
        
        container.anchoredPosition = Vector2.zero;
        var size = container.sizeDelta;
        size.y = sentences.Count * itemHeight; //20 px top bound
        container.sizeDelta = size;
        
        for (var i = 0; i < sentencePrefab.Length; i++)
        {
            if (i < sentences.Count - 1)
            {
                sentencePrefab[i].SetValue(sentences[i]);
            }
        }

        UpdatePosition(prevOffset);
    }
    
    private void UpdateSentence()
    {
        container.anchoredPosition = Vector2.zero;
        var size = container.sizeDelta;
        size.y = sentences.Count * itemHeight; //20 px top bound
        container.sizeDelta = size;
        
        for (var i = 0; i < sentencePrefab.Length; i++)
        {
            if (i < sentences.Count - 1)
            {
                sentencePrefab[i].SetValue(sentences[i]);
            }
        }

        UpdatePosition(prevOffset);
    }

    public static void UpdateCurrentSentence(CompleteSentence sentence)
    {
        Instance._UpdateCurrentSentence(sentence);
    }
    
    private void _UpdateCurrentSentence(CompleteSentence _sentence)
    {
        this.currentSentence = _sentence;
        
        foreach (var item in sentencePrefab)
        {
            item.SetActive(false);
            if (item.GetSentence() == currentSentence)
            {
                item.SetActive(true);
            }
        }
    }

    private void UpdateCurrentSentence()
    {
        foreach (var item in sentencePrefab)
        {
            item.SetActive(false);
            if (item.GetSentence() == currentSentence)
            {
                item.SetActive(true);
            }
        }
    }
}

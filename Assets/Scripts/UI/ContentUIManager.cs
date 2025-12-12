using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ContentUIManager : MonoBehaviour
{
    public static ContentUIManager Instance;
    [SerializeField] private SentenceUIItem sentencePrefab;
    [SerializeField] private RectTransform sentencesContainer;
    [SerializeField] private RectTransform viewport;
    [SerializeField] private TMP_InputField fileNameIF;
    [SerializeField] private TextMeshProUGUI songKeyText;

    private readonly float itemHeight = 60;
    private readonly float gap = 10;

    private List<SentenceUIItem> sentencesUI = new List<SentenceUIItem>();
    private List<CompleteSentence> sentencesData = new List<CompleteSentence>();
        
    private int prevCurrentSentenceIndex = 0;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    //Sentence
    
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
        for (var i = 0; i < sentencesData.Count; i++)
        {
            var item = sentencesData[i];
            var tempSentence = Instantiate(sentencePrefab, sentencesContainer);
            tempSentence.Init(item, i);
            tempSentence.transform.localPosition = new Vector2(tempSentence.transform.localPosition.x, -posY);
            sentencesUI.Add(tempSentence);
            posY += itemHeight;
        }

        UpdateSentenceSize();
    }

    public static void UpdateCurrentSentence(bool showSentence)
    {
        Instance._UpdateCurrentSentence(showSentence);
    }

    public static void UpdateCurrentSentence(SentenceUIItem item, bool showSentence)
    {
        Instance._UpdateCurrentSentence(item, showSentence);
    }

    private void _UpdateCurrentSentence(bool _showSentence)
    {
        if (prevCurrentSentenceIndex < sentencesUI.Count)
        {
            sentencesUI[prevCurrentSentenceIndex].SetActiveSentence(false);
        }

        if (GameManager.currentSentenceIndex == -1) return;
        sentencesUI[GameManager.currentSentenceIndex].SetActiveSentence(_showSentence);
        prevCurrentSentenceIndex = GameManager.currentSentenceIndex;
    }

    private void _UpdateCurrentSentence(SentenceUIItem _item, bool _showSentence)
    {
        if (prevCurrentSentenceIndex >= sentencesUI.Count)
        {
            sentencesUI[prevCurrentSentenceIndex].SetActiveSentence(false);
        }
        
        if (GameManager.currentSentenceIndex == -1) return;
        for (var i = 0; i < sentencesUI.Count; i++)
        {
            sentencesUI[i].SetActiveSentence(false);
            if (sentencesUI[i] != _item) continue;
            GameManager.currentSentenceIndex = i;
            sentencesUI[i].SetActiveSentence(_showSentence);
        }
        prevCurrentSentenceIndex = GameManager.currentSentenceIndex;
    }

    private void UpdateSentenceSize()
    {
        var size = sentencesContainer.sizeDelta;
        size.y = sentencesData.Count * itemHeight + gap / 2;
        sentencesContainer.sizeDelta = size;
    }

    public static void ContainerFollow(int index)
    {
        Instance._ContainerFollow(index);
    }

    private void _ContainerFollow(int _index)
    {
        GameManager.currentSentenceIndex = _index;

        var pos = (GameManager.currentSentenceIndex * -itemHeight) - gap;
        var upperBound = Mathf.Abs(pos) - (gap / 2);
        var lowerBound = Mathf.Max(0, Mathf.Abs(pos) - viewport.rect.height + itemHeight - (gap / 2));

        if (sentencesContainer.localPosition.y > upperBound)
        {
            sentencesContainer.localPosition = new Vector2(sentencesContainer.localPosition.x, upperBound);
        }

        if (sentencesContainer.localPosition.y < lowerBound)
        {
            sentencesContainer.localPosition = new Vector2(sentencesContainer.localPosition.x, lowerBound);
        }
    }

    //Note
    
    public static void UpdateCurrentNote(bool activeValue)
    {
        Instance._UpdateCurrentNote(activeValue);
    }

    private void _UpdateCurrentNote(bool _activeValue)
    {
        sentencesUI[GameManager.currentSentenceIndex].UpdatePositionNote(_activeValue);
    }

    public static void UpdateQuantityNote(List<CompleteNote> notes)
    {
        Instance._UpdateQuantityNote(notes);
    }

    private void _UpdateQuantityNote(List<CompleteNote> _notes)
    {
        sentencesUI[GameManager.currentSentenceIndex].UpdateQuantityNote(_notes);
    }

    public static void EditNote(CompleteNote note)
    {
        Instance._EditNote(note);
    }

    private void _EditNote(CompleteNote _note)
    {
        sentencesUI[GameManager.currentSentenceIndex].EditNote(_note);
    }

    //Function
    
    public static void SaveSentence()
    {
        Instance._SaveSentence();
    }

    private void _SaveSentence()
    {
        if (sentencesData.Count == 0) return;
        sentencesUI[prevCurrentSentenceIndex].SaveData();
    }

    public static void RefreshSentence()
    {
        Instance._RefreshSentence();
    }

    private void _RefreshSentence()
    {
        foreach (var item in sentencesUI)
        {
            item.RefreshNote();
        }
    }

    public static void EditLyric()
    {
        Instance._EditLyric();
    }

    private void _EditLyric()
    {
        if (sentencesData.Count > 0)
        {
            sentencesUI[GameManager.currentSentenceIndex].EditLyric();
        }
    }
    
    //Get Set
    public static string GetFileName()
    {
        return Instance._GetFileName();
    }

    private string _GetFileName()
    {
        return fileNameIF.text;
    }

    public static void SetFileName(string fileName)
    {
        Instance._SetFileName(fileName);
    }

    private void _SetFileName(string _fileName)
    {
        fileNameIF.text = _fileName;
    }

    public static void SetSongKey(Notes note)
    {
        Instance._SetSongKey(note);
    }

    private void _SetSongKey(Notes _note)
    {
        songKeyText.text = "Key: " + _note;
    }
}
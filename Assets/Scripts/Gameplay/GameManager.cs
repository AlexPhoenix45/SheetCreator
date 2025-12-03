using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private GameObject contentPos;

    private List<Sentence> sentences = new List<Sentence>();
    private Sentence currentSentence;
    private Note currentNote;

    public static bool editMode = false; //0 = Sentence, 1 = Note

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameData.changeModeKey))
        {
            editMode = !editMode;
        }
        
        if (editMode)
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
                
            }

            if (Input.GetKeyDown(GameData.downKey))
            {
                
            }
        }
        else
        {
            
        }
    }

    private void CreateSentence()
    {
        if (sentences.Count > 0)
        {
            foreach (var item in sentences)
            {
                item.SetActive(false);
            }
        }

        GameObject tempSentence = Instantiate(sentencePrefab, contentPos.transform);
        sentences.Add(tempSentence.GetComponent<Sentence>());
        currentSentence = tempSentence.GetComponent<Sentence>();
        currentSentence.SetActive(true);
    }

    private void DeleteSentence()
    {
        if (sentences.Count <= 0) return;
        
        Destroy(currentSentence.gameObject);
        var index = sentences.IndexOf(currentSentence);
        sentences.RemoveAt(index);

        if (index - 1 < 0) return;
        currentSentence = sentences[index - 1];
        currentSentence.SetActive(true);
    }

    private void SentenceMoveUp()
    {
    }

    private void SentenceMoveDown()
    {
        
    }
}
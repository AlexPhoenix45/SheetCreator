using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject sentencePrefab;
    [SerializeField] private GameObject contentPos;

    private List<Sentence> sentences = new List<Sentence>();

    private Sentence currentSentence;
    private Note currentNote;

    private void Update()
    {
        if (Input.GetKeyDown(GameData.newSentence))
        {
            CreateSentence();
        }

        if (Input.GetKeyDown(GameData.deleteSentence))
        {
            DeleteSentence();
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
        Debug.Log(sentences.Count);
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
}
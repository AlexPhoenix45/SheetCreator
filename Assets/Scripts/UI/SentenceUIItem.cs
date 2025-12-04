using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentenceUIItem : MonoBehaviour
{
    [SerializeField] private Transform notesContainer;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private TMP_InputField lyric;
    [SerializeField] private Image bgImage;

    private CompleteSentence sentence;
    
    public void SetActive(bool value)
    {
        bgImage.color = value ? GameData.activeColor : GameData.normalColor;
    }

    public void SetValue(CompleteSentence sentence)
    {
        this.sentence = sentence;
        
        lyric.text = sentence.lyric;
        
        foreach (Transform child in notesContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < sentence.notes.Length; i++)
        {
            NoteUIItem tempNoteUIItem = Instantiate(notePrefab, notesContainer).GetComponent<NoteUIItem>();
            tempNoteUIItem.UpdateNote(sentence.notes[i]);
        }
    }

    public CompleteSentence GetSentence()
    {
        return sentence;
    }
}

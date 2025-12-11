using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteUIItem : MonoBehaviour
{
    [SerializeField] private Image bgImage;
    [SerializeField] private TextMeshProUGUI noteText;

    public Action<NoteUIItem> callback = null;
    
    private CompleteNote noteData = new CompleteNote();

    public void UpdateNote(CompleteNote note)
    {
        noteData = note;
        switch (note.note)
        {
            case Notes.A:
                noteText.text = "A" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.ASharp:
                noteText.text = (GameData.isSharpNotes ? "A#" : "Bb") + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.B:
                noteText.text = "B" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.C:
                noteText.text = "C" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.CSharp:
                noteText.text = (GameData.isSharpNotes ? "C#" : "Db") + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.D:
                noteText.text = "D" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.DSharp:
                noteText.text = (GameData.isSharpNotes ? "D#" : "Eb") + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.E:
                noteText.text = "E" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.F:
                noteText.text = "F" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.FSharp:
                noteText.text = (GameData.isSharpNotes ? "F#" : "Gb") + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.G:
                noteText.text = "G" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.GSharp:
                noteText.text = (GameData.isSharpNotes ? "G#" : "Ab") + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.None:
            default:
                noteText.text = "...";
                return;
        }
    }
    
    public void UpdateNote()
    {
        switch (noteData.note)
        {
            case Notes.A:
                noteText.text = "A" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.ASharp:
                noteText.text = (GameData.isSharpNotes ? "A#" : "Bb") + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.B:
                noteText.text = "B" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.C:
                noteText.text = "C" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.CSharp:
                noteText.text = (GameData.isSharpNotes ? "C#" : "Db") + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.D:
                noteText.text = "D" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.DSharp:
                noteText.text = (GameData.isSharpNotes ? "D#" : "Eb") + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.E:
                noteText.text = "E" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.F:
                noteText.text = "F" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.FSharp:
                noteText.text = (GameData.isSharpNotes ? "F#" : "Gb") + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.G:
                noteText.text = "G" + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.GSharp:
                noteText.text = (GameData.isSharpNotes ? "G#" : "Ab") + (noteData.octave == 1 ? "" : noteData.octave.ToString());
                return;
            case Notes.None:
            default:
                noteText.text = "...";
                return;
        }
    }
    
    public void SetActive(bool value)
    {
        if (!bgImage) return;
        bgImage.color = value ? GameData.activeColor : GameData.normalColor;
    }
}

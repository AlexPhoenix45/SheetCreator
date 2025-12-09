using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NoteUIItem : MonoBehaviour
{
    [SerializeField] private Image bgImage;
    [SerializeField] private TextMeshProUGUI noteText;

    public void UpdateNote(CompleteNote note)
    {
        switch (note.note)
        {
            case Notes.A:
                noteText.text = "A" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.ASharp:
                noteText.text = "A#" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.B:
                noteText.text = "B" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.C:
                noteText.text = "C" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.CSharp:
                noteText.text = "C#" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.D:
                noteText.text = "D" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.DSharp:
                noteText.text = "D#" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.E:
                noteText.text = "E" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.F:
                noteText.text = "F" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.FSharp:
                noteText.text = "F#" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.G:
                noteText.text = "G" + (note.octave == 1 ? "" : note.octave.ToString());
                return;
            case Notes.GSharp:
                noteText.text = "G#" + (note.octave == 1 ? "" : note.octave.ToString());
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

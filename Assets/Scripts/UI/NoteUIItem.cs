using TMPro;
using UnityEditor;
using UnityEngine;

public class NoteUIItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;

    public void UpdateNote(CompleteNote note)
    {
        switch (note.note)
        {
            case Notes.A:
                noteText.text = "A" + note.octave;
                return;
            case Notes.ASharp:
                noteText.text = "A#" + note.octave;
                return;
            case Notes.B:
                noteText.text = "B" + note.octave;
                return;
            case Notes.C:
                noteText.text = "C" + note.octave;
                return;
            case Notes.CSharp:
                noteText.text = "C#" + note.octave;
                return;
            case Notes.D:
                noteText.text = "D" + note.octave;
                return;
            case Notes.DSharp:
                noteText.text = "D#" + note.octave;
                return;
            case Notes.E:
                noteText.text = "E" + note.octave;
                return;
            case Notes.F:
                noteText.text = "F#" + note.octave;
                return;
            case Notes.FSharp:
                noteText.text = "F#" + note.octave;
                return;
            case Notes.G:
                noteText.text = "G" + note.octave;
                return;
            case Notes.GSharp:
                noteText.text = "G#" + note.octave;
                return;
            default:
                noteText.text = "...";
                return;
        }
    }
}

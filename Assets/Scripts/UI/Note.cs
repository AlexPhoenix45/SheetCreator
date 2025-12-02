using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;

    public void UpdateNote(string text)
    {
        noteText.text = text;
    }
}

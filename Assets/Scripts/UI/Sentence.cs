using UnityEngine;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Image bgImage;

    public void SetActive(bool value)
    {
        bgImage.color = value ? GameData.activeColor : GameData.normalColor;
    }
    
    public void CreateNote()
    {
        
    }

    public void DeleteNote()
    {
        
    }
}

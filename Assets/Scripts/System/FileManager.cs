using UnityEngine;
using System.IO;
using TMPro; 

public class FileManager : MonoBehaviour
{
    public static FileManager Instance;
    public TMP_InputField fileNameInput; 
    private string fileName = "";
    private string filePath = "";

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("Save file location: " + filePath);
    }

    private void GetFileName()
    {
        if (!fileNameInput)
        {
            fileName = "TemplateSheet.txt";
        }
        else
        {
            fileName = fileNameInput.text + ".txt";
        }
    }

    private void WriteToFile(string content)
    {
        GetFileName();
        try
        {
            File.WriteAllText(filePath, content);
            Debug.Log("Successfully saved data to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }

    public void LoadTextFromFile()
    {
        GetFileName();
        if (File.Exists(filePath))
        {
            var loadedText = File.ReadAllText(filePath);
            Debug.Log("Loaded data: " + loadedText);
            // if (fileNameInput != null)
            // {
            //     fileNameInput.text = loadedText;
            // }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }

    // private void DataToFile()
    // {
    //     var content = "";
    //     foreach (var VARIABLE in GameManager)
    //     {
    //         
    //     }
    // }
}
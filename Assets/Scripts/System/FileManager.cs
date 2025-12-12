using System;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    public static FileManager Instance;
    [SerializeField] private Button readButton;
    [SerializeField] private Button writeJsonButton;
    [SerializeField] private Button writeReadableButton;

    private string filePath = "";

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        
        readButton.onClick.AddListener(OnClick_ReadButton);
        writeJsonButton.onClick.AddListener(OnClick_WriteJsonButton);
        writeReadableButton.onClick.AddListener(OnClick_WriteReadableButton);
    }

    private void GetPath(bool isJson)
    {
        var tempFileName = ContentUIManager.GetFileName();

        if (tempFileName == string.Empty)
        {
            tempFileName = isJson ? "TemplateSheet.json" : "TemplateSheet.txt";
        }
        else
        {
            tempFileName += isJson ? ".json" : ".txt";
        }

        filePath = Path.Combine(Application.persistentDataPath, tempFileName);
    }

    private void OnClick_WriteJsonButton()
    {
        GetPath(true);

        try
        {
            var saveString = JsonUtility.ToJson(GameManager.GetSheet());
            File.WriteAllText(filePath, saveString);
            Debug.Log("Successfully saved data to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }

    private void OnClick_ReadButton()
    {
        GetPath(true);

        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            var tempSheet = JsonUtility.FromJson<CompleteSheet>(jsonString);
            GameManager.RefreshSheet(tempSheet);
            Debug.Log("Successfully loaded data from: " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }

    private void OnClick_WriteReadableButton()
    {
        GetPath(false);

        try
        {
            var saveString = GameManager.SheetToReadable();
            File.WriteAllText(filePath, saveString);
            Debug.Log("Successfully saved data to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }
}
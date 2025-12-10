using System;
using UnityEngine;

public class GameData : MonoBehaviour
{   
    //Mapping
    public static KeyCode changeModeKey = KeyCode.Tab;
    public static KeyCode deleteKey = KeyCode.Delete;
    public static KeyCode upKey = KeyCode.UpArrow;
    public static KeyCode downKey = KeyCode.DownArrow;
    public static KeyCode rightKey = KeyCode.RightArrow;
    public static KeyCode leftKey = KeyCode.LeftArrow;
    public static KeyCode sharpKey = KeyCode.LeftShift;
    public static KeyCode sharpKeyAlt = KeyCode.RightShift;
    public static KeyCode flatKey = KeyCode.LeftControl;
    public static KeyCode flatKeyAlt = KeyCode.RightControl;
    public static KeyCode newKey = KeyCode.Return;
    public static KeyCode editLyricKey = KeyCode.Return;
    public static KeyCode addNewKey = KeyCode.LeftShift;
    public static KeyCode addNewKeyAlt = KeyCode.RightShift;
    public static KeyCode cKey = KeyCode.Alpha1;
    public static KeyCode dKey = KeyCode.Alpha2;
    public static KeyCode eKey = KeyCode.Alpha3;
    public static KeyCode fKey = KeyCode.Alpha4;
    public static KeyCode gKey = KeyCode.Alpha5;
    public static KeyCode aKey = KeyCode.Alpha6;
    public static KeyCode bKey = KeyCode.Alpha7;
    public static KeyCode cKeyAlt = KeyCode.Keypad1;
    public static KeyCode dKeyAlt = KeyCode.Keypad2;
    public static KeyCode eKeyAlt = KeyCode.Keypad3;
    public static KeyCode fKeyAlt = KeyCode.Keypad4;
    public static KeyCode gKeyAlt = KeyCode.Keypad5;
    public static KeyCode aKeyAlt = KeyCode.Keypad6;
    public static KeyCode bKeyAlt = KeyCode.Keypad7;
    public static KeyCode octaveUpKey = KeyCode.UpArrow;
    public static KeyCode octaveDownKey = KeyCode.DownArrow;
    public static KeyCode sharpSwitchKey = KeyCode.F1;
    
    //Color
    public static Color activeColor = new Color(1f, 0.3726415f, 0.3726415f);
    public static Color normalColor = Color.white;

    //Variable
    public static bool editMode = false; //0 = Sentence, 1 = Note
    public static bool isSharpNotes = true;
}

public enum Notes
{
    None,
    C,
    CSharp,
    D,
    DSharp,
    E,
    F,
    FSharp,
    G,
    GSharp,
    A,
    ASharp,
    B,
}

[Serializable]
public class CompleteNote
{
    public Notes note = Notes.None;
    public int octave = 1;
}

[Serializable]
public class CompleteSentence
{
    public CompleteNote[] notes = new []{new CompleteNote()};
    public string lyric = null;
}

[Serializable]
public class CompleteSheet
{
    public CompleteSentence[] sentences = null;
    public string songName = null;
    public Notes key = Notes.None;
}
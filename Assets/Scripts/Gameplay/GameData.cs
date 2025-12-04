using System;
using UnityEngine;

public class GameData : MonoBehaviour
{   
    //Mapping
    public static KeyCode changeModeKey = KeyCode.Tab;
    public static KeyCode deleteKey = KeyCode.Delete;
    public static KeyCode upKey = KeyCode.UpArrow;
    public static KeyCode downKey = KeyCode.DownArrow;
    public static KeyCode sharpKey = KeyCode.LeftShift;
    public static KeyCode newKey = KeyCode.Return;
    
    //Color
    public static Color activeColor = new Color(1f, 0.3726415f, 0.3726415f);
    public static Color normalColor = Color.white;
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
    public int octave = 0;
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
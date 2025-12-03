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
public class Sheet
{
    public Notes note;
    public int[] octave;
}
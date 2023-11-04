using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Selection")]
public class SO_Level : ScriptableObject
{
    public Sprite img_Tile;
    public string levelName;
    public int levelIndex;
    public Color bgColor;
}

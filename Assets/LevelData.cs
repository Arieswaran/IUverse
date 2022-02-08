using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Level[] levels_data; //not using for now
}

[System.Serializable]
public class Level
{
    public Sprite[] current_level_sprites;
    public int level;
    public string quote;
    public string author;
}
[System.Serializable]
public class Points
{
    public int x, y;
    public Points(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

}
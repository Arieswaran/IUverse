using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGridController : MonoBehaviour
{
    public static int array_size_x = 3;
    public static int array_size_y = 3;
    public TileController tile_prefab;
    public TileController[,] mytiles = new TileController[array_size_x, array_size_y];
    public WinPopupController win_popup_controller;

    public Level[] levels;
    public Transform grid_layout;
    public Text level_text;
    public Button reset_button, close_button;
    public int current_level = 0;
    private List<Points> points;
    private Points already_selected_point = null;
    private void Start()
    {
        //destroyLevel();
        close_button.onClick.RemoveAllListeners();
        close_button.onClick.AddListener(delegate ()
        {
            transform.parent.gameObject.SetActive(false);
        });
    }
    private void OnEnable()
    {
        //init();
    }

    private void OnDisable()
    {
        destroyLevel();
    }

    public void init()
    {
        points = new List<Points>();
        int index = 0;
        for (int i = 0; i < array_size_x; i++)
        {
            for (int j = 0; j < array_size_y; j++)
            {
                TileController tile = Instantiate(tile_prefab, new Vector2(i, j), Quaternion.identity, grid_layout);
                tile.Init(i, j, levels[current_level % levels.Length].current_level_sprites[index], index, tileClickFunc);
                index += 1;
                mytiles[i, j] = tile;
                points.Add(new Points(i, j));
            }
        }
        //randomise();

        //randomize the start
        //check win condition if yes, randomize again
        for (int i = 0; i <= current_level && points.Count > 1; i++)
        {
            randomiseV2();

        }
        if (level_text != null)
        {
            level_text.text = (current_level + 1).ToString();
        }
        if (reset_button != null)
        {
            reset_button.onClick.RemoveAllListeners();
            reset_button.onClick.AddListener(resetToZero);
        }
        Debug.Log("Game Loaded Successfully");
    }

    private void resetToZero()
    {
        current_level = 0;
        PlayerPrefs.SetInt("Current_level", current_level);
        destroyLevel();
        init();
    }
    private bool checkIfWin()
    {
        int actual_index = 0;
        for (int i = 0; i < array_size_x; i++)
        {
            for (int j = 0; j < array_size_y; j++)
            {
                if (mytiles[i, j].index != actual_index)
                {
                    return false;
                }
                else
                {
                    actual_index++;
                }
            }
        }
        return true; ;
    }

    private int getDx(int x, int y)
    {
        //check right
        if (x < array_size_x - 1 && mytiles[x + 1, y].isEmpty())
        {
            return 1;
        }
        //check left 
        if (x > 0 && mytiles[x - 1, y].isEmpty())
        {
            return -1;
        }
        return 0;
    }

    private int getDy(int x, int y)
    {
        //check bottom
        if (y < array_size_y - 1 && mytiles[x, y + 1].isEmpty())
        {
            return 1;
        }
        //check top
        if (y > 0 && mytiles[x, y - 1].isEmpty())
        {
            return -1;
        }
        return 0;
    }

    private void tileClickFunc(int x, int y)
    {
        if (already_selected_point == null)
        {
            already_selected_point = new Points(x, y);
            //highlight that tile?
            return;
        }
        swapTiles(x, y, already_selected_point.x, already_selected_point.y);
        already_selected_point = null;
        if (checkIfWin())
        {
            win_popup_controller.gameObject.SetActive(true);
        }
    }

    private void swapFunc(int x1, int y1)
    {
        int dx = getDx(x1, y1);
        int dy = getDy(x1, y1);
        if (dx == 0 && dy == 0)
        {
            Debug.Log("No change");
            return;
        }
        swapTiles(x1, y1, x1 + dx, y1 + dy);
        if (checkIfWin())
        {
            win_popup_controller.gameObject.SetActive(true);
        }
    }

    private void swapTiles(int x1, int y1, int x2, int y2)
    {
        //delay it by 1 second?
        TileController from_tile = mytiles[x1, y1];
        TileController to_tile = mytiles[x2, y2];

        int from_tile_index = from_tile.getIndex();
        from_tile.transform.SetSiblingIndex(to_tile.getIndex());
        to_tile.transform.SetSiblingIndex(from_tile_index);
        mytiles[x1, y1] = to_tile;
        mytiles[x2, y2] = from_tile;

        to_tile.setPosition(x1, y1);
        from_tile.setPosition(x2, y2);
        to_tile.highlighTile(false);
        from_tile.highlighTile(false);
    }

    private void swapTiles(Points a, Points b)
    {
        swapTiles(a.x, a.y, b.x, b.y);
    }
    private void randomise(int difficulty = 1) //1 - easy ,2 - medium ,3 - hard
    {
        int x1 = UnityEngine.Random.Range(0, array_size_x - 1);
        int y1 = UnityEngine.Random.Range(0, array_size_y - 1);

        int x2 = UnityEngine.Random.Range(0, array_size_x - 1);
        int y2 = UnityEngine.Random.Range(0, array_size_y - 1);

        swapTiles(x1, y1, x2, y2);
    }

    private void randomiseV2()
    {
        if (points != null && points.Count > 1)
        {
            int index = Random.Range(0, points.Count);
            Points a = points[index];
            points.RemoveAt(index);
            index = Random.Range(0, points.Count);
            Points b = points[index];
            points.RemoveAt(index);
            swapTiles(a, b);
        }
    }

    private void createPoints()
    {
        points = new List<Points>();
        points.Add(new Points(0, 0));
        points.Add(new Points(0, 1));
        points.Add(new Points(0, 2));
        points.Add(new Points(1, 0));
        points.Add(new Points(1, 1));
        points.Add(new Points(1, 2));
        points.Add(new Points(2, 0));
        points.Add(new Points(2, 1));
        points.Add(new Points(2, 2));
    }
    public void destroyLevel()
    {
        foreach (Transform t in grid_layout.transform)
        {
            {
                Destroy(t.gameObject);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TileController : MonoBehaviour
{
    public Button tile_button;
    public Image tile_image;
    private int x, y;
    public int index = -1;
    public Outline tile_highlight_outline;

    private Action<int, int> swapFunc = null;
    private void Start()
    {
        if (tile_button == null)
        {
            tile_button = gameObject.GetComponent<Button>();
        }
        if (tile_image == null)
        {
            tile_image = gameObject.GetComponent<Image>();
        }
        if (tile_highlight_outline == null)
        {
            tile_highlight_outline = gameObject.GetComponent<Outline>(); //why it does not work?
        }
        gameObject.name = "tile_" + index;
        setButtonClick();
    }

    private void setButtonClick()
    {
        tile_button.onClick.RemoveAllListeners();
        tile_button.onClick.AddListener(delegate ()
        {
            highlighTile(true);
            if (swapFunc != null)
            {
                swapFunc.Invoke(this.x, this.y);
            }
        });
    }

    public bool isEmpty()
    {
        return index == 8; //9th cell is empty
    }

    public void setSprite(Sprite sprite)
    {
        tile_image.sprite = sprite;
    }

    public void setPosition(int i, int j)
    {
        this.x = i;
        this.y = j;
    }

    public void Init(int i, int j, Sprite sprite, int index, Action<int, int> swapFunc)
    {
        setPosition(i, j);
        setSprite(sprite);
        this.index = index;
        this.swapFunc = swapFunc;
        highlighTile(false);
    }

    public void highlighTile(bool doHighlight = true)
    {
        tile_highlight_outline.enabled = doHighlight;
    }

    public int getIndex()
    {
        return transform.GetSiblingIndex();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementRowController : MonoBehaviour
{
    public Text text;
    public void setText(string wish)
    {
        text.text = wish;
    }
}

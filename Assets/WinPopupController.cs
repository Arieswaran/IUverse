using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WinPopupController : MonoBehaviour
{
    public Button Continue_button;
    public Text win_text;
    public List<String> wishes;
    private void OnEnable()
    {
        int current_level = PlayerPrefs.GetInt("Current_level", 0);
        Debug.Log("Current Level : " + current_level);
        current_level = current_level % wishes.Count; //i am genius
        Debug.Log("Current Level : " + current_level);
        if (current_level < wishes.Count)
        {
            win_text.text = wishes[current_level];
        }
    }

    public void setContinueButton(Action callback = null)
    {
        Continue_button.onClick.RemoveAllListeners();
        Continue_button.onClick.AddListener(delegate ()
        {
            gameObject.SetActive(false);
            if (callback != null)
            {
                callback.Invoke();
            }
        });
    }

}

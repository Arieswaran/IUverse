using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WinPopupController : MonoBehaviour
{
    public Button Continue_button;
    public Text win_text,author_text;
    public List<String> wishes;
    private bool isWin = false;
    private void OnEnable()
    {
        // int current_level = PlayerPrefs.GetInt("Current_level", 0);
        // Debug.Log("Current Level : " + current_level);
        // current_level = current_level % wishes.Count; //i am genius
        // Debug.Log("Current Level : " + current_level);
        // if (current_level < wishes.Count)
        // {
        //     win_text.text = wishes[current_level];
        // }
    }

    public void renderWinPopup(int level,int max_level){
        win_text.text = "Congratulations!";//"'"+level.quote+"'";
        //author_text.text = "- "+level.author.ToUpper();
        if(level == max_level){
            win_text.text = "Thank you for playing my game! Now let's see how you do in difficulty mode";
        }
        isWin = true;
        gameObject.SetActive(true);
    }

    public void renderInfoPopup() {
        win_text.text = "Click on two images to swap them. Complete the image to proceed to next level";
        gameObject.SetActive(true);
        isWin = false;
    }

    public void setContinueButton(Action callback = null)
    {
        Continue_button.onClick.RemoveAllListeners();
        Continue_button.onClick.AddListener(delegate ()
        {
            gameObject.SetActive(false);
            if (callback != null && isWin)
            {
                callback.Invoke();
            }
        });
    }

}

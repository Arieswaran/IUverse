using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text timer_text,level;
    private float level_timer = 5;
    private string text_color = "white";
    private void Update() {
        if(GameManager.instance.getGameState() == STATE.PLAYING){
            level_timer -= Time.deltaTime;
            if(level_timer <= 0){
                level_timer = 0;
            }
            if(level_timer < 10){
                text_color = "red";
            }else{
                text_color = "white";
            }
            timer_text.text = "Time Left : <color="+text_color+">"+Mathf.Round(level_timer).ToString()+"</color>";
        }
    }

    public void resetTimer(){
        level_timer = 5;
    }


    public float getTimeleft(){
        return level_timer;
    }

    public void renderUI(int current_level){
        level.text = (current_level + 1).ToString();
        level_timer = GameManager.instance.getTimerBasedOnLevel(current_level);
    }

}
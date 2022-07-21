using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LevelGridController level_controller;
    public WinPopupController win_popup_controller;
    public AchievementController achievement_controller;

    public Button play_button, achievements_button, exit_button;
    public GameObject MainMenu, InGame, Achievements;

    public Image Background;
    public Sprite menu_bg, game_bg;

    private int max_levels = 13; //0,1,2

    public static GameManager instance;

   

    private STATE game_state = STATE.PAUSED;
    
    private void Awake() {
        if(instance!= null){
            Debug.LogError("Game manager instance already exists");
        }else{
            instance = this;
        }
    }
    private void Start()
    {
        init();
        setButtons();
        //if unity editor,then enable debug logs,else disable the debug logs
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

    }

    private void init()
    {
        Background.sprite = menu_bg;
        level_controller.gameObject.SetActive(false);
        win_popup_controller.gameObject.SetActive(false);
        win_popup_controller.setContinueButton(delegate ()
        {
            // if (level_controller.current_level < max_levels)
            // {
            level_controller.current_level += 1;
            //}
            setCurrentLevel(level_controller.current_level);
            level_controller.resetLevel();
        });
        MainMenu.SetActive(true);
        InGame.SetActive(false);
        Achievements.SetActive(false);
    }


    private void setButtons() //add button click sounds and animations if you can
    {
        play_button.onClick.RemoveAllListeners();
        play_button.onClick.AddListener(delegate ()
        {
            //Background.sprite = game_bg;
            InGame.SetActive(true);
            level_controller.current_level = PlayerPrefs.GetInt("Current_level", 0);
            level_controller.gameObject.SetActive(true);
            level_controller.init();
            //MainMenu.SetActive(false);
        });
        exit_button.onClick.RemoveAllListeners();
        exit_button.onClick.AddListener(delegate ()
        {
            Application.Quit();
        });
        achievements_button.onClick.RemoveAllListeners();
        achievements_button.onClick.AddListener(delegate ()
        {
            achievement_controller.gameObject.SetActive(true);
        });
    }

    public void setCurrentLevel(int level){
        PlayerPrefs.SetInt("Current_level", level);        
    }

    public int getScore(){
        return PlayerPrefs.GetInt("score",0);
    }

    public void setScore(int score){
        PlayerPrefs.SetInt("score",score);
    }

    public int getTimerBasedOnLevel(int level){
        int timer = 60;
        switch(difficultyLevel()){
            case 0: timer = 60;
            break;
            case 1: timer = 30;
            break;
            case 2: timer = 15;
            break;
            default: timer = 15 - difficultyLevel();
            break;
        }
        if(timer < 3){ //giving minimum 3 seconds whatever the difficulty is
            timer = 3;
        }
        return timer;
    }

    public void gameStarted(){
        game_state = STATE.PLAYING;
    }

    public void gameWon(){
        game_state = STATE.WON;
    }

    public void gameLost(){
        game_state = STATE.LOST;
    }

    public STATE getGameState(){
        return game_state;
    }

    public int difficultyLevel(){
        int current_level = level_controller.current_level;
        int max_level = level_controller.levels.Length;
        if(current_level == 0){
            return 0;
        }
        return current_level/max_level;
    }

    public int getRandomiseCount(){
        Debug.Log("difficulty level : "+difficultyLevel());
        if(difficultyLevel() == 0){
            if(level_controller.current_level >3){
                return 3;
            }
            return level_controller.current_level;
        }
        return difficultyLevel() * 2+ 3;
    }
}

 public enum STATE
{
    PLAYING,WON,LOST, PAUSED
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    public Transform achievement_row_prefab;
    public Transform row_panel;
    public List<string> wishes;
    public Button close_button;

    private void Start()
    {
        close_button.onClick.RemoveAllListeners();
        close_button.onClick.AddListener(delegate ()
        {
            gameObject.SetActive(false);
        });
    }
    private void OnEnable()
    {
        // int current_level = PlayerPrefs.GetInt("Current_level", 0);
        // if (current_level > 17)
        // {
        //     current_level = 17;
        // }
        // for (int i = 0; i < current_level; i++)
        // {
        //     Transform row = Instantiate(achievement_row_prefab, row_panel);
        //     row.GetComponent<AchievementRowController>().setText(wishes[i]);
        // }
    }
    private void OnDisable()
    {
        // foreach (Transform t in row_panel)
        // {
        //     {
        //         Destroy(t.gameObject);
        //     }
        // }
    }
}

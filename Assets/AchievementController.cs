using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    public Transform collection_image_prefab;
    public Transform image_content;
    public List<Texture> image_textures;
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
        int current_level = PlayerPrefs.GetInt("Current_level", 0);
        if (current_level > 17)
        {
            current_level = 17;
        }
        for (int i = 0;  i < image_textures.Count; i++)
        {
            Transform row = Instantiate(collection_image_prefab, image_content);
            row.GetComponent<CollectionImageController>().renderImage(image_textures[i],i < current_level -1);
        }
    }
    private void OnDisable()
    {
        foreach (Transform t in image_content)
        {
            {
                Destroy(t.gameObject);
            }
        }
    }
}

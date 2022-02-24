using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectionImageController : MonoBehaviour
{
    //public Image lock_icon;
    public Transform blackoverlay;
    public RawImage unlocked_image;

    public void renderImage(Texture image,bool is_unlocked){
        unlocked_image.texture = image;
        blackoverlay.gameObject.SetActive(!is_unlocked);
    }
}

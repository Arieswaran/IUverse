using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip click_clip,win_clip;
    public float volume=0.5f;
    public static AudioManager instance;
    private void Awake() {
        if(instance != null){
            return;
        }
        instance = this;
    }
    public void playClickSound()
    {
        audioSource.PlayOneShot(click_clip, volume);
    }

    public void playWinSound()
    {
        audioSource.PlayOneShot(win_clip, volume);
    }
}
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip click_clip,win_clip,bg_music;
    public float volume=1.5f;
    public static AudioManager instance;
    private void Awake() {
        if(instance != null){
            return;
        }
        instance = this;
        playBgMusic();
    }

    public void playBgMusic(){
        audioSource.loop = true;
        audioSource.clip = bg_music;
        audioSource.Play();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource backgroundmusic;

    void Start()
    {
        backgroundmusic = GetComponent<AudioSource>();
        backgroundmusic.volume = 0;
        backgroundmusic.PlayDelayed(0.1f);
        FadeIn(backgroundmusic);
    }

    public IEnumerator HubtoDungeon() {
        return FadeOut(backgroundmusic);
    }
    /*
    public IEnumerator DungeontoHub() {
    }*/

    private IEnumerator FadeOut(AudioSource music) {
        float speed = 0.05f;
        while(music.volume > 0) {
            music.volume -= speed;
            yield return new WaitForSeconds(0.05f);
            if (music.volume < 0.3) {
                music.Stop();
                break;
            }
        }
    }
       private IEnumerator FadeIn(AudioSource music) {
        float speed = 0.05f;
        while(music.volume < 1) {
            music.volume += speed;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

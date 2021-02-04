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
        StartCoroutine(FadeIn(backgroundmusic));
    }

    public void HubtoDungeon() {
        StartCoroutine(FadeOut(backgroundmusic));
    }
    /*
    public IEnumerator DungeontoHub() {
    }*/

    private IEnumerator FadeOut(AudioSource music) {
        float speed = 0.005f;
        while(music.volume > 0) {
            music.volume -= speed;
            yield return new WaitForSeconds(0.1f);
            if (music.volume < 0.05) {
                music.Stop();
                break;
            }
        }
    }
       private IEnumerator FadeIn(AudioSource music) {
        float speed = 0.05f;
        while(music.volume < 0.25) {
            music.volume += speed;
            yield return new WaitForSeconds(0.05f);
        }
    }
}

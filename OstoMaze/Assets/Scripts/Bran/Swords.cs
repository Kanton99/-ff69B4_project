using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public AudioSource[] swords;
    public Animator player_anim;

    // Start is called before the first frame update
    void Start()
    {
        swords = GetComponents<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        coll.gameObject.GetComponent<MobController>().TakeDamage(3);
    }

    public void Attack() {
        bool playing = false;
        foreach(AudioSource sound in swords) {
            if(sound.isPlaying)
                playing = true;
        }
        if(!playing) swords[Random.Range(0, swords.Length)].Play();
        player_anim.SetTrigger("swing");
    }
}

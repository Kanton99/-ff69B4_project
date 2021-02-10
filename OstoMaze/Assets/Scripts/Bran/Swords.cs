using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public AudioSource[] swords;
    public Animator player_anim;
    public MainController player;

    // Start is called before the first frame update
    void Start()
    {
        swords = GetComponents<AudioSource>();
    }

    public void Attack() {
        player_anim.SetTrigger("swing");
    }

    void Update() {
        if (player.finished_animation) {  // if sword swinging animation is finished
            swords[Random.Range(0, 3)].Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public AudioSource[] swords;

    // Start is called before the first frame update
    void Start()
    {
        swords = GetComponents<AudioSource>();
    }

    public void Attack() {
        swords[Random.Range(0, 3)].Play();
    }
}

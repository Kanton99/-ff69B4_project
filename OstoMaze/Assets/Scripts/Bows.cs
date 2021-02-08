using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bows : MonoBehaviour
{
    public AudioSource bow;

    // Start is called before the first frame update
    void Start()
    {
        bow = GetComponent<AudioSource>();
    }

    public void Attack() {
        bow.Play();
    }
}

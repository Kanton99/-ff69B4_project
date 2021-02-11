using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public float TIMER;
    public float RADIUS;
    public float VELOCITY;

    public int hp;
    public int bs; // Ostomy bar
    public float player_velocity;
    public bool is_cooked;

    public AudioSource eating_sound;
    private bool collected = false;

    private float _timer;
    private MainController _player;

    public void Drop() {
        _timer = TIMER;
        _player = GameObject.FindWithTag("Player").GetComponent<MainController>();
    }

    void collect() {
        _player.AddHealth(this.hp);
        _player.bs += this.bs/50;
        eating_sound.Play();
        collected = true;
    }
        
    void Update()
    {

        float dist = Vector3.Distance(_player.gameObject.transform.position, this.transform.position);

        if(dist < RADIUS) {
            float step = VELOCITY * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, _player.gameObject.transform.position, step);
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0) Destroy(this.gameObject);
        if (!eating_sound.isPlaying && collected) Destroy(this.gameObject);

        if (dist < 0.5f && !collected) {
            collect();
        }
    }
}

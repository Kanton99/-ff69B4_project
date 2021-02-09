using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource[] impactsounds;
    private int size;
    private int index;
    private SpriteRenderer _sprite;
    private Vector3 _direction;
    private bool is_playing;
    private bool has_hit = false;

    public float VELOCITY;
    public float DIST_MOVEMENT;
    public float timer;

    private Vector3 _offset;
    private Vector3[] _directions = new Vector3[4];
    private Vector3 _curr_direction = Vector3.zero;

    public void Shoot(Vector3 curr_dir, Vector3 direction) {
        _rb = GetComponent<Rigidbody2D>();
        _directions[0] = new Vector3(0, -1, 0);
        _directions[1] = new Vector3(0, 1, 0);
        _directions[2] = new Vector3(-1, 0, 0);
        _directions[3] = new Vector3(1, 0, 0);
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        impactsounds = GetComponents<AudioSource>();
        _curr_direction = curr_dir; // Position MOB
        _direction = direction;  // Position dir
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        has_hit = true;
        index = Random.RandomRange(0, impactsounds.Length);
        impactsounds[index].Play();
        _sprite.enabled = false;
    }

    void Update() {
        _rb.velocity = (_direction - _curr_direction).normalized * VELOCITY;
        if (timer < 0) Destroy(this.gameObject);
        timer -= Time.deltaTime;
        if (!impactsounds[index].isPlaying && has_hit) Destroy(this.gameObject);
    }
}

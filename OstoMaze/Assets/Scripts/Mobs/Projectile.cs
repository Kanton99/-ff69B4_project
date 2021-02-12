using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource[] impactsounds;
    private int index;
    private SpriteRenderer _sprite;
    private Vector3 _direction;
    private bool is_playing;
    private bool has_hit = false;

    public float VELOCITY;
    public float timer;

    private Vector3 _offset;
    private Vector3 _curr_direction = Vector3.zero;

    public void Shoot(Vector3 spawn, Vector3 aim) {
        _rb = GetComponent<Rigidbody2D>();
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        impactsounds = GetComponents<AudioSource>();
        transform.position = spawn; // spawn position
        _direction = aim;  // aim position
    }

    public void Shoot(Vector3 aim) {
        _rb = GetComponent<Rigidbody2D>();
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        impactsounds = GetComponents<AudioSource>();
        _direction = aim;  // aim position
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "MainHurtBox") {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<MainController>().TakeDamage(1);
        }
        has_hit = true;
        index = Random.Range(0, impactsounds.Length);
        impactsounds[index].Play();
        _sprite.enabled = false;
    }

    void Update() {
        _rb.velocity = _direction * VELOCITY;
        if (timer < 0) Destroy(this.gameObject);
        timer -= Time.deltaTime;
        if (!impactsounds[index].isPlaying && has_hit) Destroy(this.gameObject);
    }
}

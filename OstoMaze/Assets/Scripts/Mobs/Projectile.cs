using System;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource[] impactsounds;
    private SpriteRenderer _sprite;
    private Vector3 _direction;

    public float VELOCITY;
    public float timer;

    private Vector3 _offset;
    private Vector3 _curr_direction = Vector3.zero;

    public void Shoot(Vector3 position, Vector3 direction) {
        _rb = GetComponent<Rigidbody2D>();
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        impactsounds = GetComponents<AudioSource>();
        this.transform.position = position; // Position MOB
        _direction = direction;  // Position dir
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(this.gameObject);
    }

    void Update() {
        _rb.velocity = _direction * VELOCITY;
        if (timer < 0) Destroy(this.gameObject);
        timer -= Time.deltaTime;
    }
}

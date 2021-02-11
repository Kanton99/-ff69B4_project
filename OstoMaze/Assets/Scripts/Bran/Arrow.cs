using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rb;
    public AudioSource impactsound;
    private int index;
    private SpriteRenderer _sprite;
    private Vector3 _direction;
    private bool has_hit = false;

    public float VELOCITY;
    public float timer;

    public void Shoot(Vector3 spawn, Vector3 aim)
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        transform.position = spawn; // spawn position
        _direction = aim;  // aim position
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        has_hit = true;
        impactsound.Play();
        _sprite.enabled = false;
    }

    void Update()
    {
        _rb.velocity = (_direction - transform.position).normalized * VELOCITY;
        if (timer < 0) Destroy(this.gameObject);
        timer -= Time.deltaTime;
        if (!impactsound.isPlaying && has_hit) Destroy(this.gameObject);
    }
}

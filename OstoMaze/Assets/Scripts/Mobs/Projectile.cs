using System;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource[] impactsounds;
    private SpriteRenderer _sprite;
    private Vector3 _direction;

    public float VELOCITY;
    public float DIST_MOVEMENT;

    private Vector3 _offset;
    private Vector3[] _directions = new Vector3[4];
    private Vector3 _curr_direction = Vector3.zero;

    /*
    public Projectile() {}

    public Projectile(MobController mob, Vector3 direction) {
        _direction = direction;
        transform.position = mob.transform.position;
        Initialize();
    }
    
    public void Dispose() {
        // perform clean up
        // tell the GC not to finailze
        GC.SuppressFinalize(this);
    }
    */

    public void Shoot(Vector3 curr_dir, Vector3 direction) {
        _curr_direction = curr_dir;
        _direction = direction;
        //Vector2 dir = new Vector2(_direction.x, _direction.y);
        //_rb.AddForce(dir * 20f);
    }

    public void Initialize() {
        _rb = GetComponent<Rigidbody2D>();
        _directions[0] = new Vector3(0, -1, 0);
        _directions[1] = new Vector3(0, 1, 0);
        _directions[2] = new Vector3(-1, 0, 0);
        _directions[3] = new Vector3(1, 0, 0);
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        impactsounds = GetComponents<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        /*
            print("new collision\n");
            VELOCITY = 0f;
        */
    }
    
    void Update() {
        Physics2D.Raycast(transform.position + _offset, _direction, DIST_MOVEMENT);
        _rb.velocity = _curr_direction * VELOCITY;
        //this.Dispose();
    }
}

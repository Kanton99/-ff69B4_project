using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeController : MonoBehaviour
{
    public float TIMER;
    public string char_name;
    public float _timer;

    public Animator mob_anim;
    public UnityEvent dying_event;

    private Vector3 _offset;

    private Rigidbody2D _rb;
    private GameObject _player;
    private AudioSource[] _attacksounds;
    private SpriteRenderer _sprite;
    private Projectile _projectile;

    [SerializeField]
    private int _hp = 0;

    public enum State {SPAWNING, IDLE, MOVING, ATTACKING, DYING};
    public State _curr_state;

    public GameObject getPlayer() {
        return _player;
    }

    private void Attack()
    {
        switch(Random.Range(0, 2)) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
        _attacksounds[Random.Range(0, _attacksounds.Length)].Play();
        Vector3 spawn = this.transform.position - new Vector3(0, 0.5f, 0);  // mob position - offset
        Projectile projectile = Instantiate(this._projectile, spawn, new Quaternion(0,0,0,0));
        projectile.transform.parent = null;
        projectile.Shoot(spawn, _player.transform.position);
    }

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        _attacksounds = GetComponents<AudioSource>();
        _projectile = Resources.Load<Projectile>("Projectile");

        _curr_state = State.IDLE;
    }

    public void takeDamage(int damage) {
        this._hp -= damage;
        if(this._hp <= 0) {
            dying_event.Invoke();
        }
    }

    // Update is called once per frame
    void Update() {
        switch(_curr_state) {
            case State.SPAWNING:
                break;
            case State.IDLE:
                    mob_anim.SetBool("attack", false);
                break;
            case State.ATTACKING:
                if(Vector3.Dot(_player.transform.position - transform.position, transform.right) > 0)
                    _sprite.flipX = true;
                if(Vector3.Dot( _player.transform.position - transform.position, transform.right) < 0)
                    _sprite.flipX = false;
                break;
        }
    }
}

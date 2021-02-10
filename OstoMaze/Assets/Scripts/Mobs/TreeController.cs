using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public float TIMER;
    public string char_name;
    public float _timer;

    public Animator mob_anim;

    private Vector3 _offset;

    private Rigidbody2D _rb;
    private GameObject _player;
    private AudioSource[] attacksounds;
    private SpriteRenderer _sprite;
    private Projectile _projectile;

    public enum State {IDLE, MOVING, ATTACKING};
    public State _curr_state;

    public GameObject getPlayer() {
        return _player;
    }

    public void enterRange(GameObject player) {
        mob_anim.SetBool("attack", true);
        mob_anim.SetBool("walk", false);
        _player = player;
        _rb.velocity = Vector3.zero;
        _curr_state = State.ATTACKING;
    }

    public void leaveRange() {
        mob_anim.SetBool("attack", false);
        mob_anim.SetBool("walk", false);
        _curr_state = State.IDLE;
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
        attacksounds[Random.Range(0, 2)].Play();
        Vector3 spawn = this.transform.position - new Vector3(0, 0.5f, 0);  // mob position - offset
        Projectile projectile = Instantiate(this._projectile, spawn, new Quaternion(0,0,0,0));
        projectile.transform.parent = null;
        projectile.Shoot(spawn, _player.transform.position);
    }

    private void Shoot(Vector3 direction) {
        attacksounds[Random.Range(0, 2)].Play();
        Vector3 spawn = this.transform.position - new Vector3(0, 0.5f, 0);  // mob position - offset
        Projectile projectile = Instantiate(this._projectile, spawn, new Quaternion(0,0,0,0));
        projectile.transform.parent = null;
        projectile.Shoot(spawn, _player.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        attacksounds = GetComponents<AudioSource>();
        _curr_state = State.IDLE;
        _projectile = Resources.Load<Projectile>("Projectile");
    }

    // Update is called once per frame
    void Update() {
        switch(_curr_state) {
            case State.IDLE:
                    mob_anim.SetBool("attack", false);
                break;
            case State.MOVING:
                _timer -= Time.deltaTime;
                if(_timer < 0) {
                    _rb.velocity = Vector3.zero;
                    _timer = TIMER;
                    mob_anim.SetBool("walk", false);
                    mob_anim.SetBool("attack", false);
                    _curr_state = State.IDLE;
                }
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

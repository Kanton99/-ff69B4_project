using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float TIMER;
    public float VELOCITY; // Velocita di movimento
    public float DIST_MOVEMENT;
    public string char_name;
    public float _timer;

    public Animator mob_anim;

    private Vector3 _offset;
    private Vector3[] _directions = new Vector3[4];
    private Vector3 _curr_direction = Vector3.zero;

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

    private Vector3 first_available_direction() {
        foreach(Vector3 dir in _directions) {
            Debug.DrawRay(transform.position + _offset, dir * DIST_MOVEMENT, Color.red, 2);
            if (!Physics2D.Raycast(transform.position + _offset, dir, DIST_MOVEMENT)) {
                Debug.DrawRay(transform.position + _offset, dir * DIST_MOVEMENT, Color.green, 2, false);
                return dir;
            }
        }
        return Vector3.zero;
    }

    private void shuffle(Vector3[] array) {
        for(int i = 0; i < array.Length; i++) {
        int ridx = Random.Range(0, array.Length-1);
        Vector3 buff = array[i];
        array[i] = array[ridx];
        array[ridx] = buff;
        }
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
        attacksounds[Random.RandomRange(0, 2)].Play();
    //    Quaternion rotation;
        Projectile projectile = Instantiate(this._projectile, this.transform.position, new Quaternion(0,0,0,0));
        projectile.transform.parent = null;
        projectile.Initialize();
        projectile.Shoot(this.transform.position, _player.transform.position);
    }

    private void Move()
    {
        _rb.velocity = _curr_direction * VELOCITY;
        if (Vector3.Dot(_curr_direction, transform.right) < 0)
            _sprite.flipX = true;
        if (Vector3.Dot(_curr_direction, transform.right) > 0)
            _sprite.flipX = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _directions[0] = new Vector3(0, -1, 0);
        _directions[1] = new Vector3(0, 1, 0);
        _directions[2] = new Vector3(-1, 0, 0);
        _directions[3] = new Vector3(1, 0, 0);
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
                _timer -= Time.deltaTime;
                if(_timer < 0) {
                    shuffle(_directions);
                    _curr_direction = first_available_direction();
                    if(_curr_direction == Vector3.zero){
                        _timer = TIMER;
                        break;
                    }
                    _curr_state = State.MOVING;
                    _timer = DIST_MOVEMENT / VELOCITY;
                    mob_anim.SetBool("walk", true);
                    mob_anim.SetBool("attack", false);
                }
                break;
            case State.MOVING:
                _timer -= Time.deltaTime;
                Move();
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

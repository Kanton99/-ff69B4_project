using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float TIMER;
    public float VELOCITY; // Velocita di movimento
    public float DIST_MOVEMENT;
    public Animator char_anim;
    public Animator cloud_anim;

    public float _timer;
    private Vector3 _offset;
    private Vector3[] _directions = new Vector3[4];

    public enum State {IDLE, MOVING, SUGG_DIALOG};
    public State _curr_state;

    private Vector3 _curr_direction = Vector3.zero;
    private Rigidbody2D _rb;
    private GameObject _player;

    public AudioSource[] voices = new AudioSource[4];

    private SpriteRenderer _sprite;
    private string player_tag = "Player";


    void shuffle(Vector3[] array) {
      for(int i = 0; i < array.Length; i++) {
        int ridx = Random.Range(0, array.Length-1);
        Vector3 buff = array[i];
        array[i] = array[ridx];
        array[ridx] = buff;
      }
    }

    Vector3 first_available_direction() {
        foreach(Vector3 dir in _directions) {
            Debug.DrawRay(transform.position + _offset + dir * 0.8f, dir * DIST_MOVEMENT, Color.red, 2);
            if (!Physics2D.Raycast(transform.position + _offset+ dir * 0.8f, dir, DIST_MOVEMENT)) {
                Debug.DrawRay(transform.position + _offset + dir * 0.8f, dir * DIST_MOVEMENT, Color.green, 2, false);
                return dir;
            }
        }
        return Vector3.zero;
    }

    void move() {
        _rb.velocity = _curr_direction * VELOCITY;
        if(Vector3.Dot(_curr_direction, transform.right) < 0)
            _sprite.flipX = true;
        if(Vector3.Dot(_curr_direction, transform.right) > 0)
            _sprite.flipX = false;
    }

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();

        _timer = TIMER + Random.Range(0, 3);
        _directions[0] = new Vector3(0, -1, 0);
        _directions[1] = new Vector3(0, 1, 0);
        _directions[2] = new Vector3(-1, 0, 0);
        _directions[3] = new Vector3(1, 0, 0);
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _curr_state = State.IDLE;
        voices = GetComponents<AudioSource>();
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
                _timer = DIST_MOVEMENT / VELOCITY;
                char_anim.SetBool("Walk", true);
                _curr_state = State.MOVING;
            }
            break;
        case State.MOVING:
            _timer -= Time.deltaTime;
            move();
            if(_timer < 0) {
                _rb.velocity = Vector3.zero;
                _timer = TIMER;
                char_anim.SetBool("Walk", false);
                _curr_state = State.IDLE;
            }
            break;
        case State.SUGG_DIALOG:
            if(Vector3.Dot(transform.position - _player.transform.position, transform.right) > 0)
                _sprite.flipX = true;
            if(Vector3.Dot(transform.position - _player.transform.position, transform.right) < 0)
                _sprite.flipX = false;
            break;
      }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.tag == player_tag) {
            char_anim.SetBool("Walk", false);
            _rb.velocity = Vector3.zero;
            cloud_anim.SetBool("Visible", true);
            _player = coll.gameObject;
            _curr_state = State.SUGG_DIALOG;
             voices[0].Play();
            for (int i = 1; i < 3; i++) {
                var v = Random.Range(1,3);
                voices[v].PlayDelayed(0.25f);
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll) {
        if(coll.tag == player_tag) {
            cloud_anim.SetBool("Visible", false);
            _curr_state = State.IDLE;
        }

    }
}

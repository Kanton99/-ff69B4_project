using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeController : MonoBehaviour, IEnemy
{
    public float TIMER;
    public string char_name;
    private float _timer;

    public Animator animator;
    public UnityEvent dead_event;

    private Vector3 _offset;

    private Rigidbody2D _rb;
    private GameObject _player;
    private AudioSource[] _attacksounds;
    private SpriteRenderer _sprite;
    private Projectile _projectile;

    [SerializeField]
    private float _hp = 0;
    [SerializeField]
    private Transform bullet_spawn;


    private Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    private void Die() {
        dead_event.Invoke();
        gameObject.SetActive(false);
    }

    private void randomAttack() {
        Vector3 dir = _player.gameObject.transform.position - this.transform.position;
        switch(Random.Range(0,2)) {
            case 0:
                ShootMultiples(dir, 10, Mathf.PI / 2);
                break;
            case 1:
                ShootMultiples(dir, 15, Mathf.PI);
                break;
        }
    }

    private void Shoot(Vector3 dir) {
        Vector3 spawn = bullet_spawn.position;
        Projectile projectile = Instantiate(this._projectile, spawn, new Quaternion(0,0,0,0));
        projectile.VELOCITY = 2;
        projectile.transform.parent = null;
        projectile.Shoot(spawn, dir);
    }

    public void ShootPlayer(int num) {
        Vector3 dir = (_player.gameObject.transform.position - this.transform.position).normalized;
        ShootMultiples(dir, num, Mathf.PI / 2);
    }

    private void ShootMultiples(Vector3 dir, int num, float radians) {
        float start = -(radians / 2);
        float step = radians / num;
        for(float i = start; i < radians/2; i += step)
            Shoot(Rotate(dir, i));
    }

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _offset = GetComponent<BoxCollider2D>().offset * transform.localScale.y;
        _sprite = GetComponent<SpriteRenderer>();
        _attacksounds = GetComponents<AudioSource>();
        _projectile = Resources.Load<Projectile>("Projectile");
        _player = GameObject.FindWithTag("Player");
    }

    public void TakeDamage(float damage) {
        this._hp -= damage;
        if(this._hp <= 0) animator.Play("Dying");
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.tag == "Arrow") TakeDamage(1);
    }

    // Update is called once per frame
    void Update() {
        _timer += Time.deltaTime;
        if(_timer > TIMER) {
            animator.SetTrigger("attack");
        }
    }
}

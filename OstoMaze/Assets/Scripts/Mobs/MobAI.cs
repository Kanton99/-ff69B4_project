using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    public Transform Player;
    public float speed = 10f;
    public float distance = 10f;
    public float Attack_timer = 5f;

    private AudioSource[] attacksounds;
    private Projectile _projectile;
    private SpriteRenderer sprite;
    private Animator animator;

    public enum State { IDLE, MOVING, ATTACKING };
    private Vector2 Player_dir;
    // Start is called before the first frame update
    void Start()
    {
        _projectile = Resources.Load<Projectile>("Projectile");
        attacksounds = GetComponents<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Player_dir = Player.position - gameObject.transform.position;
        float player_dis = Player_dir.magnitude;

        Vector2 nPlayer_dir = Player_dir.normalized;


        sprite.flipX = (Vector3.Dot(Player_dir, Vector2.right) < 0) ? true : false;
        if(player_dis >= distance) gameObject.transform.position += new Vector3(nPlayer_dir.x, nPlayer_dir.y, 0) * speed * Time.deltaTime;
        else
        {
        }
    }

    private void Attack()
    {
        attacksounds[Random.RandomRange(0, 2)].Play();
        Vector3 spawn = gameObject.transform.position - new Vector3(0, 0.5f, 0);  // mob position - offset
        Projectile projectile = Instantiate(_projectile, spawn, new Quaternion(0, 0, 0, 0));
        projectile.transform.parent = null;
        projectile.Shoot(spawn, Player.transform.position);
    }
}

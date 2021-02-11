using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bows : MonoBehaviour
{
    public AudioSource sound;
    public MainController player;
    private Arrow _arrow;
    public Transform player_pos;
    public Animator player_anim;
    public bool is_finished = false;
    List<IEnemy> mobs = new List<IEnemy>();

    // Start is called before the first frame update
    void Start()
    {
        _arrow = Resources.Load<Arrow>("Arrow");
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        mobs.Add(coll.gameObject.GetComponent<IEnemy>());
    }

    private void OnTriggerExit2D(Collider2D coll) {
        mobs.Remove(coll.gameObject.GetComponent<IEnemy>());
    }

    private Vector3 Nearest() {
        float min_dist = (player_pos.position - mobs[0].GetPosition()).magnitude;
        Vector3 nearest = mobs[0].GetPosition();
        foreach (IEnemy mob in mobs) {
            float dist = (player_pos.position - mob.GetPosition()).magnitude;
            if (dist < min_dist) {
                nearest = mob.GetPosition();
                min_dist = dist;
            }
        }
        return nearest;
    }

    public void Attack() {
        if (mobs.Count > 0) {
            player_anim.SetTrigger("shooting");  // start animation
        }
    }

    void Update() {
        if (player.finished_animation) {
            sound.Play();
            if (mobs.Count > 0) {
                Vector3 near = Nearest();
                Vector3 spawn = player_pos.position;
                Vector3 forward = new Vector3(0,0,1);
                Vector3 up = Vector3.Cross(forward, near - spawn);
                Arrow arrow = Instantiate(this._arrow, spawn, Quaternion.LookRotation(forward, up));
                arrow.transform.parent = null;
                arrow.Shoot(spawn, near);
            }
        }
    }
}

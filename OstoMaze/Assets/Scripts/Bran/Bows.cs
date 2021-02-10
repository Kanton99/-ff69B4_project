using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bows : MonoBehaviour
{
    public AudioSource sound;
    private Arrow _arrow;
    public Transform player_pos;
    List<MobController> mobs = new List<MobController>();

    // Start is called before the first frame update
    void Start()
    {
        _arrow = Resources.Load<Arrow>("Arrow");
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        MobController mob = coll.gameObject.GetComponent<MobController>();
        mobs.Add(mob);
    }

    private void OnTriggerExit2D(Collider2D coll) {
        MobController mob = coll.gameObject.GetComponent<MobController>();
        mobs.Remove(mob);
    }

    private Vector3 Nearest() {
        float min_dist = (player_pos.position - mobs[0].transform.position).magnitude;
        Vector3 nearest = mobs[0].transform.position;
        foreach (MobController mob in mobs) {
            float dist = (player_pos.position - mob.transform.position).magnitude;
            if (dist < min_dist) {
                nearest = mob.transform.position;
                min_dist = dist;
            }
        }
        return nearest;
    }

    public void Attack() {
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

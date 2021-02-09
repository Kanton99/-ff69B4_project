using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bows : MonoBehaviour
{
    public AudioSource bow;
    private Projectile _arrow;
    public Transform player_pos;
    List<MobController> mobs = new List<MobController>();

    // Start is called before the first frame update
    void Start()
    {
        _arrow = Resources.Load<Projectile>("Arrow");
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "Mob")
        {
            MobController mob = coll.gameObject.GetComponent<MobController>();
            mobs.Add(mob);
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        if (coll.tag == "Mob") {
            MobController mob = coll.gameObject.GetComponent<MobController>();
            mobs.Remove(mob);
        }
    }

    private Vector3 Nearest() {
        Vector3 nearest = new Vector3(10f, 10f, 10f);
        float nearest_val = nearest.x + nearest.y;
        Vector3 near = player_pos.position - nearest;
        float near_val = Mathf.Abs(near.x) + Mathf.Abs(near.y);

        foreach (MobController mob in mobs) {
            near = player_pos.position - mob.transform.position;
            near_val = Mathf.Abs(near.x) + Mathf.Abs(near.y);
            if (near_val < nearest_val) {
                nearest = near;
                nearest_val = near_val;
            }
        }
        return nearest;
    }

    public void Attack() {
        bow.Play();
        Vector3 spawn = player_pos.position;
        Projectile arrow = Instantiate(this._arrow, spawn, new Quaternion(0,0,0,0));
        arrow.transform.parent = null;
        arrow.Shoot(spawn, Nearest());
    }
}

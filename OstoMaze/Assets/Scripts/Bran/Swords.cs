using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public AudioSource[] swords;
    public Animator player_anim;
    public MainController player;
    List<MobController> mobs = new List<MobController>();

    // Start is called before the first frame update
    void Start()
    {
        swords = GetComponents<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        MobController mob = coll.gameObject.GetComponent<MobController>();
        mobs.Add(mob);
    }

    private void OnTriggerExit2D(Collider2D coll) {
        MobController mob = coll.gameObject.GetComponent<MobController>();
        mobs.Remove(mob);
    }

    public void Attack() {
        player_anim.SetTrigger("swing");
    }

    void Update() {
        if (player.finished_animation) {  // if sword swinging animation is finished
            swords[Random.Range(0, 3)].Play();
            if (mobs.Count > 0) {
                foreach (MobController mob in mobs) {
                    mob.TakeDamage(3);
                }
            }
        }
    }
}

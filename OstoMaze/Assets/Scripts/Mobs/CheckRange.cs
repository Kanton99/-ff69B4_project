using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour
{

    public MobController mob;

    void OnTriggerEnter2D(Collider2D coll)
    {
       // MainController player = coll.gameObject.GetComponent<MainController>();

        if (coll.tag == "MainHurtBox")
        {
            mob.enterRange(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (mob.getPlayer() != null)
        {
            if (coll.gameObject == mob.getPlayer())
            {
                Debug.Log("Leave range");
                mob.leaveRange();
            }
        }
    }
}

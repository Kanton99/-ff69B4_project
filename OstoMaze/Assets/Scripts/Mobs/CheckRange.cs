using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour
{

    public MobController mob;

    void OnTriggerEnter2D(Collider2D coll)
    {
        mob.enterRange(coll.gameObject);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (mob.getPlayer() != null)
        {
            if (coll.gameObject == mob.getPlayer())
            {
                mob.leaveRange();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggDialog : MonoBehaviour
{

    public MainController player;

    void OnTriggerEnter2D(Collider2D coll) {
        if(!player.isBusy()) {
            NPCController npc = coll.gameObject.GetComponent<NPCController>();
            if (npc != null)
            {
                player.enterReadyTalk(npc);
            }
        }

    }
    void OnTriggerExit2D(Collider2D coll) {
        if(coll.gameObject == player.npc.gameObject) {
            player.leaveReadyTalk();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggDialog : MonoBehaviour
{

    public MainController player;

    void OnTriggerEnter2D(Collider2D coll) {
        //Debug.Log("Entering");
        if(!player.isBusy()) {
        IInteractible interactible = coll.gameObject.GetComponent<IInteractible>();
        if(interactible != null)
            player.enterInteractionRange(coll.gameObject.GetComponent<IInteractible>());
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (player.interactible != null && coll.gameObject == player.interactible.getGameObject())
            player.leaveInteractionRange();
    }
}

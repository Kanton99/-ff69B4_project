using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour, IInteractible
{
    public bool _talking = false;

    public Animator cloud_anim;
    public Dialog dialog;
    public UnityEvent interaction_over;

    public bool interact() {
        if(talk())
            return true;
        else {
            interaction_over.Invoke();
            this.gameObject.SetActive(false);
            return false;
        }
    }

    public void enterInteractionRange(GameObject gameObject) {
        Debug.Log("Player in range!");
        readyTalk(gameObject);
    }

    public void leaveInteractionRange() {
        leaveReadyTalk();
    }

    public GameObject getGameObject() {
        return this.gameObject;
    }

    public void readyTalk(GameObject player) {
        cloud_anim.SetBool("Visible", true);
    }

    public void leaveReadyTalk() {
        _talking = false;
        cloud_anim.SetBool("Visible", false);
    }

    public bool talk() {
        if(!_talking) {
            dialog.animator.SetBool("IsOpen", true);
            _talking = true;
            return true;
        } else
            if(dialog.next()) {
                return true;
            } else {
                dialog.animator.SetBool("IsOpen", false);
                _talking = false;
                return false;
            }
    }
}

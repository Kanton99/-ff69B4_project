using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour, IInteractible
{
    private GameManager _manager;
    // Start is called before the first frame update
    void Start() {
        _manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        enabled = false;
    }

    public bool interact() {
        _manager.changeStateTo(GameManager.State.TO_HUB);
        return true;
    }

    public void enterInteractionRange(GameObject gameObject) {}
    public void leaveInteractionRange() {}
    public GameObject getGameObject() { return null; }
}

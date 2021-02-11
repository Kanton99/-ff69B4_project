using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour, IInteractible
{
    public UnityEvent interaction;
    // Start is called before the first frame update
    void Start() { enabled = false; }

    public bool interact() {
        interaction.Invoke();
        return true;
    }

    public void enterInteractionRange(GameObject gameObject) {}
    public void leaveInteractionRange() {}
    public GameObject getGameObject() { return null; }
}

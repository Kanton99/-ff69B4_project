using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible {

    /*
    It returns true if the interaction can continue, false
    otherwise.
    */
    bool interact();
    void enterInteractionRange(GameObject gameObject);
    void leaveInteractionRange();
    GameObject getGameObject();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Room room;
    // Start is called before the first frame update
    void Start() { enabled = false; }

    void OnTriggerEnter2D (Collider2D collider) {
        room.curr_state = Room.State.TO_FIGHT;
    }
}

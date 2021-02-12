using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHub : MonoBehaviour
{
    private GameManager manager;

    // Start is called before the first frame update
    void Start() {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        manager.curr_state = GameManager.State.TO_DUNGEON;
    }
}

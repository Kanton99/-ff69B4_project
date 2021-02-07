using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject wall;
    public GameObject door;
    public Animator _door_anim;

    // Disable the update callback
    void Start() {
        _door_anim = GetComponent<Animator>();
        setUnavailable();
        enabled = false;
    }

    public void setAvailable() {
        wall.SetActive(false);
        door.SetActive(true);
    }

    public void setUnavailable() {
        wall.SetActive(true);
        door.SetActive(false);
    }

    public void open() {
        _door_anim.SetBool("isOpen", true);
    }
    public void close() {
        _door_anim.SetBool("isOpen", false);
    }
}

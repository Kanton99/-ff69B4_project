using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    public Transform getEnemies() {
        foreach(Transform child in transform)
            if(child.tag == "Mobs") {
                return child;
            }
        return null;
    }
}

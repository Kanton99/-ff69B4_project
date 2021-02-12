using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survive : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() {
        if (GameObject.FindGameObjectsWithTag("UI").Length > 1)
            Destroy(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}

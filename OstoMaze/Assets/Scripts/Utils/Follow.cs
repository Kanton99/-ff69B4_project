using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform to_follow;
    public Vector3 offset;

    // Quasi singleton paradigm, only a GameManager per scene, the oldest one takes priority.
    void Awake() {
        if(GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = to_follow.position + offset;
    }
}

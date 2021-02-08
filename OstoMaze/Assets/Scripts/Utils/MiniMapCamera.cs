using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Transform p_pos;

    private void Start()
    {
        p_pos = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        gameObject.transform.position = p_pos.position + new Vector3(0,0,-10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Vector3 p_pos;
    private void Start()
    {
        
    }

    private void Update()
    {
        p_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        gameObject.transform.position.Set(p_pos.x, p_pos.y, gameObject.transform.position.z);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHub : MonoBehaviour
{
    public GameManager manager;

    // Start is called before the first frame update
    void Start() {}

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(manager.loadDungeon());
    }
}
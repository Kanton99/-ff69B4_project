using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IInteractible
{

    public SpriteRenderer sprite;
    public float TIMER;
    private float _timer;

    public bool interact() {
        return true;
    }

    public void enterInteractionRange(GameObject gameObject) {
        Debug.Log("Player in range!");
    }

    public void leaveInteractionRange() {

    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public void Drop() {

    }


    void Start()
    {
        _timer = TIMER;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0) Destroy(this.gameObject);
    }
}

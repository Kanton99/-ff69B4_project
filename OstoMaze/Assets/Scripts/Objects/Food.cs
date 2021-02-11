using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public SpriteRenderer sprite;
    public float TIMER;
    private float _timer;
    
    public void Drop() {
        _timer = TIMER;
        // a dirty Little secret in unity is that you can iterate Transform.
        foreach(Transform child in this.transform) {
            if (child.name == "Animated") { 
                child.gameObject.AddComponent<SpriteRenderer>();
                Sprite[] sprites = Resources.LoadAll<Sprite>("FoodSprites");
                child.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                child.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0) Destroy(this.gameObject);
    }
}

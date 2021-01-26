using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAvailable : MonoBehaviour
{
    public bool isAvailable = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collider other)
    {
          isAvailable = false;
    }

    //When the Primitive exits the collision, it will change Color
    private void OnCollisionExit2D(Collider other)
    {
        isAvailable = true;
    }
}

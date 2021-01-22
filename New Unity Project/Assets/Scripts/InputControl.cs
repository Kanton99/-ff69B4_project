using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public float moving_speed;
    public Animator animator;
    public SpriteRenderer arms;
    public SpriteRenderer body;
    public Rigidbody2D rigid;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start() {}
    
    /* ANOTHER METHOD TO MOVE THE CHARACTER
    // Update is called once per frame
    void Update() {
      Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
      if(direction != Vector3.zero) {
        animator.SetBool("run", true);
        bool flip = false;
        if(Vector3.Dot(direction, new Vector3(1,0,0)) < 0)
          flip = true;
        arms.flipX = flip;
        body.flipX = flip;
        rigid.velocity = moving_speed * direction;
      } else {
        animator.SetBool("run", false);
        rigid.velocity = Vector3.zero;
      }
    }*/

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if(direction != Vector2.zero) {
            animator.SetBool("run", true);
            bool flip = false;
            if(Vector2.Dot(direction, new Vector2(1,0)) < 0)    flip = true;
            arms.flipX = flip;
            body.flipX = flip;
        } 
        else   animator.SetBool("run", false);
    }

    void FixedUpdate() 
    {
        if(direction != Vector2.zero)   rigid.MovePosition(rigid.position + direction * moving_speed * Time.fixedDeltaTime);
        else         rigid.velocity = Vector3.zero;
    }

}

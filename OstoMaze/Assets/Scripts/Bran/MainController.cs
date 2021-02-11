using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public float moving_speed;
    public Animator animator;
    public SpriteRenderer arms;
    public SpriteRenderer bow;
    public SpriteRenderer body;
    public Rigidbody2D rigid;
    public IInteractible interactible;
    public Slider slider;
    public Swords swords;
    public Bows bows;
    public bool is_playing;
    public bool finished_animation = false; // to cotrol the shooting of the arrows and the swing of the sword
    public Joystick input;

    public Vector2 direction;
    public enum State { NORMAL, DEAD, SCRIPTED, READY_INTERACT, INTERACT, CHANGING_BAG };
    public State state;

    public int hp;
    public float bs;  // barra stomia [0, 50]

    // Quasi singleton paradigm, only a Character per scene, the oldest one takes priority.
    void Awake() {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        state = State.NORMAL;
        animator = GetComponent<Animator>();
    }

    void move() {
        Vector3 direction = new Vector2(Input.GetAxis("Horizontal") + input.Horizontal, Input.GetAxis("Vertical")+input.Vertical).normalized;
        if(direction != Vector3.zero) {
            animator.SetBool("run", true);
            if(Vector3.Dot(direction, new Vector3(1,0,0)) < 0)
                flip(false);
            if(Vector3.Dot(direction, new Vector3(1,0,0)) > 0)
                flip(true);
            rigid.velocity = moving_speed * direction;
        } else
            stop();
    }

    void stop() {
        animator.SetBool("run", false);
        rigid.velocity = Vector3.zero;
    }

    public void attack() {
        if (state == State.NORMAL)
        {
            if (animator.GetBool("sword")) swords.Attack();
            else bows.Attack();
        } else if (state == State.INTERACT || state == State.READY_INTERACT) { // Emergency fix!
            interact();
        }
    }

    public void interact() {
        if (state == State.INTERACT || state == State.READY_INTERACT)
        {
            if (interactible.interact())
                state = State.INTERACT;
            else
                state = State.NORMAL;
        }
    }

    public void changeWeapon() {
        if (state == State.NORMAL)
        {
            bool toggle = animator.GetBool("sword");
            animator.SetBool("sword", !toggle);
        }
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        animator.SetTrigger("wound");
    }

    public void AddHealth(int health)
    {
        hp += health;
    }

    public void enterInteractionRange(IInteractible interactible) {
        this.interactible = interactible;
        interactible.enterInteractionRange(this.gameObject);
        state = State.READY_INTERACT;
    }

    public void leaveInteractionRange() {
        interactible.leaveInteractionRange();
        this.interactible = null;
        state = State.NORMAL;
    }

    public bool isBusy() {
        return state == State.READY_INTERACT || state == State.INTERACT;
    }

    public void change_bag() {
        state = State.CHANGING_BAG;
        animator.SetTrigger("ChangeBag");
        bs = 0;
    }

    public void change(State new_state) {
        state = new_state;
    }

    public void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void flip(bool look_right) {
        if(look_right && transform.localScale.x < 0)
            flip();
        else if(!look_right && transform.localScale.x > 0)
            flip();
    }

    // Update is called once per frame
    void Update() {
        bs += 0.01f * Time.deltaTime;
        switch(state){
            case State.NORMAL:
                move();
                break;
            case State.READY_INTERACT:
                move();
                //interact();
                break;
            case State.INTERACT:
                stop();
                //interact();
                break;
            // TODO: Needs to be corrected!
            case State.CHANGING_BAG:
               change(State.NORMAL);
               break;
        }
    }
}

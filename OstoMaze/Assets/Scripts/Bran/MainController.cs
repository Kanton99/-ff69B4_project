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
    public Swords swords;
    public Bows bows;
    public bool is_playing;
    public bool finished_animation = false; // to cotrol the shooting of the arrows and the swing of the sword
    public Joystick input;
    public Slider ostomy;
    public AudioSource die_sound;
    public AudioSource respawn_sound;
    private bool dead = false;

    public Vector2 direction;
    public enum State {NORMAL, DEAD, SCRIPTED, READY_INTERACT, INTERACT, CHANGING_BAG};
    public State state;

    public int hp;
    public float bs;  // barra stomia

    // Quasi singleton paradigm, only a Character per scene, the oldest one takes priority.
    void Awake() {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
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
            bool flip = body.flipX;
            if(Vector3.Dot(direction, new Vector3(1,0,0)) < 0)
                flip = true;
            if(Vector3.Dot(direction, new Vector3(1,0,0)) > 0)
                flip = false;
            arms.flipX = flip;
            body.flipX = flip;
            bow.flipX = flip;
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
        if (hp <= 0 && !dead) {
            dead = true;
            die_sound.Play();
            animator.Play("Death");
            animator.SetLayerWeight(animator.GetLayerIndex("Wounded"), 0);
        }
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

    public void Die() {
        
    }

    // Update is called once per frame
    void Update() {
        bs += 0.01f * Time.deltaTime;
       // ostomy.value = bs;
        switch(state){
            case State.NORMAL:
                //change_bag();
                move();
                //attack();
                //changeWeapon();
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

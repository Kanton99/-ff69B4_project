﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public float moving_speed;
    public Animator animator;
    public SpriteRenderer arms;
    public SpriteRenderer body;
    public Rigidbody2D rigid;
    public NPCController npc;
    public MobController mob;
    public AudioSource[] swords; 

    public Vector2 direction;
    public enum State {NORMAL, DEAD, SCRIPTED, READY_TALK, TALK, CHANGING_BAG};
    public State _curr_state;

    // Start is called before the first frame update
    void Start() {
        _curr_state = State.NORMAL;
        swords = GetComponents<AudioSource>();
    }

    void move() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
        if(direction != Vector3.zero) {
            animator.SetBool("run", true);
            bool flip = body.flipX;
            if(Vector3.Dot(direction, new Vector3(1,0,0)) < 0)
                flip = true;
            if(Vector3.Dot(direction, new Vector3(1,0,0)) > 0)
                flip = false;
            arms.flipX = flip;
            body.flipX = flip;
            rigid.velocity = moving_speed * direction;
          } else
            stop();
    }

    void stop() {
        animator.SetBool("run", false);
        rigid.velocity = Vector3.zero;
    }

    void attack() {
        if(Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("swing");
            swords[Random.RandomRange(0,3)].Play();
        }
    }

    void talk() {
        if(Input.GetButtonDown("Fire1")) {
            if(npc.talk())
                _curr_state = State.TALK;
            else
                _curr_state = State.NORMAL;
        }
    }

    public void enterReadyTalk(NPCController npc) {
        this.npc = npc;
        npc.readyTalk(this.gameObject);
        _curr_state = State.READY_TALK;
    }
    /*
    public void enterMobRange(MobController mob)
    {
        this.mob = mob;
        mob.enterRange(this.gameObject);
    }
    */
    public void leaveReadyTalk() {
        npc.leaveReadyTalk();
        this.npc = null;
        _curr_state = State.NORMAL;
    }    
    /*
    public void leaveMobRange() {
        mob.leaveRange();
        this.mob = null;
        _curr_state = State.NORMAL;
    }
    */
    public bool isBusy() {
        return _curr_state == State.READY_TALK || _curr_state == State.TALK;
    }

    public void change_bag() {
        if(Input.GetButtonDown("Fire2")) {
            _curr_state = State.CHANGING_BAG;
            animator.SetTrigger("ChangeBag");
        }
    }

    private void change(State new_state) {
        _curr_state = new_state;
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch(_curr_state){
            case State.NORMAL:
                change_bag();
                move();
                attack();
                break;
            case State.READY_TALK:
                move();
                talk();
                break;
            case State.TALK:
                stop();
                talk();
                break;
            case State.CHANGING_BAG:
               change(State.NORMAL);
                break;
        }
    }
}
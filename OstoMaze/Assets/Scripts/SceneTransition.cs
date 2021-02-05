using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    private string _dungeon1;
    private string _hub;
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    public void goToDungeon() {
        animator.SetBool("FadeIn", true);
    }

}

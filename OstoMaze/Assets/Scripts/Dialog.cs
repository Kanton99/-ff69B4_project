using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TMP_Text name_text;
    public TMP_Text dialog_text;
    public Animator animator;

    public float scrolling_speed;
    public NPCController npc;
    public string my_name;
    [TextArea]
    public string[] dialog;

    private int _idx;
    private bool _is_typing;
    // Start is called before the first frame update
    void Start()
    {
        dialog_text.text = "";
        if(npc != null)
            name_text.text = npc.gameObject.name;
        else
            name_text.text = my_name;
        _idx = 0;
        _is_typing = false;
        enabled = false;
    }

    public void startType() {
        _idx = 0;
        StartCoroutine(type(dialog[_idx]));
    }

    public void clear() {
        _idx = 0;
        _is_typing = false;
        dialog_text.text = "";
    }

    public bool next() {
        if(_is_typing)
            return typeEntireScreen();
        return nextScreen();
    }

    private bool typeEntireScreen() {
        StopAllCoroutines();
        dialog_text.text = dialog[_idx];
        _is_typing = false;
        return true;
    }

    private bool nextScreen() {
        if(_idx >= dialog.Length - 1)
            return false;
        StopAllCoroutines();
        StartCoroutine(type(dialog[++_idx]));
        return true;
    }

    private IEnumerator type(string text) {
        _is_typing = true;
        dialog_text.text = "";
        foreach(char ele in text.ToCharArray()) {
            if(npc != null) npc.randomSpeak();
            dialog_text.text += ele;
            yield return new WaitForSeconds(1 / scrolling_speed);
        }
        _is_typing = false;
    }
}

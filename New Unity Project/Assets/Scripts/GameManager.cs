using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerData))]
public class GameManager : MonoBehaviour
{
    public Camera camera;
    public Animator animator;

    private bool is_playing = true;

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject); 
        enabled = false;
    }

    private void animationPlaying() { is_playing = true; }

    private void animationNotPlaying () { is_playing = false; }

    public IEnumerator loadDungeon() {
        animator.SetBool("ChangingScene", true);
        while(is_playing) yield return null;
        yield return load("Assets/Scenes/Dungeon1.unity");

        GameObject generator = GameObject.FindWithTag("RoomGenerator");
        generator.GetComponent<RandomRoomGenerator>().generateRooms(7);
    }

    public IEnumerator loadHUB () {
        animator.SetBool("ChangingScene", true);
        while(is_playing) yield return null;
        yield return load("Assets/Scenes/HUB.unity");
    }

    private IEnumerator load(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) yield return null;


        animator.SetBool("ChangingScene", false);
    }
}

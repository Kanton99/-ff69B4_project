using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MainController player;
    public Camera camera;
    public Animator animator;

    private bool is_playing = true;

    private void animationPlaying() {
        is_playing = true;
    }

    private void animationNotPlaying () {
        is_playing = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(camera.gameObject);
        DontDestroyOnLoad(this.gameObject);
        enabled = true;
    }

    public IEnumerator loadDungeon() {
        animator.SetBool("ChangingScene", true);
        while(is_playing) yield return null;
        StartCoroutine(load("Assets/Scenes/Dungeon1.unity"));
    }

    public IEnumerator loadHUB () {
        animator.SetBool("ChangingScene", true);
        StartCoroutine(load("Assets/Scenes/HUB.unity"));
        return null;
    }

    private IEnumerator load(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        RandomRoomGenerator generator = GameObject.FindWithTag("RoomGenerator").GetComponent<RandomRoomGenerator>();
        Vector3 spawn_point = generator.generateRooms(5);
        player.gameObject.transform.position = spawn_point;

        animator.SetBool("ChangingScene", false);
    }

}

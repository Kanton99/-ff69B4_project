using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class GameManager : MonoBehaviour
{
    private MusicManager music;
    public MainController player;
    public Camera players_camera;
    public Animator animator;

    private bool is_playing = true;

    public enum State {HUB, TO_DUNGEON, TO_HUB, DUNGEON};
    public State curr_state;

    // Quasi singleton paradigm, only a GameManager per scene, the oldest one takes priority.
    void Awake() {
        if(GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(players_camera.gameObject);

    }

    void Start() {
        if(curr_state == State.DUNGEON)
            initDungeon();
    }

    private void animationPlaying() { is_playing = true; }
    private void animationNotPlaying () { is_playing = false; }
    private IEnumerator fadeIn() {
        animator.SetBool("ChangingScene", true);
        while(is_playing) yield return null;
    }
    private IEnumerator fadeOut() {
        animator.SetBool("ChangingScene", false);
        yield break;
    }

    private void initDungeon(int num_rooms=7) {
        GameObject generator = GameObject.FindWithTag("RoomGenerator");
        Vector3 spawn = generator.GetComponent<RandomRoomGenerator>().generateRooms(num_rooms);
        player.gameObject.transform.position = spawn;
    }

    public IEnumerator loadDungeon() {
        GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>().HubtoDungeon();
        yield return fadeIn();
        yield return loadScene("Assets/Scenes/Dungeon1.unity");
        initDungeon();
        yield return fadeOut();
    }

    private void initHub() {
        GameObject spawn_point = GameObject.FindWithTag("SpawnPoint");
        player.gameObject.transform.position = spawn_point.transform.position;
    }

    public IEnumerator loadHUB () {
        yield return fadeIn();
        yield return loadScene("Assets/Scenes/HUB.unity");
        initHub();
        yield return fadeOut();
    }

    private IEnumerator loadScene(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
            yield return null;
    }

    public void changeStateTo(State state) {
        curr_state = state;
    }

    void Update() {
        switch(curr_state) {
            case State.HUB:
            break;
            case State.DUNGEON:
            break;
            case State.TO_HUB:
                StartCoroutine(loadHUB());
                changeStateTo(State.HUB);
            break;
            case State.TO_DUNGEON:
                StartCoroutine(loadDungeon());
                changeStateTo(State.DUNGEON);
            break;
        }

    }
}

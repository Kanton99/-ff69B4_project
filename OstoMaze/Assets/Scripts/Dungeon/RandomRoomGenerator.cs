using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomGenerator : MonoBehaviour
{
    public GameObject room_template;
    public Room boss_room;

    public GameObject[] obstacles;

    public Vector3 starting_point = Vector3.zero;
    public Transform master_room;

    [SerializeField]
    List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start() {
        enabled = false;
    }

    public Vector3 generateRooms(int num_rooms = 0) {
        Dictionary<Vector3, Room> room_locations = new Dictionary<Vector3, Room>();
        rooms.Add(generate(room_template, starting_point, master_room));
        room_locations[starting_point] = rooms[0];

        Vector3 spawn_point = rooms[0].getCenter();

        for(int i = 0; i < num_rooms - 1; i++) {
            int rint = Random.Range(0, rooms.Count - 1);
            Room extend_room = rooms[rint];
            Room.DoorType door_type = extend_room.unavailable[Random.Range(0, extend_room.unavailable.Count - 1)];
            Vector3 position = extend_room.transform.position + extend_room.getSize() * extend_room.directionOf(door_type);
            if(!room_locations.ContainsKey(position)) {
                if(i == num_rooms - 2) {
                    boss_room.gameObject.transform.parent = master_room;
                    boss_room.transform.position = position;
                    connect(extend_room, boss_room.GetComponent<Room>(), door_type);
                } else {
                    Room room = generate(room_template, position, master_room);

                    connect(extend_room, room, door_type);
                    Obstacles obstacle = Instantiate(getRandomObstacle(), room.gameObject.transform).GetComponent<Obstacles>();
                    room.addEnemies(obstacle.getEnemies());
                    //room.addContent(obstacle.gameObject.transform);
                    rooms.Add(room);
                    room_locations[room.gameObject.transform.position] = room;
                }
            } else {
                Room room = room_locations[position];
                connect(extend_room, room, door_type);
                if(room.available.Count == 4)
                    rooms.Remove(room);
                i--;
            }
            if(extend_room.available.Count == 4)
                rooms.Remove(extend_room);
        }
        room_template.SetActive(false);
        return spawn_point;
    }

    private GameObject getRandomObstacle() {
        if(obstacles.Length == 0)
            return null;
        return obstacles[Random.Range(0, obstacles.Length)];
    }

    private Room generate(GameObject room_template, Vector3 position, Transform parent) {
        GameObject room = Instantiate(room_template, parent);
        room.transform.position = position;
        return room.GetComponent<Room>();
    }

    private void connect(Room room1, Room room2, Room.DoorType door_type1) {
        room1.openPath(door_type1);
        room2.openPath(((int)door_type1 + 2)%4);
    }
}

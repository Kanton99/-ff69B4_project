using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomGenerator : MonoBehaviour
{
    public int num_rooms;
    public GameObject room_template;
    public Vector3 starting_point = Vector3.zero;
    public Transform master_room;

    [SerializeField]
    List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start() { enabled = false;}

    public Vector3 generateRooms(int rooms_num = 0) {
        if (num_rooms <= 0) rooms_num = this.num_rooms;
        Dictionary<Vector3, Room> room_locations = new Dictionary<Vector3, Room>();
        rooms.Add(generate(room_template, starting_point, master_room));
        room_locations[starting_point] = rooms[0];

        Vector3 spawn_point = rooms[0].center;

        for(int i = 0; i < num_rooms - 1; i++) {
            int rint = Random.Range(0, rooms.Count - 1);
            Room extend_room = rooms[rint];
            Room.DoorType door_type = extend_room.unavailable[Random.Range(0, extend_room.unavailable.Count - 1)];
            Vector3 position = extend_room.transform.position + extend_room.getSize() * extend_room.directionOf(door_type);
            if(!room_locations.ContainsKey(position)) {
                Room room = generate(room_template, position, master_room);
                extend_room.setAvailable(door_type);
                room.setAvailable(((int)door_type + 2)%4);
                rooms.Add(room);
                room_locations[room.gameObject.transform.position] = room;
                // Piazza oggetti ...
                // Crea dati per la minimappa ...
            } else {
                Room room = room_locations[position];
                extend_room.setAvailable(door_type);
                room.setAvailable(((int)door_type + 2)%4);
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

    private Room generate(GameObject room_template, Vector3 position, Transform parent) {
        GameObject room = Instantiate(room_template, parent);
        room.transform.position = position;
        return room.GetComponent<Room>();
    }

}

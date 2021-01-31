using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomGenerator : MonoBehaviour
{
    public int num_rooms;
    public GameObject room_template;
    public Vector3 starting_point = Vector3.zero;
    public Transform master_room;
    // Start is called before the first frame update
    void Start()
    {
        List<Room> rooms = new List<Room>();
        room_template.transform.position = Vector3.up * 1000;
        rooms.Add(generate(room_template, starting_point, master_room));

        Debug.Log("Starting generating rooms!");
        for(int i = 0; i < num_rooms - 1; i++) {
            int rint = Random.Range(0, rooms.Count - 1);
            // Debug.Log("We have " + rooms.Count + " rooms");
            // Debug.Log("Choosen the room : " + rint);
            Room random_room = rooms[rint];
            Room.DoorType door_type = random_room.unavailable[Random.Range(0, random_room.unavailable.Count - 1)];
            Room near = random_room.getNextRoom(door_type);
            if(near == null) {
                Debug.Log("Generating a new room!");
                Door random_door = random_room.doors[(int)door_type];
                Vector3 position = random_room.transform.position + random_room.getSize() * random_room.directionOf(door_type);
                Room room = generate(room_template, position, master_room);
                random_room.setAvailable(door_type);
                room.setAvailable(((int)door_type + 2)%4);
                rooms.Add(room);
            } else {
                Debug.Log("Collided with : " + near.gameObject.name);
                Debug.Log("Room present, so opening a door!");
                random_room.setAvailable(door_type);
                near.setAvailable(((int)door_type + 2)%4);
                if(near.available.Count == 4)
                    rooms.Remove(near);
                i--;
            }
            if(random_room.available.Count == 4)
                rooms.Remove(random_room);
        }
        room_template.SetActive(false);
        Debug.Log("Generation complete!");
    }

    private Room generate(GameObject room_template, Vector3 position, Transform parent) {
        GameObject room = Instantiate(room_template, parent);
        room.transform.position = position;
        return room.GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

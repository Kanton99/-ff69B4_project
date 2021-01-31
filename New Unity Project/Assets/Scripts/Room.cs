using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Type {TREASURE, ENEMY, BOSS};
    public enum DoorType {NORTH=0, EAST, SOUTH, WEST};
    public Door[] doors;
    public List<DoorType> available;
    public List<DoorType> unavailable;
    public Transform center;

    // Start is called before the first frame update
    void Start() {
        enabled = false;
    }

    public void setAvailable(DoorType door_type) {
        Door new_door = doors[(int)door_type];
        new_door.setAvailable();
        available.Add(door_type);
        unavailable.Remove(door_type);
    }

    public void setAvailable(int door_type) {
        if(door_type < 0 || door_type >= 4)
            throw new ArgumentException("door_type must between 0 and 3");
        Door new_door = doors[door_type];
        new_door.setAvailable();
        available.Add((DoorType)door_type);
        unavailable.Remove((DoorType)door_type);
    }

    public Room getNextRoom(DoorType door_type) {
        LayerMask mask = LayerMask.GetMask("Room space");
        RaycastHit2D hit = Physics2D.Raycast(center.position + directionOf(door_type) * 5.2f, directionOf(door_type), getSize(), mask);
        if (hit.collider == null)
            return null;
        else
            return hit.collider.gameObject.transform.parent.GetComponent<Room>();
    }

    public Vector3 directionOf(DoorType door_type) {
        switch(door_type) {
            case DoorType.NORTH:
                return new Vector3(0, 1, 0);
            case DoorType.EAST:
                return new Vector3(1, 0, 0);
            case DoorType.SOUTH:
                return new Vector3(0, -1, 0);
            case DoorType.WEST:
                return new Vector3(-1, 0, 0);
        }
        return Vector3.zero;
    }

    public void openDoors() {
        foreach(DoorType type in available)
            doors[(int)type].open();

    }

    public void closeDoors() {
        foreach(DoorType type in available)
            doors[(int)type].close();
    }

    //Hard coded because i can't find the number of cells in a grid.
    public float getSize() {
        return 28.0f;
    }

}

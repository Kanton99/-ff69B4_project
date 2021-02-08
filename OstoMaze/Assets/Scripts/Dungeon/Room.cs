using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector3 center;
    public enum DoorType {NORTH=0, EAST, SOUTH, WEST};
    public Door[] doors;
    public List<DoorType> available;
    public List<DoorType> unavailable;
    public Transform enemies;

    public enum State {UNEXPLORED, TO_FIGHT, FIGHT, TO_CLEARED, CLEARED};
    public State curr_state;

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
        return 20.0f;
    }

    void Start () {
        curr_state = State.UNEXPLORED;
    }

    void Update () {
        switch (curr_state) {
            case State.UNEXPLORED:
                break;
            case State.TO_FIGHT:
                if (enemies.childCount == 0) {
                    curr_state = State.CLEARED;
                    break;
                }
                closeDoors();
                curr_state = State.FIGHT;
                break;
            case State.FIGHT:
                if (enemies.childCount == 0)
                    curr_state = State.TO_CLEARED;
                    break;
            case State.TO_CLEARED:
                openDoors();
                curr_state = State.CLEARED;
                break;
            case State.CLEARED:
                break;
        }
    }

}

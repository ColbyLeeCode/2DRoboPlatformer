using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;

    //is entry or exit door
    [SerializeField]
    GameObject DoorType;

    //track the state of the door
    int stateOfDoor = 1;

    void Start()
    {
        //init animator
        anim = GetComponent<Animator>();

        //set entry door to open
        if (DoorType.name == "EntryDoor")
            anim.SetFloat("DoorState", 3);
        //set exit door to locked
        if (DoorType.name == "ExitDoor")
            LockDoor();
    }

    //locks door and sets state
    void LockDoor()
    {
        if(DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 1);
            stateOfDoor = 1;
        }
    }

    //unlocks door and sets state
    public void UnlockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 2);
            stateOfDoor = 2;
        }

    }

    //opens door and sets state
    public void OpenDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            anim.SetFloat("DoorState", 3);
            stateOfDoor = 3;
        }
    }

    public void SetDoorState(int state)
    {
        if (state == 1 && DoorType.name == "ExitDoor")
            LockDoor();
        if (state == 2 && DoorType.name == "ExitDoor")
            UnlockDoor();
        if (state == 3 && DoorType.name == "ExitDoor")
            OpenDoor();
    }

    //get current door state
    public int GetDoorState()
    {
        return stateOfDoor;
    }
}

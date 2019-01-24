using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] switches;
    [SerializeField]
    GameObject exitDoor;

    int numOfSwitches = 0;

    [SerializeField]
    Text switchCount;

    void Start()
    {
        GetNumOfSwitches();
    }

    public int GetNumOfSwitches()
    {
        int x = 0;

        for(int i = 0; i < switches.Length; i++)
        {
            if (switches[i].GetComponent<Switch>().isOn == false)
                x++;
            else if (switches[i].GetComponent<Switch>().isOn == true)
                numOfSwitches++;
        }

        numOfSwitches = x;
        return numOfSwitches;
    }

    public void GetExitDoorState()
    {
        //open exit door
        if(numOfSwitches <= 0)
        {
            exitDoor.GetComponent<Door>().OpenDoor();
        }
    }

    void Update()
    {
        switchCount.text = GetNumOfSwitches().ToString();

        GetExitDoorState();
    }
}

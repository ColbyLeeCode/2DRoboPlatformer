using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] switches;
    [SerializeField]
    GameObject exitDoor;

    GameObject[] bullets { get; set; }

    int numOfSwitches = 0;
    

    [SerializeField]
    Text switchCount;

    public static GameManager gm;

    void Awake()
    {
        gm = this;

        Debug.Log("GameManger instance:" + gm.ToString());
    }

    public void LoadLastSave(int index)
    {
        SceneManager.LoadScene(index);
    }

    public int GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    void Start()
    {
        GetNumOfSwitches();
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
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
        //check if bullet has collided with anything
        

        switchCount.text = GetNumOfSwitches().ToString();

        GetExitDoorState();
    }
}

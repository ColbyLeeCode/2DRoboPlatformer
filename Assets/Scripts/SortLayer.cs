using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour
{
    public string sortLayerName;

    public GameObject[] go;

    void Start()
    {
        //get each of the sprite that are a child of game object this script is attatched to
        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            //set those sprites sorting layer names to the one we have specified
            sr.GetComponent<Renderer>().sortingLayerName = sortLayerName;
        }
        
    }
}

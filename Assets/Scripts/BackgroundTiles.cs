using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiles : MonoBehaviour
{
    //arrayof prefabs for the tiles
    public GameObject[] tiles;

    //start position of the tiles
    public Vector3 tileStartPos;

    //tile spacing
    Vector2 tileSpacing;

    //width of the grid
    public int gridWidth;

    //height of the grid
    public int gridHeight;

    void Start()
    {
        tileSpacing = tiles[0].GetComponent<Renderer>().bounds.size;

        //loop the number of rows height
        for(int i = 0; i < gridHeight; i++)
        {
            //loop the number of columns wide
            for(int y = 0; y < gridWidth; y++)
            {
                //grab a random number between 0 and however many tiles their are
                int randomTile = Random.Range(0, tiles.Length);

                GameObject go = Instantiate(tiles[randomTile], new Vector3(tileStartPos.x + (y * tileSpacing.x), tileStartPos.y + (i * tileSpacing.y)), Quaternion.identity) as GameObject;

                //add all the game objects as a child of bgtiles
                go.transform.parent = GameObject.Find("BGTiles").transform;
            }
        }
    }
}

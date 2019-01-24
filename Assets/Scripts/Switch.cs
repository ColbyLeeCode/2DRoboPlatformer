using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField]
    GameObject switchOn;
    [SerializeField]
    GameObject switchOff;

    public bool isOn = false;

    void Start()
    {
        audioManager = AudioManager.instance;
        //set switch to off sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;

        if(!isOn)
            audioManager.PlaySound("switch");

        isOn = true;

        
    }
}

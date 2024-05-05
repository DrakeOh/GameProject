using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    PlayerController PlayerController;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlaySound();

    }
    public void OnCollisionEnter2D(Collision2D other)
    {

    }
    public void PlaySound()
    {
        if (audioSource != null)
        {
             if (Input.GetKey(KeyCode.K))
                {


                audioSource.Play();
            }
        }
    }
}





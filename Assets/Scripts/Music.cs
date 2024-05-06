using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    PlayerController PlayerController;
    public AudioSource Mohamednull;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlaySound();

    }

    public void PlaySound()
    {
        if (Mohamednull != null)
        {
             if (Input.GetKey(KeyCode.K))
                {


                Mohamednull.Play();
            }
        }
    }
}





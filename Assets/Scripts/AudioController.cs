﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(endZoneClip, transform.position, 1f);
    }
    public AudioClip endZoneClip;


}

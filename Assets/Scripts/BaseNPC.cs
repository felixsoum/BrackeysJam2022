using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour
{
    [SerializeField] GameObject visualObject;
    Camera mainCamera;

    protected virtual void Start()
    {
        mainCamera = Camera.main;    
    }

    protected virtual void Update()
    {
        visualObject.transform.forward = mainCamera.transform.forward;
    }

    internal virtual void OnBeerHit()
    {

    }
}

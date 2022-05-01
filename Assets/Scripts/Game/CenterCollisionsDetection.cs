using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCollisionsDetection : MonoBehaviour
{
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var manager = gameManager.GetComponent<GameManager>();

        manager.OnSheepInCenter();
    }
}
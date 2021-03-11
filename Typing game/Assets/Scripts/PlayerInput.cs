using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Manager _manager;

    private void Start()
    {
        _manager = GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char key in Input.inputString)
        {
            _manager.TypeLetter(key);
        }
    }
}

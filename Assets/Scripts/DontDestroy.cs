using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _dontDestroy = new List<GameObject>();
    
    void Awake() 
    {
        foreach (var i in _dontDestroy)
        {
            DontDestroyOnLoad (i.gameObject);
        }
    }

    public void DestroyAll()
    {
        foreach (var i in _dontDestroy)
        {
            Destroy(i.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    private Camera _camera;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
        
        _camera = Camera.main;
    }

    public Vector2 PropsToUI()
    {
        Vector2 screen = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

        return _camera.ScreenToWorldPoint(screen);
    }
}

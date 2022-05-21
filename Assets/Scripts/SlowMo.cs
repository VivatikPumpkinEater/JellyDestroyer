using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    [SerializeField] private GameObject _slowMotionEffect = null;
    [SerializeField] private int _timeScaling = 4;

    private SlowMoStatus _status = SlowMoStatus.Off;
    
    private float _slowMoValue = 0;

    private void Awake()
    {
        _slowMoValue = 1f / _timeScaling;
        
        _slowMotionEffect.SetActive(false);
        
        Debug.Log(Application.platform);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                Debug.Log("Pause");
                switch (_status)
                {
                    case SlowMoStatus.On:
                        OnApplicationPause(false);
                        break;
                    case SlowMoStatus.Off:
                        OnApplicationPause(true);
                        break;
                }
            }
        }
    }

    public void OnSlowMotion()
    {
        if(_status != SlowMoStatus.On)
        {
            Time.timeScale = _slowMoValue;
            Time.fixedDeltaTime *= _slowMoValue;

            _slowMotionEffect.SetActive(true);

            _status = SlowMoStatus.On;
        }
    }

    public void DisableSlowMotion()
    {
        if(_status != SlowMoStatus.Off)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime /= _slowMoValue;

            _slowMotionEffect.SetActive(false);

            _status = SlowMoStatus.Off;
        }
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        switch (pauseStatus)
        {
            case true:
                OnSlowMotion();
                break;
            case false:
                DisableSlowMotion();
                break;
        }
    }
}

public enum SlowMoStatus
{
    On,
    Off
}

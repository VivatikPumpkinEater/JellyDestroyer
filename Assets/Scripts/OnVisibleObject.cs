using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnVisibleObject : MonoBehaviour
{
    public System.Action<bool> Visible;
    
    private void OnBecameVisible()
    {
        Visible?.Invoke(true);
    }

    private void OnBecameInvisible()
    {
        Visible?.Invoke(false);
    }
}

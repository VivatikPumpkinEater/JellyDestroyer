﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameWithGen : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene(2);
    }
}

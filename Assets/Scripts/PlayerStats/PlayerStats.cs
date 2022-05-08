using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject
{
    [field: SerializeField] public string Name = String.Empty;
    [field: SerializeField] public int Cost = Int32.MinValue;
    [field: SerializeField] public int Health = 3;
    [field: SerializeField] public int Damage = 1;
    [field: SerializeField] public GameObject PlayerPrefab = null;
    [field: SerializeField] public GameObject PlayerDecoration = null;

    [field: SerializeField] public bool Acquired = false;
}

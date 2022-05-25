using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Save")]
public class PlayerSaveData : ScriptableObject
{
    [field: SerializeField] public PlayerStats ActiveCharacter = null;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Save")]
public class PlayerSaveData : ScriptableObject
{
    [field: SerializeField] public PlayerStats ActiveCharacter = null;
    [field: SerializeField] public List<PlayerStats> AcquiredSkinsList = new List<PlayerStats>();
    [field: SerializeField] public int Money = 0;
}

[Serializable]
public struct AcquiredSkins
{
    public string Name;
    public PlayerStats PlayerStats;
}

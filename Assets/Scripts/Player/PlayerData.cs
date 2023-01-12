using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ObjectsData/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float startSpeed;
    public float startDps;
    public float startRadius;
    public GameObject prefab;
}

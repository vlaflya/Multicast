using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ObjectsData/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int health;
    public GameObject prefab;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Create EnemyData")]
public class EnemyData : UnitData
{
    [SerializeField] private List<ActionPatterns> _actionPatterns = new List<ActionPatterns>();
    [SerializeField] protected Enemy _prefab;

    public Enemy Prefab { get => _prefab; }
    public List<ActionPatterns> ActionPatterns { get => _actionPatterns; }
}

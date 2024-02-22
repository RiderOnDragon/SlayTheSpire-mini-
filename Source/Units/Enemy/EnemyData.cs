using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Create EnemyData")]
public class EnemyData : UnitData
{
    [SerializeField] private List<AbilityValidator.AbilityStruct> _actionPatterns = new List<AbilityValidator.AbilityStruct>();
    [SerializeField] protected Enemy _prefab;

    private List<Ability> _abilities = new List<Ability>();

    public Enemy Prefab { get => _prefab; }
    public List<Ability> ActionPatterns { get => _abilities; }

    private void OnValidate()
    {
        _abilities = AbilityValidator.Validate(_actionPatterns);
    }
}

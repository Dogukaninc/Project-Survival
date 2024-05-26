using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy/Config")]
public class EnemyConfig : ScriptableObject
{
    public float detectionRange;
    public float attackRange;
}

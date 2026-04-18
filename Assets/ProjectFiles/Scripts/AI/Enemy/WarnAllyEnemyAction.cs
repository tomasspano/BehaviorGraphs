using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WarnAllyEnemyAction", story: "[Self] alerts [AllyEnemies] and [PlayerAlly]", category: "Action", id: "683fdc4c180304d029e03a30f4da93aa")]
public partial class WarnAllyEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> AllyEnemies;
    [SerializeReference] public BlackboardVariable<GameObject> PlayerAlly;
    
    protected override Status OnUpdate()
    {
        if (AllyEnemies.Value != null)
        {
            foreach (var enemy in AllyEnemies.Value)
            {
                var agent = enemy.GetComponent<BehaviorGraphAgent>();
                agent.BlackboardReference.SetVariableValue("AllyEnemyState", AllyEnemyState.Patrol);
            }
        }
        var ally = PlayerAlly.Value.GetComponent<BehaviorGraphAgent>();
        ally.BlackboardReference.SetVariableValue("PlayerAllyState", PlayerAllyState.Follow);
        
        return Status.Success;
    }
}
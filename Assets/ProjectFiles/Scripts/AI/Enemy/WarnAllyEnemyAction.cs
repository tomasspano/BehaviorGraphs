using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WarnAllyEnemyAction", story: "[Self] alerts [AllyEnemy]", category: "Action", id: "683fdc4c180304d029e03a30f4da93aa")]
public partial class WarnAllyEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> AllyEnemy;
    protected override Status OnUpdate()
    {
        var agent = AllyEnemy.Value.GetComponent<BehaviorGraphAgent>();
        if (agent != null)
        {
            agent.Graph.BlackboardReference.SetVariableValue("AllyEnemyState", AllyEnemyState.Patrol);
            return Status.Success;
        }
        return Status.Failure;
    }
}
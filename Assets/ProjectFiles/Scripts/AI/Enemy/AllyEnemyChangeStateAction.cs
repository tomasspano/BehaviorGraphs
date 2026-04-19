using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AllyEnemyChangeStateAction", story: "State has changed", category: "Action", id: "c0658b078705751611ee0b1b1874bcf6")]
public partial class AllyEnemyChangeStateAction : Action
{
    [SerializeReference] public BlackboardVariable<AllyEnemyState> CurrentState;
    [SerializeReference] public BlackboardVariable<AllyEnemyState> OriginalState;

   protected override Status OnUpdate()
    {
        return CurrentState.Value !=  OriginalState.Value ? Status.Success : Status.Running;
    }

}


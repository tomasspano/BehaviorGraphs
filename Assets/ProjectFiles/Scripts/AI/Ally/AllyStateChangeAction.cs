using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AllyStateChangeAction", story: "State has changed", category: "Action", id: "8011ba2bcda9203b4e2e1f64845ce6d5")]
public partial class AllyStateChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<PlayerAllyState> CurrentState;
    [SerializeReference] public BlackboardVariable<PlayerAllyState> OriginalState;
    [SerializeReference] public BlackboardVariable<bool> IsCrouching;
    private bool wasCrouching;

    protected override Status OnStart()
    {
        wasCrouching = IsCrouching.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CurrentState.Value != OriginalState.Value || IsCrouching.Value != wasCrouching)
        {
            return Status.Success;
        }
        return Status.Running;
    }
}


using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetectTargetAction", story: "[Sensor] is detecting", category: "Action", id: "148ee3052e4a3a96aa631859523de07e")]
public partial class DetectTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<SensorSystem> Sensor;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        //busca un posible objetivo hasta que lo detecta
        GameObject possibleTarget = Sensor.Value.SearchForTarget();
        
        //si no hay objetivo y encuentro algo, lo hace objetivo
        if (Target.Value == null && possibleTarget != null)
        {
            //se encontró algo
            Target.Value = possibleTarget;
            return Status.Success;
        }
        //si hay objetivo pero lo he perdido
        if (Target.Value != null && possibleTarget == null)
        {
            Target.Value = null;
            return Status.Failure;
        }
        return Status.Running;
    }
}


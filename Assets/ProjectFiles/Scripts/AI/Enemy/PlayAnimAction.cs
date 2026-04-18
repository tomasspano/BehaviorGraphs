using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayAnimAction", story: "Animator plays animation in parameter", category: "Action", id: "50a71d8a986e160f81a6052d9d0414fa")]
public partial class PlayAnimAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> Animator;
    [SerializeReference] public BlackboardVariable<string> ParameterName;
    
    private float animLength; //duración de la animación
    private float timer;
    
    protected override Status OnStart()
    {
        Animator.Value.SetTrigger(ParameterName.Value);
        timer = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        //obtengo la longitud de la animación ejecutándose
        animLength = Animator.Value.GetCurrentAnimatorStateInfo(0).length;
        timer += Time.deltaTime;
        return timer >= animLength ? Status.Success : Status.Running;
    }
}


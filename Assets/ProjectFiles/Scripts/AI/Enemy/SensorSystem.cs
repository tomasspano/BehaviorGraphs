using System;
using UnityEngine;

public class SensorSystem : MonoBehaviour
{
    [field: SerializeField] public float SensorRadius { get; private set; }
    [field: SerializeField] public float SensorAngle { get; private set; }
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private LayerMask whatIsObstacle;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        SearchForTarget();
    }

    public GameObject SearchForTarget()
    {
        Collider[] results = Physics.OverlapSphere(transform.position, SensorRadius, whatIsTarget);

        if (results.Length <= 0) return null;
        Vector3 directionToTarget = results[0].transform.position - transform.position;

        if (!(Vector3.Angle(transform.forward, directionToTarget) <= SensorAngle / 2)) return null;
       
        if (!Physics.Raycast(transform.position, directionToTarget, directionToTarget.magnitude,whatIsObstacle)) ;
        {
            return results[0].gameObject;
        }

    }

    public Vector3 DirFromAngle(float angle, bool relativeToFront)
    {
        if (relativeToFront)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    
}
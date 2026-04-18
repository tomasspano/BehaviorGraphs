using System;
using UnityEditor;
using UnityEngine;

namespace SensorEditor
{
    [CustomEditor(typeof(SensorSystem))]
    public class SensorSystemEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            SensorSystem sensor = (SensorSystem) target;
            Handles.color = Color.white;
            Handles.DrawWireArc (sensor.transform.position, Vector3.up, Vector3.forward, 360, sensor.SensorRadius);
            Vector3 viewAngleA = sensor.DirFromAngle (-sensor.SensorAngle / 2, true);
            Vector3 viewAngleB = sensor.DirFromAngle (sensor.SensorAngle / 2, true);

            Handles.DrawLine (sensor.transform.position, sensor.transform.position + viewAngleA * sensor.SensorRadius);
            Handles.DrawLine (sensor.transform.position, sensor.transform.position + viewAngleB * sensor.SensorRadius);
        }
    }
}
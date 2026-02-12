using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Route))]
public class RouteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Route route = (Route)target;

        if (GUILayout.Button("Add Segment"))
        {
            route.AddSegment();
        }
    }

    void OnSceneGUI()
    {
        Route route = (Route)target;

        if (route.controlPoints == null)
            return;

        for (int i = 0; i < route.controlPoints.Count; i++)
        {
            Transform t = route.controlPoints[i];
            if (t == null) continue;

            // Allow editing in Scene View
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(t.position, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(t, "Move Point");
                t.position = newPos;
            }
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
public class GjkPolygonShapeCollider : MonoBehaviour, IVertexShape
{
    [SerializeField] private Vector2[] vertices;
    public Color DebugColor = Color.green;
    public Vector2 GetSupportPoint(Vector2 direction)
    {
        Vector2 worldDirection = direction.normalized;
        float maxDot = float.NegativeInfinity;
        Vector2 bestPoint = Vector2.zero;

        foreach (var localVertex in vertices)
        {
            Vector2 worldVertex = transform.TransformPoint(localVertex);
            float dot = Vector2.Dot(worldVertex, worldDirection);
            if (dot > maxDot)
            {
                maxDot = dot;
                bestPoint = worldVertex;
            }
        }
        return bestPoint;
    }
    public void OnShapeTriggerEnter(GjkPolygonShapeCollider other)
    {
        DebugColor = Color.red;
    }
    private void OnDrawGizmos()
    {
        if (vertices == null || vertices.Length < 2) return;

        Gizmos.color = DebugColor;


        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 localStart = vertices[i];
            Vector2 localend = vertices[(i + 1) % vertices.Length];
            Vector2 worldStart = transform.TransformPoint(localStart);
            Vector2 worldEnd = transform.TransformPoint(localend);
            Gizmos.DrawLine(worldStart, worldEnd);
        }
    }
}

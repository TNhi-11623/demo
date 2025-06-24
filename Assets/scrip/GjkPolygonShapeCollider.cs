using UnityEngine;

public class GjkPolygonShapeCollider : MonoBehaviour, IVertexShape
{
    [SerializeField] private Vector2[] vertices;
    public Color DebugColor = Color.green;
    public Vector2 Support(Vector2 direction)
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
        DebugColor = DebugColor.red;
    }
    private void OnDrawGizmos()
    {
        if (vertices == null || _vertices.Length < 2) return;

        Gizmos.color = DebugColor;


        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector2 localStart = _vertices[i];
            Vector2 localend = _vertices[(i + 1) % _vertices.Length];
            Vector2 worldStart = transform.TransformPoint(localStart);
            Vector2 worldEnd = transform.TransformPoint(localend);
            Gizmos.DrawLine(worldStart, worldEnd);
        }
    }
}

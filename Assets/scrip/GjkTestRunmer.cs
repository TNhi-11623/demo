using UnityEngine;

public class GjkTestRunmer : MonoBehaviour
{
    public GjkPolygonShapeCollider[] shape;
    public void Update()
    {
        for (int i = 0; i < shape.Length; i++)
        {
            for (int j = i + 1; j < shape.Length; j++)
            {
                var shapeA = shape[i];
                var shapeB = shape[j];
                if (GJKPhysic2D.IsColliding(shape[i], shape[j]))
                {
                    shape[i].OnShapeTriggerEnter(shape[A]);
                    shape[j].OnShapeTriggerEnter(shape[B]);
                }
                else
                {
                    shape[i].DebugColor = Color.green;
                    shape[j].DebugColor = Color.green;
                }
            }
        }
    }
}

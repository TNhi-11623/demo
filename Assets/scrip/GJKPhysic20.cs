using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GJKPhysic2D : MonoBehaviour
{
    public const int MaxIterations = 30;
    public static bool IsColliding(IVertexShape shapeA, IVertexShape shapeB)
    {
        Vector2 direction = Random.insideUnitCircle.normalized; // Initial random direction
        List<Vector2> simplex = new List<Vector2>();

        Vector2 point = support(shapeA, shapeB, direction);
        simplex.Add(point);
        direction = -point; // Initial direction towards the origin
        for (int i = 0; i < MaxIterations; i++)
        {
            point = support(shapeA, shapeB, direction);
            if (Vector2.Dot(point, direction) <= 0)
            {
                return false; // No collision
            }


            simplex.Add(point);

            if (UpdateSimplexAndDirection(ref simplex, ref direction))
            {
                return true; // Collision detected
            }

        }
        return false; // No collision after max iterations
    }
    private static bool UpdateSimplexAndDirection(ref List<Vector2> simplex, ref Vector2 direction)
    {
        if (simplex.Count == 2)
        {
            Vector2 a = simplex[1];
            Vector2 b = simplex[0];
            Vector2 ab = b - a;
            Vector2 ao = -a;

            direction = Vector2.Perpendicular(ab);
            if (Vector2.Dot(direction, ao) > 0)
            {
                simplex.RemoveAt(1); // Remove the point that is not needed
            }
            else
            {
                direction = -direction; // Reverse direction
                simplex.RemoveAt(0); // Remove the other point
            }
           
        }
        else if (simplex.Count == 3)
        {
            Vector2 a = simplex[2];
            Vector2 b = simplex[1];
            Vector2 c = simplex[0];

            Vector2 ab = b - a;
            Vector2 ac = c - a;
            Vector2 ao = -a;

            Vector2 acPerp = Vector2.Perpendicular(ac);
            if (Vector2.Dot(acPerp, ao) > 0)
            {
                simplex.RemoveAt(1);
                direction = acPerp;
            }
            else
            {
                Vector2 abPerp = Vector2.Perpendicular(ab);
                if (Vector2.Dot(abPerp, ao) > 0)
                {
                    simplex.RemoveAt(1);
                    direction = abPerp;
                }
                else
                {
                    return true; // Origin is inside the triangle
                }
            }

        }
        return false; // Continue to the next iteration
    }
    private static Vector2 support(IVertexShape shapeA, IVertexShape shapeB, Vector2 direction)
    {
        Vector2 pointA = shapeA.GetSupportPoint(direction);
        Vector2 pointB = shapeB.GetSupportPoint(-direction);
        return pointA - pointB;
    }
    private static float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }
}

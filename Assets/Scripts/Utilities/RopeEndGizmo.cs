using UnityEngine;
using System.Collections;

public class RopeEndGizmo : MonoBehaviour
{

    float gizmoSize = 0.3f;
    Color gizmoColor = Color.green;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(this.transform.position, gizmoSize);
    }

    public void Setup(float size, Color c)
    {
        gizmoSize = size;
        gizmoColor = c;
    }

}

using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{

    [HideInInspector]
    public  GameObject[] sections;
    //public GameObject endPoint;
    public float ropeLength = 5f;

    public bool usePhysics = true;
    public bool generateColliders = false;
    public bool fixedEnd = false;

    //public float additionalLength;
    public float density = 1f;
    public float colliderRadius = 0.3f;
    public float jointForce = 5f;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.green;
    [HideInInspector]
    public LayerMask ignoreCollision;

    [Range(1, 8)]
    public int subdivisionsPerUnit = 1;

    //Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, 0.3f);

        //if(endPoint)
        //    Gizmos.DrawSphere(endPoint.transform.position, 0.3f);

    }

    public void GenerateRope()
    {
        Vector3 direction = fixedEnd ? Vector3.right : Vector3.down;//(endPoint.transform.position - this.transform.position).normalized;
        float length = ropeLength; //(endPoint.transform.position - this.transform.position).magnitude + additionalLength;
        int sectionCount = Mathf.RoundToInt(Mathf.Ceil(length)) * subdivisionsPerUnit;
        float deltaSectionH = length / (sectionCount - 1);

        sections = new GameObject[sectionCount];

        for (int i = 0; i < sectionCount; i++)
        {
            sections[i] = new GameObject();
            sections[i].name = "RopeSec" + i.ToString();
            sections[i].transform.SetParent(gameObject.transform);
            sections[i].transform.position = transform.position + direction*i*deltaSectionH;

            if (!usePhysics)
                continue;

            Rigidbody r = sections[i].AddComponent<Rigidbody>();
            r.useGravity = (i == 0) ? false : true;
            r.mass = density;

            if (i == 0)
                r.constraints = RigidbodyConstraints.FreezePosition;
            else if(i < sectionCount)
            {
                HingeJoint hjt = sections[i].AddComponent<HingeJoint>();
                hjt.connectedBody = sections[i - 1].GetComponent<Rigidbody>();
                hjt.axis = Vector3.right + Vector3.forward;//Vector3.one;
                
                hjt.useMotor = true;
                JointMotor hjmt = new JointMotor();
                hjmt.targetVelocity = 0f;
                hjmt.force = jointForce;
                hjt.motor = hjmt;

                if(i == sectionCount - 1 && fixedEnd)
                    r.constraints = RigidbodyConstraints.FreezePosition;
            }

            if (generateColliders && (i != 0 || (i != sectionCount - 1 && fixedEnd)))
            {
                SphereCollider sc = sections[i].AddComponent<SphereCollider>();
                sc.radius = colliderRadius;                
            }

            if (i == 0 || i == sectionCount - 1)
            {
                sections[i].AddComponent<RopeEndGizmo>().Setup(gizmoSize, gizmoColor);
            }
   
        }

        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(sectionCount);
        for (int i = 0; i < sectionCount; i++)
        {
            lr.SetPosition(i, sections[i].transform.position);
        }
    }

    /*
    //Complicated math stuff
    public static const float E = 2.71828f;

    public static float CosH(float rad)
    {
        return (Mathf.Pow(E, rad) + Mathf.Pow(E, -rad)) / 2;
    }

    public static float Catenary(float x, float density, float tension)
    {
        float tbyd = tension / density;
        return (tbyd * CosH(x/tbyd)) - tbyd;
    }
    */

    public void ClearRope()
    {
        if (sections == null)
            return;

        foreach (GameObject go in sections)
        {
            DestroyImmediate(go);
        }

        GetComponent<LineRenderer>().SetVertexCount(0);
    }

    public void BakeRope()
    {
        ClearRope();
        GenerateRope();
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        for (int i = 0; i < sections.Length; i++)
        {
            lr.SetPosition(i, sections[i].transform.position);
        }
    }
}

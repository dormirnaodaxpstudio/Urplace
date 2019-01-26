using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private Camera thisCamera;

    void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = thisCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - thisCamera.ViewportToWorldPoint(new Vector3(0.3f, 0.35f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
#define DEBUGMODE

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PrototypeCharacterControllerv2 : MonoBehaviour
{

    #region Types

    private struct RaycastOrigins
    {
        public Vector3 topLeft;
        public Vector3 bottomLeft;
        public Vector3 bottomRight;
    }

    public class CharacterCollisionState
    {
        public bool right;
        public bool left;
        public bool up;
        public bool down;
        public bool wasGrounded;
        public bool becameGrounded;
        public bool isGoingDownSlope;
        public float slopeAngle;

        public bool IsColliding()
        {
            return right || left || up || down;
        }

        public void Reset()
        {
            right = left = up = down = becameGrounded = isGoingDownSlope = false;
            slopeAngle = 0f;
        }

    }

    #endregion

    #region Properties

    [SerializeField]
    [Range(0.001f, 0.3f)]
    private float skinWidth = 0.02f;

    [SerializeField]
    private float jumpingThreshold = 0.07f;

    public bool usePhysics = false;

    public LayerMask groundMask = 0;

    [Range(0f, 90f)]
    public float slopeAngleLimit = 30f;

    public AnimationCurve slopeSpeedModifier = new AnimationCurve(new Keyframe(-90, 1.3f), new Keyframe(0f, 1f), new Keyframe(90, 0f));

    [Range(2, 10)]
    public int horizontalRayCount = 2;

    [Range(2, 10)]
    public int verticalRayCount = 2;

    [HideInInspector]
    [NonSerialized]
    public Collider cCollider;
    [HideInInspector]
    [NonSerialized]
    public Rigidbody cRigidody;

    [HideInInspector]
    [NonSerialized]
    public CharacterCollisionState collisionState = new CharacterCollisionState();
    [HideInInspector]
    [NonSerialized]
    public Vector3 velocity = Vector3.zero;

    public bool isGrounded { get { return collisionState.down; } }

    #endregion

    private RaycastOrigins _raycastOrigins;

    private RaycastHit _hit;

    private List<RaycastHit> _currentRaycastHits = new List<RaycastHit>();

    private float _verticalRayDistance;
    private float _horizontalRayDistance;

    private bool _isGoingUpSlope;

    private float skinWidthFloatFactor = 0.001f;

    #region Behaviour

    void Awake()
    {
        cRigidody = GetComponent<Rigidbody>();
        cCollider = GetComponent<Collider>();

        CalculateRayDistance();
    }

    [System.Diagnostics.Conditional("DEBUGMODE")]
    private void DrawRay(Vector3 origin, Vector3 direction, Color color)
    {
        Debug.DrawRay(origin, direction, color);
    }

    #region Publics

    public void Move(Vector3 offset)
    {

        collisionState.wasGrounded = isGrounded;

        collisionState.Reset();
        _currentRaycastHits.Clear();
        _isGoingUpSlope = false;

        CalculateOrigins();

        if (offset.y < 0f && collisionState.wasGrounded)
            HandleVerticalSlope(ref offset);

        if (offset.x != 0)
            HorizontalMovement(ref offset);

        if (offset.y != 0)
            VerticalMovement(ref offset);

        //Move
        if (usePhysics)
        {
            cRigidody.MovePosition(offset);
            velocity = cRigidody.velocity;
        }
        else
        {
            transform.Translate(offset, Space.World);
            if (Time.deltaTime > 0)
                velocity = offset / Time.deltaTime;
        }

        if (!collisionState.wasGrounded && collisionState.down)
            collisionState.becameGrounded = true;

        if (_isGoingUpSlope)
            velocity.y = 0;

    }

    public void CalculateRayDistance()
    {
        float colliderHeight = cCollider.bounds.size.y * Mathf.Abs(transform.localScale.y) - (2 * skinWidth);
        _verticalRayDistance = colliderHeight / (horizontalRayCount - 1);

        float colliderWidth = cCollider.bounds.size.x * Mathf.Abs(transform.localScale.x) - (2 * skinWidth);
        _horizontalRayDistance = colliderWidth / (verticalRayCount - 1);
    }

    #endregion

    #region Privates

    private void CalculateOrigins()
    {
        Bounds originBounds = cCollider.bounds;
        originBounds.Expand(-2 * skinWidth);

        _raycastOrigins.topLeft = new Vector2(originBounds.min.x, originBounds.max.y);
        _raycastOrigins.bottomLeft = originBounds.min;
        _raycastOrigins.bottomRight = new Vector2(originBounds.max.x, originBounds.min.y);
    }

    private void HorizontalMovement(ref Vector3 offset) {
        bool goingRight = offset.x > 0;
        float rayDistance = Mathf.Abs(offset.x) + skinWidth;
        Vector3 rayDirection = goingRight ? Vector3.right : Vector3.left;
        Vector3 rayOrigin = goingRight ? _raycastOrigins.bottomRight : _raycastOrigins.bottomLeft;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector3 ray = new Vector3(rayOrigin.x, rayOrigin.y + i * _verticalRayDistance);

            DrawRay(ray, rayDirection * rayDistance, Color.green);

            bool hitFlag = Physics.Raycast(ray, rayDirection, out _hit, rayDistance, groundMask);

            if (hitFlag)
            {

                if (i == 0 && HandleHorizontalSlope(ref offset, Vector3.Angle(_hit.normal, Vector3.up))) {
                    _currentRaycastHits.Add(_hit);
                    break;
                }

                offset.x = _hit.point.x - ray.x;
                rayDistance = Mathf.Abs(offset.x);

                if (goingRight)
                {
                    offset.x -= skinWidth;
                    collisionState.right = true;
                }
                else
                {
                    offset.x += skinWidth;
                    collisionState.left = true;
                }

                _currentRaycastHits.Add(_hit);

                if (rayDistance < skinWidth + skinWidthFloatFactor)
                    break;
            }
        }
    }

    private bool HandleHorizontalSlope(ref Vector3 offset, float angle)
    {
        if(Mathf.RoundToInt(angle) >= 90f)
            return false;

        if (angle < slopeAngleLimit)
        {
            if (offset.y < jumpingThreshold)
            {
                offset.x *= slopeSpeedModifier.Evaluate(angle);
                offset.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * offset.x);            
                _isGoingUpSlope = true;
                collisionState.down = true;
            }
        }
        else
        {
            offset.x = 0f;
        }

        return true;
    }

    private void VerticalMovement(ref Vector3 offset)
    {
        bool goingUp = offset.y > 0;
        float rayDistance = Mathf.Abs(offset.y) + skinWidth;
        Vector3 rayDirection = goingUp ? Vector3.up : Vector3.down;
        Vector3 rayOrigin = goingUp ? _raycastOrigins.topLeft : _raycastOrigins.bottomLeft;

        rayOrigin.x += offset.x;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 ray = new Vector3(rayOrigin.x + i * _horizontalRayDistance, rayOrigin.y);

            DrawRay(ray, rayDirection * rayDistance, Color.green);

            bool hitFlag = Physics.Raycast(ray, rayDirection, out _hit, rayDistance, groundMask);

            if (hitFlag)
            {
                offset.y = _hit.point.y - ray.y;
                rayDistance = Mathf.Abs(offset.y);

                if (goingUp)
                {
                    offset.y -= skinWidth;
                    collisionState.up = true;
                }
                else
                {
                    offset.y += skinWidth;
                    collisionState.down = true;
                }

                _currentRaycastHits.Add(_hit);

                if (!goingUp && offset.y > 0.00001f)
                    _isGoingUpSlope = true;

                if (rayDistance < skinWidth + skinWidthFloatFactor)
                    break;
            }
        }
    }

    private void HandleVerticalSlope(ref Vector3 offset)
    {
        float _slopeLimitTangent = Mathf.Tan( slopeAngleLimit * Mathf.Deg2Rad );

        float colliderCenter = (_raycastOrigins.bottomLeft.x + _raycastOrigins.bottomRight.x) / 2f;
        Vector3 rayDirection = -Vector3.up;

        float slopeCheckRayDistance = _slopeLimitTangent * (_raycastOrigins.bottomRight.x - colliderCenter);

        Vector3 slopeCheckRay = new Vector3(colliderCenter, _raycastOrigins.bottomLeft.y);
        DrawRay(slopeCheckRay, rayDirection * slopeCheckRayDistance, Color.red);

        if (Physics.Raycast(slopeCheckRay, rayDirection, out _hit, slopeCheckRayDistance, groundMask))
        {
            float angle = Vector3.Angle(_hit.normal, Vector3.up);

            if (angle == 0)
                return;

            bool downSlope = Mathf.Sign(_hit.normal.x) == Math.Sign(offset.x);
            if (downSlope)
            {
                offset.x *= slopeSpeedModifier.Evaluate(angle); 
                offset.y = _hit.point.y - slopeCheckRay.y - skinWidth;             
                collisionState.isGoingDownSlope = true;
                collisionState.slopeAngle = angle;
            }
        }
    }

    #endregion

    #endregion

}

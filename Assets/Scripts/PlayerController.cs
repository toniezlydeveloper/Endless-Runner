using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum Position
    {
        Left, Middle, Right
    }

    [SerializeField] private Transform collisionPoint;
    [SerializeField] private float positionDifference;
    [SerializeField] private float crouchDuration;
    [SerializeField] private float jumpDuration;
    [SerializeField] private Animator animator;

    private Position _position = Position.Middle;
    private float _nextInputTime;

    private static readonly Vector3 CrouchCollisionPoint = Vector3.up * 0.125f;
    private static readonly Vector3 JumpCollisionPoint = Vector3.up * 2f;
    private static readonly Vector3 DefaultCollisionPoint = Vector3.up;
    
    private static readonly int CrouchHash = Animator.StringToHash("Crouch");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GoLeft();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            GoRight();
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Crouch();
        }
    }

    private void GoLeft()
    {
        switch (_position)
        {
            case Position.Left:
                break;
            case Position.Middle:
                transform.position += Vector3.left * positionDifference;
                _position = Position.Left;
                break;
            case Position.Right:
                transform.position += Vector3.left * positionDifference;
                _position = Position.Middle;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GoRight()
    {
        switch (_position)
        {
            case Position.Left:
                transform.position += Vector3.right * positionDifference;
                _position = Position.Middle;
                break;
            case Position.Middle:
                transform.position += Vector3.right * positionDifference;
                _position = Position.Right;
                break;
            case Position.Right:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Jump()
    {
        if (!CanReadInput())
        {
            return;
        }
        
        Animate(JumpHash);
        TimestampInput(jumpDuration);
        MoveCollisionPoint(JumpCollisionPoint);
        Invoke(nameof(RestoreCollisionPoint), jumpDuration);
    }

    private void Crouch()
    {
        if (!CanReadInput())
        {
            return;
        }
        
        Animate(CrouchHash);
        TimestampInput(crouchDuration);
        MoveCollisionPoint(CrouchCollisionPoint);
        Invoke(nameof(RestoreCollisionPoint), crouchDuration);
    }

    private bool CanReadInput() => _nextInputTime < Time.time;

    private void TimestampInput(float duration) => _nextInputTime = Time.time + duration;
    
    private void MoveCollisionPoint(Vector3 point) => collisionPoint.localPosition = point;

    private void RestoreCollisionPoint() => collisionPoint.localPosition = DefaultCollisionPoint;

    private void Animate(int hash) => animator?.SetTrigger(hash);
}
using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    private State _currentState;
    private State _previousState;
    private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private enum State
    {
        Idle,
        Walking,
        Attacking
    }

    void Update()
    {
        if(_navMeshAgent.velocity.magnitude > 1f)
        {
            _currentState = State.Walking;
        }
        else
        {
            _currentState = State.Idle;
        }

        if(_currentState != _previousState)
        {
            StateChecker();
            _previousState = _currentState;
        }
    }

    private void StateChecker()
    {
        switch(_currentState)
        {
            case State.Idle:
                StartIdling();
                break;
            case State.Walking:
                StartWalking();
                break;
            case State.Attacking:
                StartAttacking();
                break;
        }
    }

    private void StartIdling()
    {
        _animator.CrossFadeInFixedTime("IdleBreathing", 0.2f);
    }

    private void StartWalking()
    {
        _animator.CrossFadeInFixedTime("Walking", 0.2f);
    }

    private void StartAttacking()
    {
        
    }

}

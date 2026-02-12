using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    private State _currentState;
    private State _previousState;
    private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Coroutine _currentIdleCoroutine;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentState = State.Null;
    }

    private enum State
    {
        Null,
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
                if (_currentIdleCoroutine != null)
                {
                    StopCoroutine(_currentIdleCoroutine);
                }
                break;
            case State.Attacking:
                StartAttacking();
                break;
        }
    }

    private void StartIdling()
    {
        _currentIdleCoroutine = StartCoroutine(AlternateIdleAnimations());
    }

    IEnumerator AlternateIdleAnimations()
    {
        while(true)
        {
            _animator.CrossFadeInFixedTime("IdleBreathing", 0.2f);
            yield return new WaitForSeconds(4f);
            print("In Idle state");
            _animator.CrossFadeInFixedTime("IdleStretching", 0.2f);
            yield return new WaitForSeconds(5.3f);
        }
    }

    private void StartWalking()
    {
        _animator.CrossFadeInFixedTime("Walking", 0.2f);
    }

    private void StartAttacking()
    {
        
    }

}

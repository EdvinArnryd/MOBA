using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public State _currentState;
    private State _previousState;
    private Animator _animator;
    public float _attackRange = 50f;

    public bool _isAttackMode;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Coroutine _currentAnimationCoroutine;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentState = State.Null;
    }

    public enum State
    {
        Null,
        Idle,
        Walking,
        Attacking
    }

    void Update()
    {
        if(_isAttackMode && _navMeshAgent.velocity.magnitude < 1f)
        {
            _currentState = State.Attacking;
        }
        else if(_navMeshAgent.velocity.magnitude > 1f)
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
                if (_currentAnimationCoroutine != null)
                {
                    StopCoroutine(_currentAnimationCoroutine);
                }
                break;
            case State.Attacking:
                StartAttacking();
                break;
        }
    }

    private void StartIdling()
    {
        _currentAnimationCoroutine = StartCoroutine(AlternateIdleAnimations());
    }

    IEnumerator AlternateIdleAnimations()
    {
        while(true)
        {
            // Use base Idle at start
            _animator.CrossFadeInFixedTime("IdleBreathing", 0.2f);
            yield return new WaitForSeconds(4f);

            int random = Random.Range(1, 3);

            if(random == 1)
            {
                _animator.CrossFadeInFixedTime("IdleStretching", 0.2f);
                yield return new WaitForSeconds(5.3f);
            }
            else if(random == 2)
            {
                _animator.CrossFadeInFixedTime("IdleRoar", 0.2f);
                yield return new WaitForSeconds(5.4f);
            }
            else
            {
                _animator.CrossFadeInFixedTime("IdleBreathing", 0.2f);
                yield return new WaitForSeconds(4f);
            }
        }
    }

    private void StartWalking()
    {
        _animator.CrossFadeInFixedTime("Walking", 0.2f);
    }

    private void StartAttacking()
    {
        _animator.CrossFadeInFixedTime("Attacking", 0.2f);
    }

}

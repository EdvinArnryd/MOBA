using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class OldMover : MonoBehaviour
{
    public InputActionReference _move;
    private NavMeshAgent _navMeshAgent;
    private RaycastHit _hit;
    private bool _hasHit;

    [SerializeField] private StateManager _stateManager;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(_hasHit)
        {
            _navMeshAgent.destination = _hit.point;
        }
    }

    void OnEnable()
    {
        _move.action.started += Move;
    }

    void OnDisable()
    {
        _move.action.started -= Move;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        _hasHit = Physics.Raycast(ray, out _hit);
        if(_hit.collider.CompareTag("Enemy"))
        {
            _stateManager._isAttackMode = true;
            _navMeshAgent.stoppingDistance = _stateManager._attackRange;
        }
        else
        {
            _stateManager._isAttackMode = false;
            _navMeshAgent.stoppingDistance = 0;
        }
    }
}

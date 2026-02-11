using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    public InputActionReference _move;
    private NavMeshAgent _navMeshAgent;
    private RaycastHit _hit;
    private bool _hasHit;

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
    }
}

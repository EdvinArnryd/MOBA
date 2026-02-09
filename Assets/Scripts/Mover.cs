using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    public InputActionReference _move;
    private NavMeshAgent _navMeshAgent;

    private Ray _lastRay;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        // Debug.DrawRay(_lastRay.origin, _lastRay.direction * 100);
        _navMeshAgent.destination = _target.transform.position;
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
        print("Click move pressed!");
    }
}

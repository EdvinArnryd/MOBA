using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    void Update()
    {
        transform.position = _target.transform.position;
    }
}

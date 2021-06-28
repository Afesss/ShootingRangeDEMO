using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(Rigidbody))]
public class Shape : MonoBehaviour
{
    public event System.Action OnTriggered;


    private float maxDistance = 50f;

    [Inject] private ShapeSettings shapeSettings;

    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        OnTriggered?.Invoke();
        gameObject.SetActive(false);
    }
    public void AddImpulse(Vector3 projectilePosition)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
        var direction = (transform.position - projectilePosition);
        var sqrMagnitude = direction.sqrMagnitude;
        _rigidbody.AddForce(direction.normalized * Mathf.InverseLerp(maxDistance * maxDistance, 0, sqrMagnitude) *
            shapeSettings.impulseForce, ForceMode.Impulse);
    }
}

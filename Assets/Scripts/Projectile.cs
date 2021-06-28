using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject boom;

    [Inject] private ProjectileSettings projectileSettings;

    public bool IsExploded { get; private set; }

    private float startColliderRadius;
    private Vector3 startPos;

    private Rigidbody _rigidbody;
    
    private void Start()
    {
        boom.SetActive(false);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        startColliderRadius = sphereCollider.radius;
    }
    private void OnEnable()
    {
        startPos = transform.position;
        IsExploded = false;
    }
    private void Update()
    {
        if(!IsExploded && (startPos - transform.position).sqrMagnitude > projectileSettings.maxDistanceToExpolison *
            projectileSettings.maxDistanceToExpolison)
        {
            IsExploded = true;
            Explode();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!IsExploded)
        Explode();
    }
    public void Impulse(Vector3 direction)
    {
        meshRenderer.enabled = true;
        sphereCollider.radius = startColliderRadius;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * projectileSettings.shotForce, ForceMode.Impulse);
    }
    
    public void Explode()
    {
        IsExploded = true;
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        _rigidbody.velocity = Vector3.zero;
        boom.SetActive(true);
        meshRenderer.enabled = false;
        while (sphereCollider.radius < projectileSettings.explosionRange)
        {
            sphereCollider.radius += projectileSettings.explosionRange * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
        boom.SetActive(false);
        IsExploded = false;
    }
}

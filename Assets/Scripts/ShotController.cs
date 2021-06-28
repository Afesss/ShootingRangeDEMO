using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ShotController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Inject] private ProjectileSettings projectileSettings;

    private Projectile cube;
    private Projectile sphere;
    public Projectile CurrentProjectile { get; private set; }
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        cube = Instantiate(projectileSettings.cube, transform).GetComponent<Projectile>();
        cube.gameObject.SetActive(false);
        sphere = Instantiate(projectileSettings.sphere, transform).GetComponent<Projectile>();
        sphere.gameObject.SetActive(false);
    }


    public void Shot()
    {
        if(cube.gameObject.activeSelf || sphere.gameObject.activeSelf)
        {
            return;
        }

        CurrentProjectile = Random.Range(0, 2) == 0 ? cube : sphere;
        CurrentProjectile.transform.position = mainCamera.transform.position;
        CurrentProjectile.gameObject.SetActive(true);
        Vector3 direction = (target.position - CurrentProjectile.transform.position).normalized;
        StartCoroutine(WaitToShot(direction));
    }
    private IEnumerator WaitToShot(Vector3 direction)
    {
        yield return new WaitForFixedUpdate();
        CurrentProjectile.Impulse(direction);
    }
}

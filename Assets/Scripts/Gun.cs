using UnityEngine;

public class Gun : MonoBehaviour
{
  public ParticleSystem bulletFiredEffect;
  public GameObject bulletPrefab;
  public Transform firePoint;
  public float bulletSpeed = 20f;

  void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      Shoot();
      Debug.Log("sadasdas");
    }
  }
  void Shoot()
  {
    if (bulletFiredEffect != null)
      bulletFiredEffect.Play();

    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    rb.linearVelocity = firePoint.forward * bulletSpeed;
    Destroy(bullet, 3f);

    // if (bulletFiredEffect != null)
    //   bulletFiredEffect.Stop();
  }
}

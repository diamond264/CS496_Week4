using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class ShotgunShooting : MonoBehaviour
{
    public int damagePerShot = 30;
    public float timeBetweenBullets = 1f;
    public float range = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 6.0f;
    public int shotgunPellets = 4;
    public float maxBulletSpreadAngle = 15.0f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.12f;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    /*

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        for (int i=0;i<shotgunPellets;i++)
        {
            if (i==2) Debug.Log("FUCK");
            Vector3 fireDirection = transform.forward;
            Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
            Quaternion randomRotation = Random.rotation;
            fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, maxBulletSpreadAngle));
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = fireDirection;

            if (Physics.Raycast(shootRay.origin, fireRotation * Vector3.forward, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }        
    }*/

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();


        shootRay.origin = bulletSpawn.position;
        shootRay.direction = transform.forward;

        for (int i = 0; i < shotgunPellets; i++)
        {
            Quaternion bulletRot = transform.rotation;
            bulletRot.z += Random.Range(-maxBulletSpreadAngle, maxBulletSpreadAngle);
            bulletRot.x += Random.Range(-maxBulletSpreadAngle, maxBulletSpreadAngle);
            bulletRot.y += Random.Range(-maxBulletSpreadAngle, maxBulletSpreadAngle);
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Vector3 a = transform.forward;
            a = bulletRot * a;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            Destroy(bullet, 5);
        }
    }
}

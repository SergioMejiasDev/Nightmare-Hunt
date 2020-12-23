using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that ensures that the player can shoot.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int shotDamage = 10;
    [SerializeField] float cadency = 0.1f;
    [SerializeField] float range = 25.0f;
    [SerializeField] float shotDuration = 0.1f;
    float timer;

    Ray shotRay = new Ray();
    RaycastHit shotHit;
    int shotMask;
    [SerializeField] ParticleSystem gunParticles = null;
    AudioSource audioSource;
    [SerializeField] AudioClip disparoClip = null;
    [SerializeField] Transform shootPoint = null;
    [SerializeField] LineRenderer shotLine = null;

    void Start()
    {
        shotMask = LayerMask.GetMask("Shootable");
        audioSource = GetComponent<AudioSource>();

        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > cadency)
        {
            if ((Input.GetButton("Fire1")) && (Time.timeScale == 1))
            {
                Shoot();
            }
        }

        if (timer > cadency * shotDuration)
        {
            shotLine.enabled = false;
        }
    }

    /// <summary>
    /// Function called every time the player shoots.
    /// </summary>
    void Shoot()
    {
        timer = 0.0f;

        audioSource.clip = disparoClip;
        audioSource.Play();

        gunParticles.Stop();
        gunParticles.Play();

        shotLine.enabled = true;
        shotLine.SetPosition(0, shotLine.transform.position);

        shotRay.origin = shootPoint.position;
        shotRay.direction = shootPoint.forward;

        if (Physics.Raycast(shotRay, out shotHit, range, shotMask))
        {
            if(shotHit.collider.GetComponent<EnemyHealth>())
            {
                shotHit.collider.GetComponent<EnemyHealth>().TakeDamage(shotDamage);
            }

            shotLine.SetPosition(1, shotHit.point);
        }
        
        else
        {
            shotLine.SetPosition(1, shotRay.origin + shotRay.direction * range);
        }
    }
}

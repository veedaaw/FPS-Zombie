using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public float range = 100f;
    public float impactForce = 100f;
    public ParticleSystem fireBallParticle;

    public Animator anim;
    private bool isScoped = false;
    public GameObject scopeOverlay;

    PlayerStat targetStat;
    CharacterCombat combat;

    AudioSource audio;

    RaycastHit hitInfo;

    // this camera only shows the weapon
    public GameObject weaponCamera;

    // this is the main camera - culling masks excludes gun layer
    public Camera mainCamera;

    // when zoom in, change the FOV of the main camera
    public float scopedFieldOfView = 15f;
    public float originalFOV;

    void Start()
    {
        combat = GetComponentInParent<CharacterCombat>();
        originalFOV = mainCamera.fieldOfView;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // mouse-left
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // mouse-right
        if (Input.GetButtonDown("Fire2"))
        {
            Scoped();
        }
    }

    // shooting with the weapon
    void Shoot()
    {
        fireBallParticle.Play();
        audio.Play();

        if(Physics.Raycast(weaponCamera.transform.position, weaponCamera.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
            // add force to the target
            if (hitInfo.rigidbody != null && hitInfo.collider.tag != "Enemy")
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }

            targetStat = hitInfo.transform.gameObject.GetComponent<PlayerStat>();
            if (targetStat != null)
            {
                Debug.Log("Enemy's Health:" + targetStat.currentHealth);
                combat.Attack(targetStat);
            }
        }

    }

    // when the weapon is scoped
    void Scoped()
    {
        isScoped = !isScoped;
        anim.SetBool("Scoped", isScoped);

        if (isScoped)
        {
            StartCoroutine(OnScoped());
        }
        else
        {
           OnUnscoped();
        }
    }

    IEnumerator OnScoped()
    {
        // change this amount based on the animator transition amount
        yield return new WaitForSeconds(0.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        mainCamera.fieldOfView = scopedFieldOfView;
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = originalFOV;
    }

}

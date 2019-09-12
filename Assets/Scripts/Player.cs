using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [Header("Projectile")]
    [SerializeField] GameObject laserbeam;
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] float projectilePeriodDelay = 2f;
    [Header("Player")]
    [SerializeField] GameObject particleExplosion;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip pewSound;
    [SerializeField] [Range(0, 1)] float pewSoundVolume = 0.5f;
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] public int health = 100;
    [SerializeField] float moveSpeed=10f;
    [SerializeField] float padding = 1f;
    LoadScenes scene;
    
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine fireCorutine;
	void Start () {
        MoveBoundaries();
        scene=FindObjectOfType<LoadScenes>();
       
    }
    
  

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        DamageDealer damagedealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damagedealer){ return; }
        ProccessHit(damagedealer);
    }

    private void ProccessHit(DamageDealer damagedealer)
    {
        health-= damagedealer.getDamage();
        damagedealer.Hit();

        Debug.Log(health);
        if (health <= 0)
        {
            Die();
           
        }

    }

    public void Die()
    {
        
        AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        GameObject explosion = Instantiate(particleExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        scene.LoadGameoverScene();
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           fireCorutine=StartCoroutine(FireContinuously());
           

        }
         if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCorutine);
        }
    }
    IEnumerator FireContinuously() {
        while (true)
        {
            GameObject laser = Instantiate(laserbeam,transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(pewSound, Camera.main.transform.position,pewSoundVolume);
            yield return new WaitForSeconds(projectilePeriodDelay);
        }
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") *Time.deltaTime*moveSpeed;
        var newXpos =Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin,yMax);
        transform.position = new Vector2(newXpos,newYpos);
        
    }
    private void MoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}

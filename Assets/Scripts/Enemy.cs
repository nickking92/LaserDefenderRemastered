using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] public int health = 100;
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] AudioClip pewSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    int damageDealt;
    [SerializeField] GameObject particleExplosion;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShot = 0.2f;
    [SerializeField] float maxTimeBetweenShot = 3f;
    [SerializeField] GameObject enemyLaser;
    [SerializeField]  float projectileSpeed=10f;
    [SerializeField]int scoreValue=150;
    DamageDealer damagedealer;
    WaveConfig waveConfig;
    Player p;

    void Start () {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        
	}
	
	// Update is called once per frame
	void Update () {
        CountDownandShoot();
	}

    private void CountDownandShoot()
    {
        shotCounter-=Time.deltaTime;
        if (shotCounter <= -0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity =new Vector2(0,-projectileSpeed);
        AudioSource.PlayClipAtPoint(pewSound, Camera.main.transform.position);
    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(other.gameObject);
        damagedealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damagedealer) { return; }
        ProccessHit(damagedealer);

    }

    private void ProccessHit(DamageDealer damageDealer)
    {

        health -= damagedealer.getDamage();

        damagedealer.Hit();
        //Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position,deathSoundVolume);
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddScore(scoreValue);
        GameObject explosion = Instantiate(particleExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
    }
}

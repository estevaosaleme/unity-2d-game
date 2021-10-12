﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{

    public float fullHealth;
    public GameObject deathFX;
    public AudioClip playerHurt;
    public AudioClip playerAddHealth;

    float currentHealth;

    playerController controlMovement;

    public AudioClip playerDeathSound;
    AudioSource playerAS;

    //HUD variables
    public Slider healthSlider;
    public Image damageScreen;

    bool damaged = false;
    Color damagedColour = new Color(0f, 0f, 0f, 0.5f);
    float smoothColour = 5f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        controlMovement = GetComponent<playerController>();

        // HUD Initilisation
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        damaged = false;

        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged){
            damageScreen.color = damagedColour;
        } else {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColour*Time.deltaTime);
        }
        damaged = false;
    }

    public void addDamage(float damage){
        if (damage <= 0) 
            return;

        currentHealth -= damage;
        
		// two alternatives for playing sound
        //playerAS.clip = playerHurt;
        //playerAS.Play();
        playerAS.PlayOneShot(playerHurt);

        healthSlider.value = currentHealth;
        damaged = true;

        if (currentHealth <= 0) {
            makeDead();
        }
    }

    public void addHealth(float healthAmount){
        currentHealth += healthAmount;
        if (currentHealth > fullHealth){
            currentHealth = fullHealth;
        }
        playerAS.PlayOneShot(playerAddHealth);
        healthSlider.value = currentHealth;
    }

    public void makeDead(){
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSound, transform.position, 10f);
    }
}

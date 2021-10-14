using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    public GameObject deathFX;
    public Slider enemySlider; 

    public bool drops;
    public GameObject theDrop;
    public int dropChance;

    public float enemyMaxHealth;

    public AudioClip deathKnell;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDamage(float damage){

        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage; 
        enemySlider.value = currentHealth;

        if (currentHealth <= 0){
            makeDead();
        }
    }

    void makeDead(){
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);
        AudioSource.PlayClipAtPoint(deathKnell, transform.position, 10f);
        if (drops && Random.Range(1,100) <= dropChance){
            Instantiate(theDrop, transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyTag))] //installa automaticamente lo script Enemytag per forzare l'utilizzo di entrambi gli script
public class Health : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [Tooltip("Adds amount to Max HP on death")]
    [SerializeField] int difficultyUp = 3; //aumenta la vita di Enemy
    [SerializeField] AudioClip demolish;

    new AudioSource audio;
    int currentHP = 0;

    EnemyTag enemy;
    void OnEnable()
    {
        currentHP = maxHP;
    }

    void Start()
    {
        enemy = GetComponent<EnemyTag>();
        audio = GetComponentInParent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        { audio.mute = !audio.mute; }
    }


    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHP--;
        audio.PlayOneShot(demolish);

        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            maxHP += difficultyUp;
            enemy.Reward();
        }
    }
}
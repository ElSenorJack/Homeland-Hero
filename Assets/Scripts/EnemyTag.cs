using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    [SerializeField] int moneyReward = 25;
    [SerializeField] int moneySteal = 25;
    [SerializeField] AudioClip villageDamage;

    new AudioSource audio;

    Money money;
    void Start()
    {
        money = FindObjectOfType<Money>();
        audio = GetComponentInParent<AudioSource>();
    }


    public void Reward()
    {
        if (money == null) {  return; }
        money.Deposit(moneyReward);
    }

    public void Steal()
    {
        if (money == null) { return; }
        money.Withdraw(moneySteal);
        audio.PlayOneShot(villageDamage);
    }
}

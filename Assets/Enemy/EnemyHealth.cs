using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoint = 5;
    [Tooltip("amount added to maxHitPoint when enemy dies")]
    [SerializeField] int DificaltyRamp = 1;
    [SerializeField] int RewardImprove = 2;
    int currentHitPoint = 0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoint = maxHitPoint;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        HitProcess();
    }

    void HitProcess()
    {
        currentHitPoint--;

        if (currentHitPoint <= 0)
        {
            enemy.RewardGold();
            maxHitPoint += DificaltyRamp;
            enemy.SetGoldReward(RewardImprove);
            
            gameObject.SetActive(false);
            enemy.checkWin();
        }
    }
}

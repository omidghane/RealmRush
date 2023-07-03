using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLocator : MonoBehaviour
{
    Transform target;
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectTileParticles;
    [SerializeField] float range = 15f;
    
    void Update()
    {
        findClosestTarget();
        aimWeapon();
    }

    void findClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                maxDistance = targetDistance;
                closestEnemy = enemy.transform;
            }
        }
        target = closestEnemy;
    }

    void aimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        if(targetDistance <= range)
        {
            attack(true);
        }
        else{
            attack(false);
        }

        weapon.LookAt(target);
    }

    void attack(bool isActive)
    {
        var emissionModule = projectTileParticles.emission;
        emissionModule.enabled = isActive;
    }
}

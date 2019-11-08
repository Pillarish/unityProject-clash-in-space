using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class MinionCombat : MonoBehaviour
{
    CharacterStats myStats;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public bool attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f) {
            attackCooldown = 1f / attackSpeed;

            return true;
        }

        return false;
    }
}

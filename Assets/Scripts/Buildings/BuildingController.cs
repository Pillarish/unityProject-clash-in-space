using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingController : MonoBehaviour
{
    protected CharacterStats myStats;
    public GameObject currentTarget;
    public GameObject healthBarContainer;
    public bool rotatable = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        
        if (currentTarget == null) {
            findTarget();
        }

        // if we have a target
        if (currentTarget != null) {

            // check if we're in range
            float distance = Vector3.Distance(this.transform.position, currentTarget.transform.position);

            if (distance > myStats.range.GetValue()) {
                currentTarget = null;
            }
            else {
                if (rotatable) {
                    faceTarget();
                }
                attackTarget();
            }
        }

        if (myStats.currentHealth < myStats.maxHealth && !healthBarContainer.activeSelf) {
            healthBarContainer.transform.gameObject.SetActive(true);
        }
        else if (myStats.currentHealth == myStats.maxHealth && healthBarContainer.activeSelf) {
            healthBarContainer.transform.gameObject.SetActive(false);
        }
    }

    protected void findTarget()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("minion");

        foreach(GameObject possibleTarget in possibleTargets) {

            float distance = Vector3.Distance(this.transform.position, possibleTarget.transform.position);
            if (distance <= myStats.range.GetValue()) {
                currentTarget = possibleTarget;
                break;
            }
        }
    }

    protected virtual void attackTarget()
    {
        MinionCombat combat = GetComponent<MinionCombat>();
        if (combat != null) {
            combat.attack(currentTarget.GetComponent<CharacterStats>());
        }
    }

    protected virtual void faceTarget()
    {
        // override me pls
    }
}

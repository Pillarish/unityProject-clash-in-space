using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class MinionController : MonoBehaviour
{
    public GameObject currentTarget;
    public Vector3 targetPosition;
    public string preferredTarget;
    public NavMeshAgent agent;
    protected CharacterStats myStats;
    public string[] buildingTags = new string[]{"building", "defence", "storage", "resource", "builder"};
    public GameObject healthBarContainer;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        agent.stoppingDistance = myStats.range.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        // if we don't already have a target then find one
        if (!currentTarget) {

            findBestTarget();
        }

       // if we have a target
        if (currentTarget) {

            // check if we're in range
            float distance = Vector3.Distance(this.transform.position, targetPosition);
            if (distance <= myStats.range.GetValue()) {
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

    protected virtual void attackTarget()
    {
        MinionCombat combat = GetComponent<MinionCombat>();
        if (combat != null) {
            bool fired = combat.attack(currentTarget.GetComponent<CharacterStats>());

            if (fired) {
                CharacterStats targetStats = currentTarget.GetComponent<CharacterStats>();
                targetStats.takeDamage(targetStats.damage.GetValue());
            }
        }
    }

    private void findBestTarget()
    {
        GameObject[] possibleTargets = new GameObject[0];

        // if this character has a preferredTarget then find one
        if (!string.IsNullOrEmpty(preferredTarget)) {
            possibleTargets = GameObject.FindGameObjectsWithTag(preferredTarget);
        }
        else {
            foreach(string buildingTag in buildingTags) {
                GameObject[] extraObjs = GameObject.FindGameObjectsWithTag(buildingTag);
                possibleTargets = possibleTargets.Concat(extraObjs).ToArray();
            }
        }

        if (possibleTargets.Length == 0) {
            return;
        }

        // get closest
        currentTarget = pickBestTarget(possibleTargets);

        Collider targetCollider = currentTarget.GetComponent<Collider>();
        targetPosition = targetCollider.ClosestPoint(this.transform.position);

        agent.SetDestination(targetPosition);

        // FIXME: This should be more intelligent, 
        // like checking that the path isn't insane, 
        // checking for walls to destroy to make it easier, 
        // checking for alternate targets
    }

    private GameObject pickBestTarget(GameObject[] possibleTargets)
    {
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = this.transform.position;
        foreach (GameObject target in possibleTargets)
        {
            float dist = Vector3.Distance(target.transform.position, currentPos);
            if (dist < minDist)
            {
                closest = target;
                minDist = dist;
            }
        }
        return closest;
    }

}

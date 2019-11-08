using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : BuildingController
{
    public GameObject bulletPrefab;
    public GameObject projectileSpawn;
    public Animation fireAnimation;

    protected override void Start()
    {
        base.Start();
        rotatable = true;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void attackTarget()
    {
        MinionCombat combat = GetComponent<MinionCombat>();
        if (combat != null) {
            bool fired = combat.attack(currentTarget.GetComponent<CharacterStats>());

            if (fired) {
                // fire bullet
                GameObject b = Instantiate(bulletPrefab, projectileSpawn.transform.position, Quaternion.identity);

                BulletController controller = b.GetComponent<BulletController>();
                controller.target = currentTarget;
                controller.damage = myStats.damage.GetValue();

                // animate the firing in some way
            }
        }
    }

    protected override void faceTarget()
    {
        Vector3 targetPos = currentTarget.transform.position;

        Vector3 targetDir = targetPos - this.transform.position;

        // The step size is equal to speed times frame time.
        float step = 6f * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(this.transform.forward, targetDir, step, 0.0f);

        // Move our position a step closer to the target.
        this.transform.rotation = Quaternion.LookRotation(newDir);
    }
}

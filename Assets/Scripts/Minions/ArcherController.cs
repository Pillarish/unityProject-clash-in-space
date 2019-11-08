using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MinionController
{
    public GameObject bulletPrefab;
    public GameObject projectileSpawn;

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
            }
        }
    }
}

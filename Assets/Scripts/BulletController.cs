using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public int damage;
    Vector3 dirNormalized;
    
    void Start() {
        dirNormalized = (target.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(dirNormalized);

        if (target == null) {
            Destroy(this.gameObject);
            return;
        }

        if(Vector3.Distance(target.transform.position, transform.position) <= 1) {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            targetStats.takeDamage(damage);

            Destroy(this.gameObject);
        } else {
            transform.position = transform.position + dirNormalized * speed * Time.deltaTime;
        }
    }
}

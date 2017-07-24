using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damagePerShot = 20;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        EnemyHealth health = hit.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.TakeDamage(damagePerShot, collision.contacts[0].point);
        }
        if (hit != GameObject.FindGameObjectWithTag("Player")) Destroy(gameObject);
    }
}

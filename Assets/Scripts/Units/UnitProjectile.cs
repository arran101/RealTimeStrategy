using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class UnitProjectile : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float destroyAfterSeconds = 5f;
    [SerializeField] private float launchForce = 10f;

    void Start()
    {
        rb.velocity = transform.forward * launchForce;
    }

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfterSeconds);
    }

    //only calls on server 
    [ServerCallback]
    //did something collide?
    private void OnTriggerEnter(Collider other)
    {
        //does whatever collided have an identity component?
        if (other.TryGetComponent<NetworkIdentity>(out NetworkIdentity networkIdentity))
        {
            //if they belong to the same client return
            if (networkIdentity.connectionToClient == connectionToClient) { return; }
        }

        //if the object has health
        if(other.TryGetComponent<Health>(out Health health))
        {
            //deal damage
            health.DealDamage(damageToDeal);
        }
        //destroy projectile
        DestroySelf();
    }

    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

}

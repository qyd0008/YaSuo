using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Role : MonoBehaviour
{
    protected CharacterStats characterStats;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Rigidbody rig;
    protected Collider coll;

    protected bool isDead;

    protected virtual void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        isDead = false;
    }

    protected bool IsDeath()
    {
        return isDead;
    }

    public void RemoveRigidbody()
    {
        Destroy(rig);
        rig = null;
    }

    public void AddRigidbody(float mass, float drag)
    {
        if (rig == null)
        {
            Debug.Log("AddRigidbody");
            rig = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rig.mass = mass;
            rig.drag = drag;
            rig.angularDrag = 0.05f;
            rig.useGravity = true;
            rig.isKinematic = false;
        }
        else
        {
            rig.mass = mass;
            rig.drag = drag;
        }
    }
}

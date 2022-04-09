using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollState : MonoBehaviour
{
    public Collider MyCollider;
    public UnityEngine.AI.NavMeshAgent Agent;
    public Animator Animator;
    private Rigidbody[] rigidbodies;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    public void ToggleRagdoll(bool bisAnimating) //Переключение рэгдолла
    {
        MyCollider.enabled = bisAnimating;
        Agent.enabled = bisAnimating;
        foreach(Rigidbody ragdollBone in rigidbodies) 
        { 
            ragdollBone.isKinematic = bisAnimating; 
        }
        Animator.enabled = bisAnimating; 
    }
}

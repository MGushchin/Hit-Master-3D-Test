using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public Animator Animator;
    private State currentState;
    public State CurrentState => currentState;

    public void SetState(State nextState)
    {
        Animator.SetBool("Run", false);
        Animator.SetBool("Idle", false);
        currentState = nextState;
        switch (nextState)
        {
            case (State.Idle):
                {
                    Animator.SetBool("Idle", true);
                }
                break;
            case (State.Run):
                {
                    Animator.SetBool("Run", true);
                }
                break;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using util;

public class WanderBehaviour : StateMachineBehaviour
{
    public float walkRadius;
    public GameObject go;
    Vector3 randomDirection;
    float timeUntilDecision;
    public bool WorkDone;
    choice escolha;
    Vector3 finalPosition;
    Vector3 oldpos = Vector3.zero;
    SpriteRenderer sr;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        go = GameObject.FindGameObjectWithTag("Player");
        sr = GameObject.FindGameObjectWithTag("playersprite").GetComponent<SpriteRenderer>();
        sr.flipX = false;
        //timeUntilDecision = Random.Range(1f, 3f) * 60;
        WorkDone = false;
        escolha = null;
        finalPosition = Vector3.zero;
        oldpos = Vector3.zero;
        walkRadius = 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!WorkDone)
        {
            randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += go.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            finalPosition = hit.position;
            go.GetComponent<NavMeshAgent>().destination = finalPosition;

            //Debug.Log("start " + go.transform.position);
            //Debug.Log("end " + finalPosition);

            //GameObject.FindGameObjectWithTag("Respawn").transform.position = finalPosition;

            var conta = (finalPosition - go.transform.position).normalized;
            //Debug.Log("X: " + conta.x + " - Z: " + conta.z);
            sr.flipX = (conta.x > 0 || conta.z <= 0) || (conta.z < 0 || conta.x >= 0);

            escolha = new WeightedRandom(new List<choice> {
                new choice {
                    nome = "IDLE", probabilidade = 60
                }
            }).Escolhe();

            WorkDone = true;
            oldpos = Vector3.zero;
            return;
        }

        if (oldpos == Vector3.zero)
        {
            oldpos = go.transform.position;
            return;
        }

        if (go.transform.position == oldpos)
        {
            WorkDone = false;

            if (escolha != null)
            {
                animator.SetTrigger(escolha.nome);
            }
        }
        else
        {
            oldpos = go.transform.position;
        }

        return;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sr.flipX = false;
        WorkDone = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

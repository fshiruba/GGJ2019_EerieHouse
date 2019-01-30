using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using util;

public class IdleBehaviour : StateMachineBehaviour
{
    float timeUntilDecision;
    NavMeshAgent goNavMesh;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeUntilDecision = Random.Range(2f, 4f) * 60;
        var go = GameObject.FindGameObjectWithTag("Player");
        goNavMesh = go.GetComponent<NavMeshAgent>();
        goNavMesh.isStopped = true;
        goNavMesh.speed = 5;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("IDLING");
        timeUntilDecision--;

        if (timeUntilDecision <= 0)
        {
            timeUntilDecision = Random.Range(1f, 3f) * 60;

            var escolha = new WeightedRandom(new List<choice> {
                new choice{
                    nome="WANDER", probabilidade = 60
                },
                new choice{
                    nome="WAVE", probabilidade = 40
                }
            }).Escolhe();

            //Debug.Log(escolha?.nome);

            if (escolha == null)
            {
                //var outrogo = GameObject.FindGameObjectWithTag("playersprite");
                //var sprite = FindObjectOfType<PersonCoordinator>().BackupSprite;
                //outrogo.GetComponent<SpriteRenderer>().sprite = sprite;
                return;
            }

            animator.SetTrigger(escolha.nome);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        goNavMesh.isStopped = false;
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

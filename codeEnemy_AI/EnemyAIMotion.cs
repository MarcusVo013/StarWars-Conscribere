using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMotion : MonoBehaviour
{
    [SerializeField] string MotionName = "Speed";
    [SerializeField] float MaxTime = 1.0f;
    [SerializeField] float MaxDistance = 1.0f;
    [SerializeField] Transform PlayerPosition;
    NavMeshAgent agent;
    Animator animator;
    float Timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
    }
    private void FindPlayer()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 0 )
        {
            float sqrDistiance = (PlayerPosition.position - agent.destination).sqrMagnitude;
            if(sqrDistiance > MaxDistance ) 
            {
                agent.destination = PlayerPosition.position;                
            }
            Timer = MaxTime;
        }
        animator.SetFloat(MotionName, agent.velocity.magnitude);
    }
}

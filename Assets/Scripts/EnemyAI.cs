using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterCombat))]
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    public float aggroRange = 15f;

    // array of waypoints gameobjects in the game
    public GameObject[] waypoints;
    int index;
    
    bool playerIsVisible = false;
    readonly float angleToSeePlayer = 180f;

    // ref to combat script
    CharacterCombat combat;
    PlayerStat agentStat;

    Animator anim;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        player = PlayerManager.Instance.player;
        combat = GetComponent<CharacterCombat>();
        agentStat = GetComponent<PlayerStat>();

        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        // subscribe to the event in the player state script.
        agentStat.OnHealthReachedZero += Die;

        index = Random.Range(0, waypoints.Length - 1);
        InvokeRepeating("Patrol", 1.0f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled == true)
        {
            Seek();
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }

    }

    // patroling with waypoints
    private void Patrol()
    {
        agent.SetDestination(waypoints[index].transform.position);
        if (index != waypoints.Length - 1)
        {
            index++;
        }
        else
            index = 0;
    }

    // behaviour of the seeking
    private void Seek()
    {
        float distance = Vector3.Distance(agent.transform.position, player.transform.position);

        // if we can see target, follow it then attack it if close.
        if ( distance < aggroRange && CanSeeTarget())
        {
            agent.SetDestination(player.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                audio.Play();
                combat.Attack(player.GetComponent<PlayerStat>());
                anim.SetBool("Attack", true);

            }
        }
    }

    // check to see if we can actually see the target
    bool CanSeeTarget()
    {
        Vector3 direction = player.transform.position - agent.transform.position;
        float angle = Vector3.Angle(agent.transform.forward, direction);
        if (angle < angleToSeePlayer)
        {
            playerIsVisible = true;
        }
        return playerIsVisible;
    }

    public void Die()
    {
        Debug.Log(gameObject.transform.name + " is dead.");

        StartCoroutine(PlayDead());
      
        // add death particle

    }

    private IEnumerator PlayDead()
    {
        anim.SetBool("Dead", true);
       // agent.enabled = false;
        agent.speed = 0f;
        agent.velocity = Vector3.zero;
        agent.acceleration = 0f;

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

   
}

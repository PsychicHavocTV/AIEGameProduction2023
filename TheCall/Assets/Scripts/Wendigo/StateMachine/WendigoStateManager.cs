using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.HighDefinition;

public class WendigoStateManager : MonoBehaviour
{
    [SerializeField]
    GameObject Wendigo;
    public GameObject RaycastOrigin;
    [SerializeField]
    NavMeshAgent nma;
    public GameObject playerRef;
    public PlayerController pController;
    BaseState currentState;
    WendigoRoamingState roamingState = new WendigoRoamingState();
    WendigoChaseState chaseState = new WendigoChaseState();
    public bool timerFinished = false;
    public bool timerRunning = false;
    bool inView = false;
    public int layerMask = 1 << 3;

    private void Start()
    {
        layerMask = ~layerMask;

        // Starting state for the state machine.
        currentState = roamingState;
        roamingState.Wendigo = Wendigo;
        roamingState.nma = nma;
        chaseState.nma = nma;

        pController = playerRef.GetComponent<PlayerController>();

        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        
        currentState.UpdateState(this);
    }

    public void StartChasing()
    {
        currentState = chaseState;
        currentState.EnterState(this);
    }

    public void StartRoaming()
    {
        currentState = roamingState;
        currentState.EnterState(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        //if (currentState == roamingState)
        //{
        //    if (other.tag != "Player" && other.tag != "Ground")
        //    {
        //        if (roamingState.moving == true)
        //        {
        //            roamingState.moving = false;
        //            roamingState.moveAmount = 0;
        //        }
        //    }
        //}
    }

    public void ControlTimer()
    {
        if (timerRunning == false)
        {
            StartCoroutine(ChaseTimer());
            return;
        }
        else
        {
            return;
        }
    }

    public IEnumerator ChaseTimer()
    {
        Debug.Log("Counting Down..");
        timerRunning = true;
        timerFinished = false;
        yield return new WaitForSeconds(5f);
        Debug.Log("Timer Finished...");
        timerFinished = true;
        timerRunning = false;
        RaycastHit hit;
        Vector3 rayDirection = playerRef.transform.position - transform.position;
        if (inView == false)
        {
            StartRoaming();
        }
        StopCoroutine(ChaseTimer());
    }

    public void checkView()
    {
        RaycastHit hit;

        Vector3 rayDirection = playerRef.transform.position - transform.position;
        if ((Vector3.Angle(rayDirection, transform.forward)) < 25)
        {
            if ((Vector3.Angle(rayDirection, transform.forward)) < 25) // Is player within field of view
            {
                if (Physics.Raycast(transform.position, rayDirection, out hit, 30))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        inView = true;
                    }
                }
                else
                {
                    inView = false;
                }

            }
        }
    }
}

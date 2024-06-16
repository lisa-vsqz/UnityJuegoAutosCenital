using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class CompetitorsNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        navMeshAgent.destination = movePositionTransform.position;
    }
}

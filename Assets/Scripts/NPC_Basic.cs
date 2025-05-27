using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NPC_Basic : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    //Componentes
    [SerializeField] private Transform _target;
    Animator _agentAnim;
    NavMeshAgent _agent1;
    private float previousAceleration;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        _agentAnim = GetComponent<Animator>();
        _agent1 = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
       // previousAceleration = _agent1.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        _agent1.destination = _target.position;
        if(_agent1.velocity.magnitude == 0f)
        {
           // _agent1.isStopped = true;
            _agentAnim.SetBool("isWalking", false);
        }
        else
        {
            //_agent1.isStopped = false;
            _agentAnim.SetBool("isWalking", true);
        }

        if(Vector3.Distance(transform.position, _target.position) < 2f)
        {
            _agent1.isStopped = true;
            _agentAnim.SetBool("isWalking", false);
        }
        else
        {
            _agent1.isStopped = false;
        }

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

 
}
    #endregion


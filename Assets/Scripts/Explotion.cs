using System;
using Cinemachine;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    #region Properties
    public event Action OnExplosion;
    [SerializeField] private float _explosionForce = 1000;
    public GameObject Player { get; set; }
    #endregion

    #region Fields
    [SerializeField] private GameObject _explosionPrefab;
    private float _explosionArea=100;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        Player=null;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Effect
            Player = other.gameObject;
            _explosionPrefab.GetComponent<ParticleSystem>().Play();
        }
        ExplosionForce();
        OnExplosion?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _explosionArea);
    }
    private void ExplosionForce()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, _explosionArea);
        for (int i = 0; i < objects.Length; i++)
        {
            Rigidbody objectRB = objects[i].GetComponent<Rigidbody>();
            if (objectRB != null)
            {
                objectRB.AddExplosionForce(_explosionForce, transform.position, _explosionArea);
            }
        }
    }
    #endregion
}

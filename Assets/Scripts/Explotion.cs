using System;
using Cinemachine;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Explotion : MonoBehaviour
{
    #region Properties
    public event Action OnExplosion;
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
            _explosionPrefab.SetActive(true);
            Player = other.gameObject;
            Destroy(_explosionPrefab.gameObject, 2f);
        }

        OnExplosion?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _explosionArea);
    }
    #endregion
}

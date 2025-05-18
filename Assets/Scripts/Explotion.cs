using System;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    #region Properties

    #endregion

    #region Fields
    [SerializeField] private GameObject _explosionPrefab;
    private float _explosionArea;
    #endregion

    #region Unity Callbacks
    void Start()
    {
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
            other.GetComponent<Animator>().enabled = false;
            _explosionPrefab.SetActive(true);
            Destroy(_explosionPrefab.gameObject, 2f);
        }
        Animator playeAnim = other.GetComponentInParent<Animator>();
        if (playeAnim != null)
            playeAnim.enabled = false;

        ExplosionForce();
    }

    private void ExplosionForce()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _explosionArea);
    }
    #endregion
}

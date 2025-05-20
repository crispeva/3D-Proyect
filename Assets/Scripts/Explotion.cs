using System;
using Cinemachine;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    #region Properties
    public event Action OnExplosion;
    #endregion

    #region Fields
    [SerializeField] private GameObject _explosionPrefab;
    private float _explosionArea=100;
    [SerializeField] private Camera _cameraExplotion;
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private GameObject _player=null;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _player=null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            _cameraExplotion.transform.LookAt(_player.transform.position);
            _cameraExplotion.transform.Translate(_cameraExplotion.transform.forward * Time.deltaTime * 2,Space.World);
        }
            
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Cameras
            _cameraMain.enabled = false;
            _cameraExplotion.enabled = true;

            //Effect
            other.GetComponent<Animator>().enabled = false;
            _explosionPrefab.SetActive(true);

            Destroy(_explosionPrefab.gameObject, 2f);
        }
        Animator playeAnim = other.GetComponentInParent<Animator>();
        if (playeAnim != null)
            playeAnim.enabled = false;

            _player = playeAnim.transform.GetChild(1).gameObject;



        //OnExplosion?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _explosionArea);
    }
    #endregion
}

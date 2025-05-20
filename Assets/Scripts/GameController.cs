using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    private Explotion _explosion;
    [SerializeField] private CinemachineVirtualCamera _cameraExplotion;
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private GameObject _player;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
       // _explosion = FindFirstObjectByType<Explotion>(); // Updated to use the recommended method

    }
    void Start()
    {
        //_explosion.OnExplosion += DeathCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void DeathCamera()
    {
        Debug.Log("La cámara de muerte se ha activado");
       // _cameraMain.enabled = false;
        _cameraExplotion.enabled = true;
        _cameraExplotion.Priority = 100;
    }
    #endregion
}

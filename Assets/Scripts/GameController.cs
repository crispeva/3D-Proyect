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
    [SerializeField] private Camera _cameraExplotion;
    [SerializeField] private Camera _cameraMain;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
       _explosion = FindFirstObjectByType<Explotion>(); // Updated to use the recommended method

    }
    void Start()
    {
        _explosion.OnExplosion += Explotion;
    }

    // Update is called once per frame
    void Update()
    {
        OnCameraFollow();
        if (_cameraExplotion.enabled) PlayerRecover();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void Explotion()
    {
        //Cameras
        _cameraMain.enabled = false;
        _cameraExplotion.enabled = true;

        //Animations
        Animator playeAnim = _explosion.Player.GetComponentInParent<Animator>();
        _explosion.Player = playeAnim.transform.GetChild(1).gameObject;
        if (playeAnim != null)
        {
            playeAnim.enabled = false;
        }
    }
    private void OnCameraFollow()
    {
        //Camera follow death
        if (_explosion.Player != null)
        {
            _cameraExplotion.transform.LookAt(_explosion.Player.transform.position);
            _cameraExplotion.transform.Translate(_cameraExplotion.transform.forward * Time.deltaTime * 2, Space.World);
        }
    }
    private void PlayerRecover()
    {
        float distancia = Vector3.Distance(_cameraExplotion.transform.position, _explosion.Player.transform.position);
        Debug.Log(distancia);
        if (distancia < 1.5f)
        {
            _cameraMain.enabled = true;
            _explosion.Player.GetComponentInParent<Animator>().enabled = true;
            _explosion.Player.transform.position = Vector3.zero;
            _cameraExplotion.enabled = false;

            Debug.Log("Hola ratitas");

        }
    }
}
    #endregion


using System;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    private Explotion _explosion;
    [SerializeField] private Camera _cameraExplotion;
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private GameObject _playerRoot;
    float distancia;
    Rigidbody rb;
    CharacterController controller;
    Animator playeAnim;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
       _explosion = FindFirstObjectByType<Explotion>(); // Updated to use the recommended method
       // rb = _playerRoot.GetComponentInChildren<Rigidbody>();
      

    }
    void Start()
    {
        _explosion.OnExplosion += Explotion;
    }

    // Update is called once per frame
    void Update()
    {
        OnCameraFollow();
        if (_cameraExplotion.enabled )
        {

            if (distancia < 3f && rb.linearVelocity.magnitude < 0.5f)
            {
                PlayerRecover();
            }
            else
            {

                distancia = Vector3.Distance(_cameraExplotion.transform.position, _explosion.Player.transform.position);
            }
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void Explotion()
    {
        //Referencias
        rb = _explosion.Player.GetComponentInChildren<Rigidbody>();
        controller = _explosion.Player.GetComponentInChildren<CharacterController>();
        //Cameras
        _cameraMain.enabled = false;
        _cameraExplotion.enabled = true;

        //Animations
        playeAnim = _explosion.Player.GetComponentInParent<Animator>();
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
        if (controller != null)
        {
            controller.enabled = false; // Desactiva el controlador para evitar problemas de colisi�n
        }
        // Mueve el jugador entero (root) a la posici�n del cad�ver
        _playerRoot.transform.position = _explosion.Player.transform.position;
        _playerRoot.transform.rotation = Quaternion.identity; // o mantener rotaci�n de la c�mara

        if (controller != null) controller.enabled = true;
        _cameraMain.enabled = true;
        _cameraExplotion.enabled = false;

        // Reactiva Animator y controles
       
        if (playeAnim != null) playeAnim.enabled = true;

        // Reactiva controlador
        Debug.Log("Jugador recuperado");
    }
}
    #endregion


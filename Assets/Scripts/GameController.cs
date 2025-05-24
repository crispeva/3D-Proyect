using System;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    //Componentes
    Rigidbody rb;
    CharacterController controller;
    Animator playeAnim;
    Transform rootTransform;
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

        if (_cameraExplotion.enabled)
        {
            distancia = Vector3.Distance(_cameraExplotion.transform.position, _explosion.Player.transform.position);
            OnCameraFollow();
            if (distancia < 4f && rb.linearVelocity.magnitude < 2f && rb!=null)
            {
                PlayerRecover();
            }
           // Debug.Log($"Distancia: {distancia}, Velocidad: {rb.linearVelocity.magnitude}");
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void Explotion()
    {
        if (_explosion.Player!=null)
        {
            //Referencias
        rb = _explosion.Player.GetComponentInChildren<Rigidbody>();
        controller = _explosion.Player.GetComponentInChildren<CharacterController>();
        rootTransform = _explosion.Player.GetComponentInParent<Transform>();
        playeAnim = _explosion.Player.GetComponentInParent<Animator>();

        //Cameras
        _cameraMain.enabled = false;
        _cameraExplotion.enabled = true;

        //Animations
        _explosion.Player = playeAnim.transform.GetChild(1).gameObject;
        if (playeAnim != null)
        {
            playeAnim.enabled = false;
        }
            //Desactiva controlador input

        if (_explosion.Player != null) _explosion.Player.GetComponentInParent<PlayerInput>().enabled = false;
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
            Debug.Log("Controlador desactivado");
        }

        // Mueve el jugador entero
        
        rootTransform.position = _explosion.Player.transform.position;
         rootTransform.rotation = Quaternion.identity; // o mantener rotaci�n de la c�mara

        if (controller != null) controller.enabled = true;
        _cameraMain.enabled = true;
        _cameraExplotion.enabled = false;

        // Reactiva Animator y controles

        if (playeAnim != null)
        {
            playeAnim.enabled = true;
        }
        //Desactiva controlador input
        if(_explosion.Player != null) _explosion.Player.GetComponentInParent<PlayerInput>().enabled = true;

        // Reactiva controlador
        Debug.Log("Jugador recuperado");
    }
}
    #endregion


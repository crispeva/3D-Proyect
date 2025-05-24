using System;
using System.Collections;
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
    private bool _isRecovering = false;
    [SerializeField] private GameObject _playerRoot;
    float distancia;
    Rigidbody rb;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
       _explosion = FindFirstObjectByType<Explotion>(); // Updated to use the recommended method
        rb = _playerRoot.GetComponentInChildren<Rigidbody>();

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

            if (distancia < 2f && rb.linearVelocity.magnitude < 0.5f)
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
        _isRecovering = false; // Permite la recuperaci�n tras una nueva explosi�n
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
        CharacterController controller = _playerRoot.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // Desactiva el controlador para evitar problemas de colisi�n
        }
        // Mueve el jugador entero (root) a la posici�n del cad�ver
        _playerRoot.transform.position = _explosion.Player.transform.position;
        _playerRoot.transform.rotation = Quaternion.identity; // o mantener rotaci�n de la c�mara

        Animator anim = _playerRoot.GetComponent<Animator>();
        if (controller != null) controller.enabled = true;
        _cameraMain.enabled = true;
        _cameraExplotion.enabled = false;

        // Reactiva Animator y controles
       
        if (anim != null) anim.enabled = true;

        // Reactiva controlador
        Debug.Log("Jugador recuperado");
    }
}
    #endregion


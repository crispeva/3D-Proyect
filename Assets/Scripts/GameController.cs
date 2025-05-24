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
        if (_cameraExplotion.enabled && !_isRecovering)
        {

            if (distancia < 2f)
            {
                StartCoroutine(PlayerRecover());
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
        _isRecovering = false; // Permite la recuperación tras una nueva explosión
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
    private IEnumerator PlayerRecover()
    {
        _isRecovering = true; // Evita múltiples corrutinas
        yield return new WaitForSeconds(3f);
        CharacterController controller = _playerRoot.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // Desactiva el controlador para evitar problemas de colisión
        }
        // Mueve el jugador entero (root) a la posición del cadáver
        _playerRoot.transform.position = _explosion.Player.transform.position;
        _playerRoot.transform.rotation = Quaternion.identity; // o mantener rotación de la cámara

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


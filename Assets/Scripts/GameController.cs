using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    private Animator _animator;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
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

    #endregion
}

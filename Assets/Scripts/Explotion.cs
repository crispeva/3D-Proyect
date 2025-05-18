using UnityEngine;

public class Explotion : MonoBehaviour
{
    #region Properties

    #endregion

    #region Fields
    [SerializeField] private GameObject _explosionPrefab;
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
            //other.GetComponent<Rigidbody>().isKinematic = true;
            // other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject, 2f);
        }
    }
    #endregion
}

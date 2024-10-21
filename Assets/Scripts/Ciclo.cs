using UnityEngine;

public class Ciclo : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake chamado.");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable chamado.");
    }

    void Start()
    {
        Debug.Log("Start chamado.");
    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate chamado.");
    }

    void Update()
    {
        Debug.Log("Update chamado.");
    }

    void LateUpdate()
    {
        Debug.Log("LateUpdate chamado.");
    }

    void OnDisable()
    {
        Debug.Log("OnDisable chamado.");
    }
}

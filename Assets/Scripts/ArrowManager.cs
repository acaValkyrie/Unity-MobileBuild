using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private GameObject _gameObject;
    private Rigidbody _rb;
    private Vector3 _baseScale;
    void Start()
    {
        _gameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        _rb = this.gameObject.GetComponent<Rigidbody>();
        _baseScale = this.gameObject.transform.localScale;
    }

    private void SetArrowState(Vector3 position, Vector3 direction)
    {
        _gameObject.transform.position = position;
        _gameObject.transform.rotation = Quaternion.LookRotation(direction);
        Vector3 localScale = _gameObject.transform.localScale;
        localScale.z = direction.magnitude * 1.0f;
        _gameObject.transform.localScale =  localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 arrowPos = this.gameObject.transform.position;
        const float maxVelocityMagnitude = 10.0f;
        Vector3 arrowDir = _rb.velocity / maxVelocityMagnitude;
        SetArrowState(arrowPos, arrowDir);
    }
}

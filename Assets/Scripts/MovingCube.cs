using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
[RequireComponent(typeof(Rigidbody))]
public class MovingCube : MonoBehaviour
{
    private Dictionary<DataType, float> _propeties = new();
    
    private Vector3 _startPos;

    private Rigidbody _myRB;
    
    private PoolObject _poolObject;
    
    private delegate void Move();

    private Move _move;

    private void Start()
    {
        _poolObject = GetComponent<PoolObject>();
        _myRB = GetComponent<Rigidbody>();

        _startPos = transform.position;
    }

    private void OnEnable()
    {
        _move = MovingLogic;
    }

    private void FixedUpdate()
    {
        _move?.Invoke();
    }
    
    private void MovingLogic()
    {
        if ((transform.position - _startPos).magnitude >= _propeties[DataType.Distance])
        {
            DisableMe();
        }
        else
        {
            _myRB.AddForce(Vector3.forward * _propeties[DataType.Speed]);
        }
    }

    private void DisableMe()
    {
        _move = null;

        _myRB.velocity = Vector3.zero;

        _poolObject.ReturnToPool();
    }

    public void AssignNewValues(Dictionary<DataType, float> newPropeties)
    {
        _propeties = new Dictionary<DataType, float>(newPropeties);
    }
}

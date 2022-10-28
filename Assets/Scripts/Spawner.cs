using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _defaultDistance;

    private Dictionary<DataType, float> _spawnerData = new()
    {
        {DataType.SpawnTime, 0}
    };
    private Dictionary<DataType, float> _propeties = new()
    {
        {DataType.Speed, 0},
        {DataType.Distance, 0}
    };

    private DataType _newDataType;
    private float _newValue;

    private bool _newData;

    private Pool _pool;

    private void Start()
    {
        ValueChangeAssing.ValueChangedHandler += TransferValues;

        _pool = GetComponent<Pool>();
        
        _spawnerData[DataType.SpawnTime] = _spawnDelay;
        _propeties[DataType.Speed] = _defaultSpeed;
        _propeties[DataType.Distance] = _defaultDistance;
        
        StartCoroutine(SpawnDelay());
    }

    private void OnDisable()
    {
        ValueChangeAssing.ValueChangedHandler -= TransferValues;
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(_spawnerData[DataType.SpawnTime]);
        
        SpawnObject();

        StartCoroutine(SpawnDelay());
    }

    private void SpawnObject()
    {
        Transform myTransform = transform;
        PoolObject newObject = _pool.GetFreeElement(myTransform.position, myTransform.rotation);

        newObject.GetComponent<MovingCube>().AssignNewValues(_propeties);
    }

    private void TransferValues(DataType dataType, float value)
    {
        if (!_spawnerData.ContainsKey(dataType))
        {
            _propeties[dataType] = value;
        }
        else
        {
            _spawnerData[dataType] = value;
        }
    }
}

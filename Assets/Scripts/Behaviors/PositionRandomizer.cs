using UnityEngine;

public class PositionRandomizer
{
    private const float HalfReduceValue = 2;

    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionZ;
    private float _maxPositionZ;
    private float _basePositionY;

    public PositionRandomizer(Transform spawnPlate)
    {
        _minPositionX = spawnPlate.localPosition.x - spawnPlate.localScale.x / HalfReduceValue;
        _maxPositionX = spawnPlate.localPosition.x + spawnPlate.localScale.x / HalfReduceValue;
        _minPositionZ = spawnPlate.localPosition.z - spawnPlate.localScale.z / HalfReduceValue;
        _maxPositionZ = spawnPlate.localPosition.z + spawnPlate.localScale.z / HalfReduceValue;

        _basePositionY = spawnPlate.localPosition.y;
    }

    public Vector3 GetRandomPosition()
    {
        return new Vector3(
            x: Random.Range(_minPositionX, _maxPositionX),
            y: _basePositionY,
            z: Random.Range(_minPositionZ, _maxPositionZ));
    }
}
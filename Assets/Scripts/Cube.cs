using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public MeshRenderer MeshRenderer => _meshRenderer;

    private void OnValidate()
    {
        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void UpdateBeforeSpawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }
}
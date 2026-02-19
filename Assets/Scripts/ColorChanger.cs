using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private readonly Color _baseColor;

    public ColorChanger()
    {
        _baseColor = Color.white;
    }

    public void UpdateColor()
    {
        _meshRenderer.material.color = Random.ColorHSV();
    }

    public void SetBaseColor()
    {
        _meshRenderer.material.color = _baseColor;
    }
}
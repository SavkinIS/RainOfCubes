using UnityEngine;

public class TransparencyChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private Color _baseColor;
    private Material _material;
    private readonly float _minAlpha = 0;

    private void Awake()
    {
        _material = new Material(_meshRenderer.material);
        _meshRenderer.material = _material;
        
        _baseColor = _material.color;
    }
    
    public void UpdateColor(float currentValue, float maxValue)
    {
        if (_material == null)
        {
            return;
        }
        
        currentValue = Mathf.Clamp(currentValue, _minAlpha, maxValue);
        Color newColor = _material.color;
        newColor.a = (currentValue / maxValue);
        
        _material.color = newColor;
    }

    public void SetBaseColor()
    {
        if (_material != null)
        {
            _material.color = _baseColor;
        }
    }
}
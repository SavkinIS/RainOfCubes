using UnityEngine;

public class ColorChanger
{
    private readonly Color _baseColor;

    public ColorChanger()
    {
        _baseColor = Color.white;
    }

    public void UpdateColor(Cube cube)
    {
        cube.MeshRenderer.material.color = Random.ColorHSV();
    }

    public void SetBaseColor(Cube cube)
    {
        cube.MeshRenderer.material.color = _baseColor;
    }
}
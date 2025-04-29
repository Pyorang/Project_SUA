using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Shapes
{
    Circle,
    Triangle,
    Square
}

public enum Colors
{
    Red,
    Blue,
    Green

}

public class ShapeGenerator : Image
{
    private readonly int _segment = 64;
    private Shapes currentShape;
    private Colors currentColor;

    private void Start()
    {
        SetAllDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        if (currentShape == Shapes.Circle)
            MakeCircle(vh, width, height);
        else if (currentShape == Shapes.Triangle)
            MakeTriangle(vh, width, height);
        else if (currentShape == Shapes.Square)
            MakeSquare(vh, width, height);
    }

    public void SetShape(Shapes targetShape)
    {
        currentShape = targetShape;
        SetAllDirty();
    }

    public void SetColor(Colors targetColor)
    {
        switch (targetColor)
        {
            case Colors.Red:
                color = Color.red;
                break;
            case Colors.Blue:
                color = Color.blue;
                break;
            case Colors.Green:
                color = Color.green;
                break;
        }
        SetAllDirty();
    }

    private void MakeSquare(VertexHelper vh, float width, float height)
    {
        vh.AddVert(new Vector2(-width * 0.5f, -height * 0.5f), color, new Vector2(0f, 0f));
        vh.AddVert(new Vector2(-width * 0.5f, height * 0.5f), color, new Vector2(0f, 1f));
        vh.AddVert(new Vector2(width * 0.5f, height * 0.5f), color, new Vector2(1f, 1f));
        vh.AddVert(new Vector2(width * 0.5f, -height * 0.5f), color, new Vector2(1f, 0f));

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }

    private void MakeTriangle(VertexHelper vh, float width, float height)
    {
        vh.AddVert(new Vector2(-width * 0.5f, -height * 0.5f), color, new Vector2(0f, 0f));
        vh.AddVert(new Vector2(0f, height * 0.5f), color, new Vector2(0.5f, 1f));
        vh.AddVert(new Vector2(width * 0.5f, -height * 0.5f), color, new Vector2(1f, 0f));

        vh.AddTriangle(0, 1, 2);
    }

    private void MakeCircle(VertexHelper vh, float width, float height)
    {
        for (int i = 0; i < _segment; ++i)
        {
            float rad = Mathf.PI * 2f * i / _segment;
            float sin = Mathf.Sin(rad);
            float cos = Mathf.Cos(rad);

            Vector2 pos = new Vector2(sin * width * 0.5f, cos * height * 0.5f);
            Vector2 uv = new Vector2((sin + 1f) * 0.5f, (cos + 1f) * 0.5f);
            vh.AddVert(pos, color, uv);
            vh.AddTriangle(i, (i + 1) % _segment, _segment);
        }

        vh.AddVert(new Vector2(0f, 0f), color, new Vector2(0.5f, 0.5f));
    }
}

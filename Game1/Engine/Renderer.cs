using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CUBE_FACES
{
    Top, Bottom,
    Left, Right,
    Front, Back
}

public struct Cube
{
    public static Vector3[] CUBE_VERTICES =
    {
        new Vector3(0, 0, 0),
        new Vector3(2, 0, 0),
        new Vector3(2, 0, 2),
        new Vector3(0, 0, 2),
        new Vector3(0, 2, 0),
        new Vector3(2, 2, 0),
        new Vector3(2, 2, 2),
        new Vector3(0, 2, 2)
    };
    public static Vector3[] CUBE_NORMALS =
    {
        new Vector3(0, 1, 0), new Vector3(0, -1, 0),
        new Vector3(1, 0, 0), new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1), new Vector3(0, 0, -1)
    };
}

public struct VertexPositionColorNormal : IVertexType
{
    public Vector3 Position;
    public Color Color;
    public Vector3 Normal;

    public readonly static VertexDeclaration VertexDeclaration
       = new VertexDeclaration(
           new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
           new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
           new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
           );

    VertexDeclaration IVertexType.VertexDeclaration
    {
        get { return VertexDeclaration; }
    }

}

public class Renderer
{
    private BasicEffect BasicEffect;
    private Camera Camera;
    private GraphicsDevice GraphicsDevice;
    private VertexPositionColorNormal[] vertices;
    private int vertexIndex = 0;

    public void Initialize(GraphicsDevice device, Camera camera)
    {
        GraphicsDevice = device;
        BasicEffect = new BasicEffect(device);
        BasicEffect.EnableDefaultLighting();

        Camera = camera;

    }

    public void CreateCube()
    {
        vertices = new VertexPositionColorNormal[99];

        // Each faces
        for (int i = 0; i < 6; i++)
        {
            CreateFace(i, new Vector3(0, 0, 0));
        }

        BasicEffect.View = Camera.view;
        BasicEffect.Projection = Camera.projection;

        foreach (var pass in BasicEffect.CurrentTechnique.Passes)
        {
            pass.Apply();

           GraphicsDevice.DrawUserPrimitives(
                // We’ll be rendering two trinalges
                PrimitiveType.TriangleList,
                // The array of verts that we want to render
                vertices,
                // The offset, which is 0 since we want to start
                // at the beginning of the floorVerts array
                0,
                // The number of triangles to draw
                12);
        }

        Console.WriteLine("Cube!");
    }

    // Default red.
    private void AddVertex(Vector3 position, int face, Vector3 normal)
    {
        vertices[vertexIndex].Position = position;
        vertices[vertexIndex].Normal = normal;
        vertices[vertexIndex].Color = Color.Red;
    }

    private void CreateFace(int face, Vector3 position)
    {
       
        switch (face)
        {
            case (int)CUBE_FACES.Top:
                AddVertex(position, 4, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 5, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 7, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 5, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 6, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 7, Cube.CUBE_NORMALS[face]);
                break;
            case (int)CUBE_FACES.Bottom:
                AddVertex(position, 1, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 3, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 2, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 1, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 0, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 3, Cube.CUBE_NORMALS[face]);
                break;
            case (int)CUBE_FACES.Left:
                AddVertex(position, 0, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 7, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 3, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 0, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 4, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 7, Cube.CUBE_NORMALS[face]);
                break;
            case (int)CUBE_FACES.Right:
                AddVertex(position, 2, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 5, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 1, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 2, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 6, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 5, Cube.CUBE_NORMALS[face]);
                break;
            case (int)CUBE_FACES.Front:
                AddVertex(position, 3, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 6, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 2, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 3, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 7, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 6, Cube.CUBE_NORMALS[face]);
                break;
            case (int)CUBE_FACES.Back:
                AddVertex(position, 0, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 1, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 5, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 5, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 4, Cube.CUBE_NORMALS[face]);
                AddVertex(position, 0, Cube.CUBE_NORMALS[face]);
                break;
        }
    }
}


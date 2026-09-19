using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

public class ObjImporter
{
    public Mesh ImportFile(string filePath)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> tempNormals = new List<Vector3>();

        string[] lines = File.ReadAllLines(filePath);
        char[] splitChars = { ' ' };

        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed[0] == '#') continue;

            string[] parts = trimmed.Split(splitChars, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) continue;

            switch (parts[0])
            {
                case "v": // 顶点
                    vertices.Add(new Vector3(
                        float.Parse(parts[1], CultureInfo.InvariantCulture),
                        float.Parse(parts[2], CultureInfo.InvariantCulture),
                        float.Parse(parts[3], CultureInfo.InvariantCulture)));
                    break;

                case "vt": // UV
                    uvs.Add(new Vector2(
                        float.Parse(parts[1], CultureInfo.InvariantCulture),
                        float.Parse(parts[2], CultureInfo.InvariantCulture)));
                    break;

                case "vn": // 法线
                    tempNormals.Add(new Vector3(
                        float.Parse(parts[1], CultureInfo.InvariantCulture),
                        float.Parse(parts[2], CultureInfo.InvariantCulture),
                        float.Parse(parts[3], CultureInfo.InvariantCulture)));
                    break;

                case "f": // 面
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] vertData = parts[i].Split('/');
                        int vIndex = int.Parse(vertData[0]) - 1;
                        triangles.Add(vIndex);
                    }
                    break;
            }
        }

        mesh.SetVertices(vertices);
        if (uvs.Count > 0) mesh.SetUVs(0, uvs);
        if (tempNormals.Count > 0) mesh.SetNormals(tempNormals);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
using UnityEngine;
using System.IO;

public class ObjLoader : MonoBehaviour
{
    [Header("OBJ 文件绝对路径，例如 D:/obj数据/test.obj")]
    public string objFilePath = "D:/obj数据/test.obj";

    void Start()
    {
        if (!File.Exists(objFilePath))
        {
            Debug.LogError("找不到文件: " + objFilePath);
            return;
        }

        // 调用 ObjImporter 加载模型
        ObjImporter importer = new ObjImporter();
        Mesh mesh = importer.ImportFile(objFilePath);

        // 创建 GameObject 显示模型
        GameObject model = new GameObject("LoadedOBJModel");
        MeshFilter mf = model.AddComponent<MeshFilter>();
        MeshRenderer mr = model.AddComponent<MeshRenderer>();
        mf.mesh = mesh;

        // 默认材质，避免粉色
        mr.material = new Material(Shader.Find("Standard"));

        // ★★★ 访问顶点（作业要求2）★★★
        Vector3[] vertices = mesh.vertices;
        Debug.Log("===== 模型顶点数量: " + vertices.Length + " =====");
        for (int i = 0; i < Mathf.Min(10, vertices.Length); i++)
        {
            Debug.Log("顶点 " + i + ": " + vertices[i]);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FanShape : MonoBehaviour
{
    public float radius = 5f;
    public float angle = 60f;
    public int segments = 10; // ���׸�Ʈ ���� 0�� �ƴ� ������ �ʱ�ȭ
    public int skipEveryNthSegment = 2; // �� ������ �� ��° ���׸�Ʈ�� �ǳʶ��� ����
    public int wifiSegments = 3; // �������� ���׸�Ʈ ��

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateFanMesh();
    }

    Mesh CreateFanMesh()
    {
        Mesh mesh = new Mesh();

        // vertices �迭�� ũ�⸦ �����մϴ�.
        Vector3[] vertices = new Vector3[(segments + 1) * wifiSegments];
        int[] triangles;

        // triangles �迭�� ũ�⸦ �����մϴ�.
        int triangleCount = (segments * 3 * 2) * (wifiSegments - 1);
        triangles = new int[triangleCount];

        float angleStep = angle / segments;
        float radiusStep = radius / wifiSegments;

        int vertexIndex = 0, triangleIndex = 0;

        for (int j = 0; j < wifiSegments; j++)
        {
            float currentRadius = radiusStep * (j + 1);

            for (int i = 0; i <= segments; i++)
            {
                float currentAngle = angleStep * i;
                vertices[vertexIndex++] = new Vector3(
                    Mathf.Sin(currentAngle * Mathf.Deg2Rad) * currentRadius,
                    0,
                    Mathf.Cos(currentAngle * Mathf.Deg2Rad) * currentRadius
                );

                // ������ �������� ���׸�Ʈ������ �ﰢ���� �������� �ʽ��ϴ�.
                if (j < wifiSegments - 1 && i < segments)
                {
                    int baseIndex = j * (segments + 1) + i;

                    // �� �ﰢ��
                    triangles[triangleIndex++] = baseIndex;
                    triangles[triangleIndex++] = baseIndex + segments + 1;
                    triangles[triangleIndex++] = baseIndex + segments + 2;

                    // �Ʒ� �ﰢ��
                    triangles[triangleIndex++] = baseIndex;
                    triangles[triangleIndex++] = baseIndex + segments + 2;
                    triangles[triangleIndex++] = baseIndex + 1;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}

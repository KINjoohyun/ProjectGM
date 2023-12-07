using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class FanShape : MonoBehaviour
{
    public float radius = 5f;
    public float angle = 90f;
    public int segments = 50;
    public int wifiSegments = 2;

    public Vector3 centerPoint;

    public Mesh sharedMesh;
    private MeshCollider meshCollider;

    void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>(); // MeshCollider ������Ʈ ��������

        sharedMesh = CreateFanMesh();
        meshFilter.mesh = sharedMesh;
        meshCollider.sharedMesh = sharedMesh; // MeshCollider�� �޽� ����

        CalculateCenterPoint();
    }

    Mesh CreateFanMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(segments + 1) * 2]; 
        int[] triangles;

        int triangleCount = segments * 3 * 2;
        triangles = new int[triangleCount];

        float angleStep = angle / segments;
        float innerRadius = radius * ((float)(wifiSegments - 1) / wifiSegments); // ���� �ݰ� ���
        float outerRadius = radius;

        int vertexIndex = 0, triangleIndex = 0;

        // ���� ����
        for (int i = 0; i <= segments; i++)
        {
            //���ϸ��� B�����϶��� 120�������ϰ�
            // ���ϸ��� C�����϶��� 60������ ��� �ﰢ���� �������� �׻� �÷��̾� �����̾�ߵ�



            float currentAngle = angleStep * i + 0f; // 90�� �߰�
            vertices[vertexIndex++] = new Vector3(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad) * innerRadius,
                0,
                Mathf.Sin(currentAngle * Mathf.Deg2Rad) * innerRadius
            );
            vertices[vertexIndex++] = new Vector3(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad) * outerRadius,
                0,
                Mathf.Sin(currentAngle * Mathf.Deg2Rad) * outerRadius
            );
        }

        // �ﰢ�� ����
        for (int i = 0; i < segments; i++)
        {
            int baseIndex = i * 2;
            triangles[triangleIndex++] = baseIndex;
            triangles[triangleIndex++] = baseIndex + 2;
            triangles[triangleIndex++] = baseIndex + 1;

            triangles[triangleIndex++] = baseIndex + 1;
            triangles[triangleIndex++] = baseIndex + 2;
            triangles[triangleIndex++] = baseIndex + 3;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    void OnDrawGizmos()
    {
        if (sharedMesh == null)
        {
            sharedMesh = CreateFanMesh();
        }

        Gizmos.DrawMesh(sharedMesh, transform.position, transform.rotation, transform.localScale);
    }

    public void ToggleMeshRendering(bool isEnabled)
    {
        GetComponent<MeshRenderer>().enabled = isEnabled;
    }

    private void CalculateCenterPoint()
    {
        if (sharedMesh == null)
            return;

        // �޽��� ��� ���ؽ��� ��ȸ�ϸ� �߽��� ���
        Vector3 sum = Vector3.zero;
        foreach (Vector3 vertex in sharedMesh.vertices)
        {
            sum += vertex;
        }
        centerPoint = sum / sharedMesh.vertexCount;


        //// �ٿ��� ���� �����ϰ� �޽��� ��� ���ؽ��� ������� �߽����� ����ϴ� �ɷ� ����
        //if (sharedMesh == null)
        //    return;

        //// �޽��� Bounds�� �̿��� �߽����� ���
        //centerPoint = sharedMesh.bounds.center;
    }

    // �߽��� ������ ��ȯ�ϴ� �޼���
    public Vector3 GetCenterPoint()
    {
        return centerPoint;
    }

    public Vector3 Return() // z�� ��� ����
    {
        if (sharedMesh == null)
        {
            sharedMesh = CreateFanMesh();
        }

        Bounds bounds = sharedMesh.bounds;
        return bounds.size; // bounds.size�� �޽��� �ʺ�
    }
}

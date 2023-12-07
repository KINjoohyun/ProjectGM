using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class FanShape : MonoBehaviour
{
    public float radius = 5f;
    public float angle = 90f;
    public int segments = 50;
    public int wifiSegments = 2;

    private Mesh sharedMesh;
    private MeshCollider meshCollider;

    void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>(); // MeshCollider ������Ʈ ��������

        if (meshCollider == null)
        {
            Debug.Log("MeshCollider ������Ʈ�� ��� �߰��մϴ�.");
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        else
        {
            Debug.Log("MeshCollider ������Ʈ�� �̹� �����մϴ�.");
        }

        sharedMesh = CreateFanMesh();
        meshFilter.mesh = sharedMesh;
        meshCollider.sharedMesh = sharedMesh; // MeshCollider�� �޽� ����
    }

    Mesh CreateFanMesh()
    {
        Mesh mesh = new Mesh();

        // �� ���׸�Ʈ�� ������ ������ �迭
        Vector3[] vertices = new Vector3[(segments + 1) * 2]; // �� ���׸�Ʈ�� ������ ���׸�Ʈ ���� 2��
        int[] triangles;

        // �ﰢ�� �迭�� ũ�� ����
        int triangleCount = segments * 3 * 2;
        triangles = new int[triangleCount];

        float angleStep = angle / segments;
        float innerRadius = radius * ((float)(wifiSegments - 1) / wifiSegments); // ���� �ݰ� ���
        float outerRadius = radius; // �ܺ� �ݰ��� ��ü �ݰ�

        int vertexIndex = 0, triangleIndex = 0;

        // ���� ����
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = angleStep * i + 90f; // 90�� �߰�
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

        //MeshFilter meshFilter = GetComponent<MeshFilter>();

        //// ���ο� �޽ð� ���� �������� �ʾ��� ��쿡�� ����dddd
        //if (meshFilter.sharedMesh == null)
        //{
        //    meshFilter.sharedMesh = CreateFanMesh();
        //}

        //// Gizmos�� �޽� �׸���
        Gizmos.DrawMesh(sharedMesh, transform.position, transform.rotation, transform.localScale);
    }

    //public float GetColliderSize()
    //{
    //    return meshCollider.bounds.size;
    //}

    public float GetColliderSize()
    {
        Vector3 size = meshCollider.bounds.size;

        Debug.LogError(size);

        if (meshCollider == null)
        {
            Debug.LogError("meshCollider�� �Ҵ���� �ʾҽ��ϴ�.");
            return 0f; // �⺻�� ��ȯ
        }

        // ���� �� ���� ���̸� ��ȯ
        return Mathf.Max(size.x, size.y, size.z);
    }

    //public float GetColliderSize()
    //{
    //    if (meshCollider == null)
    //    {
    //        Debug.LogError("meshCollider�� �Ҵ���� �ʾҽ��ϴ�.");
    //        return 0f; // �⺻�� ��ȯ
    //    }

    //    return meshCollider.bounds.size;
    //}


    public void ToggleMeshRendering(bool isEnabled)
    {
        GetComponent<MeshRenderer>().enabled = isEnabled;
    }

    //public float Return()
    //{
    //    float width = radius;
    //    if (angle < 180f)
    //    {
    //        float halfAngleRad = (angle * 0.5f) * Mathf.Deg2Rad;
    //        width = Mathf.Sin(halfAngleRad) * radius * 2f;
    //    }
    //    float height = radius;
    //    float depth = 0; // ��ä���� ��鿡 �����Ƿ� ���̴� 0

    //    Vector3 size = new Vector3(width, height, depth);
    //    return size.magnitude; // ������ ũ��(�밢�� ����) ��ȯ
    //}
}

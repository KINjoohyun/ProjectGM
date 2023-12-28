using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class FanShape : MonoBehaviour
{
    public float radius = 5f;
    public float angle = 90f;
    public int segments = 50;
    public int wifiSegments = 1;

    public Vector3 centerPoint;

    public Mesh sharedMesh;
    private MeshCollider meshCollider;

    private Material material;
    private float startTime;

    public Transform detectedPlayer;
    private Player player;

    public EnemyAI enemyAi;

    public bool isplayerInside = false;

    [Header("�������� Ÿ��")]
    public AttackShape attackShape;

    // ��, �ݿ�, �ﰢ��, ��ä��

    // �׷��� ������ ���Ͱ� �ϰ�

    // �� ��ġ�� ���������� �������� �Ǿ��־ ������

    // ����� �޾ƿ;��ϰ�

    // ���� �����ϵ� �� �޶� ������ ��������� �޶��������� �����ε�

    // �׷��� ������ ������� ��翡 �°� ������ �ص� �ִϸ��̼� �̺�Ʈ�� �ȿ� �ִ��� �ۿ� �ִ���
    // �Ǵ��� �� �ֳ�?

    // �Ǵܱ����� ����ϸ� �� �� ������ �װ� ����� �ǳ�


    public enum AttackShape
    {
        Circle,
        SemiCircle,
        Triangle,
        Fan,
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 finalScale = CalculateFinalScale();

        DrawScaledCircleGizmo(finalScale);

        Gizmos.color = Color.white;
        Gizmos.DrawMesh(sharedMesh, transform.position, transform.rotation, transform.localScale);
    }

    void DrawScaledCircleGizmo(Vector3 finalScale)
    {
        float scaledRadius = radius * GetLargestScale(finalScale);
        Gizmos.DrawWireSphere(transform.position, scaledRadius);
    }

    Vector3 CalculateFinalScale()
    {
        Vector3 parentScale = GetParentGlobalScale();
        return new Vector3(transform.localScale.x * parentScale.x,
                           transform.localScale.y * parentScale.y,
                           transform.localScale.z * parentScale.z);
    }

    Vector3 GetParentGlobalScale()
    {
        if (transform.parent == null)
            return Vector3.one;

        return transform.parent.lossyScale;
    }

    float GetLargestScale(Vector3 scale)
    {
        return Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
    }


    private bool IsPlayerInCircleArea(Vector3 playerPosition)
    {
        player = detectedPlayer.GetComponent<Player>();

        Vector3 finalScale = CalculateFinalScale();
        float scaledRadius = radius * GetLargestScale(finalScale);

        float distanceToPlayer = (detectedPlayer.position - transform.position).magnitude;
        return distanceToPlayer <= scaledRadius;
    }

    void Start()
    {
        detectedPlayer = GameObject.FindGameObjectWithTag("Player").transform;


        material = GetComponent<Renderer>().material;
        startTime = Time.time;

        //material.SetFloat("UVSpeed", 1.0f);
    }

    void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        sharedMesh = CreateFanMesh();
        meshFilter.mesh = sharedMesh;
        meshCollider.sharedMesh = sharedMesh;

        CalculateCenterPoint();
    }

    private void OnEnable()
    {
        ResetColor();
        ResetStartTime();
    }
    private void ResetStartTime()
    {
        startTime = Time.time;
    }

    public Mesh GetSharedMesh()
    {
        return sharedMesh;
    }

    public void ResetColor()
    {
        if (material != null)
        {
            //Debug.Log("123");
            material.color = Color.yellow;
        }
    }

    void Update()
    {
        if (enemyAi == null || detectedPlayer == null)
            return;

        float t = Mathf.Clamp01((Time.time - startTime) / enemyAi.CurrentPreparationTime);
        material.color = Color.Lerp(Color.yellow, Color.red, t);

        //float uvSpeed = Mathf.Lerp(1.0f, 2.0f, t);
        //material.SetFloat("UVSpeed", uvSpeed);

        //Debug.Log(detectedPlayer.position);

        if (IsPlayerInCircleArea(detectedPlayer.position))
        {
            isplayerInside = true;

            Debug.Log("�÷��̾ �� �ȿ� �ֽ��ϴ�.");
        }
        else
        {
            isplayerInside = false;
            Debug.Log("�÷��̾ �� �ۿ� �ֽ��ϴ�.");
        }
    }

    public Mesh CreateFanMesh()
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

        // ����
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = angleStep * i; // 90�� �߰� // �ٽ� ���� Įŧ����Ʈ �ǵ�°ɷ�
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

        // �ﰢ��
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

    public void ToggleMeshRendering(bool isEnabled)
    {
        GetComponent<MeshRenderer>().enabled = isEnabled;
    }

    private void CalculateCenterPoint()
    {
        if (sharedMesh == null)
            return;

        // �޽��� ��� ���ؽ��� ��ȸ
        Vector3 sum = Vector3.zero;
        foreach (Vector3 vertex in sharedMesh.vertices)
        {
            sum += vertex;
        }
        centerPoint = sum / sharedMesh.vertexCount;
    }
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

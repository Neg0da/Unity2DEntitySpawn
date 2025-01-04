using System.Collections;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    // General settings
    public GameObject objectPrefab;      // Object prefab
    public float respawnTime = 0.2f;     // Time between spawns
    private Camera _mainCamera;          // Main camera
    public float innerRadius = 12f;       // Inner spawn radius
    public float outerRadius = 20f;      // Outer spawn radius
    public bool keepMomentum = false;    // Whether to keep momentum upon spawn
    public int poolSize = 10;            // Pool size
    private GameObject[] _objectPool;    // Object pool
    private int _currentIndex = 0;       // Current index in the pool
    public float delaySeconds = 5f;      // Delay before first spawn
    public bool enableLogs = true;       // Flag for logging
    public bool EnableGizmos = true;     // Flag for Gizmos visualization
    public bool EnableRotation = true;

    // Random size settings
    public bool randomSize = false;      // Enable random size for objects
    public Vector2 sizeRange = new Vector2(1f, 4f); // Range for random sizes

    void Start()
    {
        _mainCamera = Camera.main;

        if (objectPrefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        if (_mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        if (outerRadius <= innerRadius)
        {
            Debug.LogError("Outer radius must be greater than inner radius!");
            return;
        }

        if (poolSize <= 0)
        {
            Debug.LogError("Pool size must be greater than zero!");
            return;
        }

        // Initialize object pool
        _objectPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            _objectPool[i] = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity, transform);
                    // Set random size if enabled
            if (randomSize)
            {
                float randomScale = Random.Range(sizeRange.x, sizeRange.y);
                _objectPool[i].transform.localScale = Vector3.one * randomScale;
            }
            _objectPool[i].SetActive(false);
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
#if UNITY_EDITOR
        if (enableLogs)
        {
            Debug.Log("Waiting for the first spawn...");
        }
#endif
        yield return new WaitForSeconds(delaySeconds);

        while (true)
        {
            SpawnPrefabObject();
            yield return new WaitForSeconds(respawnTime);
        }
    }

    private void SpawnPrefabObject()
    {
        if (_currentIndex < 0 || _currentIndex >= _objectPool.Length)
        {
            Debug.LogError("Invalid currentIndex! Resetting to 0.");
            _currentIndex = 0;
        }

        GameObject currentObject = _objectPool[_currentIndex];
        _currentIndex = (_currentIndex + 1) % poolSize;

        // Check distance between object and camera
        float distanceToCamera = Vector2.Distance(currentObject.transform.position, _mainCamera.transform.position);

        if (distanceToCamera <= outerRadius && currentObject.activeSelf)
        {
#if UNITY_EDITOR
            if (enableLogs)
            {
                Debug.Log("Object is within outerRadius and active. Skipping reposition.");
            }
#endif
            return; // If the object is inside the outerRadius and active, don't reposition it
        }

        // Get new spawn position
        Vector2 spawnPosition = GetSpawnPosition();
        currentObject.transform.position = spawnPosition;


        // Add random speed on first spawn
        Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Random linear velocity
            rb.linearVelocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            rb.angularVelocity = Random.Range(-10f, 10f); // Random angular velocity
        }

        // If momentum is not kept, reset velocity
        if (!keepMomentum)
        {
            rb.linearVelocity = Vector2.zero; // Reset linear velocity
            rb.angularVelocity = 0f;    // Reset angular velocity
        }

        currentObject.SetActive(true);

#if UNITY_EDITOR
        if (enableLogs)
        {
            Debug.Log("Object spawned: " + currentObject.name);
        }
#endif
    }

    private Vector2 GetSpawnPosition()
    {
        float distance = Random.Range(innerRadius, outerRadius);
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Angle in radians
        Vector2 spawnDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)_mainCamera.transform.position + spawnDirection;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (EnableGizmos)
        {
            Camera currentCamera = Camera.main;

            if (currentCamera != null)
            {
                Vector2 cameraPosition = currentCamera.transform.position;

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(cameraPosition, innerRadius);

                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(cameraPosition, outerRadius);
            }
            else
            {
                Debug.LogWarning("Main Camera not found. Gizmos will not be centered on the camera.");
            }
        }
#endif
    }
}

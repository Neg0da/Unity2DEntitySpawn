using System.Collections;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    // ===============================
    // GENERAL SETTINGS
    // ===============================
    public GameObject objectPrefab;        // Object prefab
    public float respawnTime = 0.2f;       // Time between spawns
    public float delaySeconds = 5f;        // Delay before first spawn
    public bool enableLogs = true;         // Flag for logging
    public bool enableGizmos = true;       // Flag for Gizmos visualization
    public bool randomRotation = false;    // Flag for rotation

    // ===============================
    // SPAWN RADIUS SETTINGS
    // ===============================
    public float innerRadius = 12f;        // Inner spawn radius
    public float outerRadius = 20f;        // Outer spawn radius
    private Camera _mainCamera;            // Main camera reference

    // ===============================
    // OBJECT POOL SETTINGS
    // ===============================
    public int poolSize = 10;              // Pool size
    private GameObject[] _objectPool;      // Object pool
    private int _currentIndex = 0;         // Current index in the pool

    // ===============================
    // RANDOMIZATION SETTINGS
    // ===============================
    public bool randomSize = false;        // Enable random size for objects
    public Vector2 sizeRange = new Vector2(1f, 4f); // Range for random sizes
    public bool keepMomentum = false;      // Whether to keep momentum upon spawn

    // ===============================
    // SPAWN DURATION SETTINGS
    // ===============================
    public float spawnDuration = 10f;      // Maximum time for spawning objects
    public enum ObjectState
    {
        Spawning,
        Deactivating
    }
    [SerializeField] private ObjectState _afterDurationState = ObjectState.Spawning; // State after duration ends

    // ===============================
    // UNITY METHODS
    // ===============================
    private void Start()
    {
        _mainCamera = Camera.main;

        ValidateInputs();
        InitializeObjectPool();
        StartCoroutine(SpawnRoutine());
    }

    // ===============================
    // SPAWN ROUTINE
    // ===============================
    private IEnumerator SpawnRoutine()
    {
#if UNITY_EDITOR
        if (enableLogs)
        {
            Debug.Log($"Waiting for the first spawn for {delaySeconds} seconds.");
        }
#endif
        yield return new WaitForSeconds(delaySeconds);

        float elapsedTime = 0f;

        while (spawnDuration == 0 || elapsedTime < spawnDuration)
        {
            SpawnPrefabObject();
            yield return new WaitForSeconds(respawnTime);
            elapsedTime += respawnTime;
        }

        if (_afterDurationState == ObjectState.Deactivating)
        {
            StartCoroutine(DeactivateObjects());
        }
    }

    // ===============================
    // DEACTIVATING OBJECTS ONE BY ONE
    // ===============================
    private IEnumerator DeactivateObjects()
    {
        bool allDeactivated = true;

        foreach (var obj in _objectPool)
        {
            if (obj.activeSelf)
            {
                float distanceToCamera = Vector2.Distance(obj.transform.position, _mainCamera.transform.position);

                if (distanceToCamera > outerRadius)
                {
                    obj.SetActive(false);
#if UNITY_EDITOR
                    if (enableLogs)
                    {
                        Debug.Log($"Deactivating object: {obj.name} because it is outside outerRadius");
                    }
#endif
                }
                else
                {
#if UNITY_EDITOR
                    if (enableLogs)
                    {
                        Debug.Log($"Object {obj.name} is inside outerRadius and won't be deactivated.");
                    }
#endif
                    allDeactivated = false;
                }
            }

            yield return new WaitForSeconds(respawnTime);
        }

        if (allDeactivated)
        {
#if UNITY_EDITOR
            if (enableLogs)
            {
                Debug.Log("All objects have been deactivated.");
            }
#endif
        }
        else
        {
            StartCoroutine(DeactivateObjects());
        }
    }

    // ===============================
    // OBJECT SPAWN LOGIC
    // ===============================
    private void SpawnPrefabObject()
    {
        if (_currentIndex < 0 || _currentIndex >= _objectPool.Length)
        {
            Debug.LogError("Invalid currentIndex! Resetting to 0.");
            _currentIndex = 0;
        }

        GameObject currentObject = _objectPool[_currentIndex];
        _currentIndex = (_currentIndex + 1) % poolSize;

        if (IsObjectActiveAndWithinRadius(currentObject))
        {
            return;
        }

        Vector2 spawnPosition = GetSpawnPosition();
        currentObject.transform.position = spawnPosition;

        InitializeObjectPhysics(currentObject);

        currentObject.SetActive(true);

#if UNITY_EDITOR
        if (enableLogs)
        {
            Debug.Log($"Object spawned: {currentObject.name}");
        }
#endif
    }

    // ===============================
    // UTILITY METHODS
    // ===============================
    private void ValidateInputs()
    {
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
    }

    private void InitializeObjectPool()
    {
        if (_objectPool == null)
        {
            _objectPool = new GameObject[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                _objectPool[i] = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity, transform);
                SetRandomProperties(_objectPool[i]);
                _objectPool[i].SetActive(false);
            }
        }
    }

    private void SetRandomProperties(GameObject obj)
    {
        if (randomSize)
        {
            float randomScale = Random.Range(sizeRange.x, sizeRange.y);
            obj.transform.localScale = Vector3.one * randomScale;
        }

        if (randomRotation)
        {
            obj.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        }
    }

    private bool IsObjectActiveAndWithinRadius(GameObject obj)
    {
        float distanceToCamera = Vector2.Distance(obj.transform.position, _mainCamera.transform.position);

        if (distanceToCamera <= outerRadius && obj.activeSelf)
        {
#if UNITY_EDITOR
            if (enableLogs)
            {
                Debug.Log("Object is within outerRadius and active. Skipping reposition.");
            }
#endif
            return true;
        }

        return false;
    }

    private void InitializeObjectPhysics(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            rb.angularVelocity = Random.Range(-10f, 10f);
        }

        if (!keepMomentum)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private Vector2 GetSpawnPosition()
    {
        float distance = Random.Range(innerRadius, outerRadius);
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 spawnDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)_mainCamera.transform.position + spawnDirection;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (enableGizmos)
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
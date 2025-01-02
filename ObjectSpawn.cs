using System.Collections;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject objectPrefab;      // Префаб об'єкта
    public float respawnTime = 1.0f;     // Час між спавнами
    private Camera _mainCamera;          // Основна камера
    public float innerRadius = 5f;       // Внутрішній радіус спавну
    public float outerRadius = 10f;      // Зовнішній радіус спавну
    public bool keepMomentum = false;    // Чи зберігати імпульс при спавні
    public int poolSize = 10;            // Розмір пулу
    private GameObject[] _objectPool;    // Пул об'єктів
    private int _currentIndex = 0;       // Поточний індекс в пулі
    public float delaySeconds = 5f;      // Затримка перед першим спавном
    public bool enableLogs = true;       // Прапорець для логування
    public bool EnableGizmos = true;     // Прапорець для візуалізації Gizmos

    void Start()
    {
        if (objectPrefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        _mainCamera = Camera.main;
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

        // Ініціалізація пулу об'єктів
        _objectPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            _objectPool[i] = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity, transform);
            _objectPool[i].SetActive(false);
        }

        StartCoroutine(SpawnRoutine());
    }

    private Vector2 GetSpawnPosition()
    {
        float distance = Random.Range(innerRadius, outerRadius);
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Кут у радіанах
        Vector2 spawnDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)_mainCamera.transform.position + spawnDirection;
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

        // Перевіряємо відстань між об'єктом і камерою
        float distanceToCamera = Vector2.Distance(currentObject.transform.position, _mainCamera.transform.position);

        if (distanceToCamera <= outerRadius && currentObject.activeSelf)
        {
            if (enableLogs)
            {
                Debug.Log("Object is within outerRadius and active. Skipping reposition.");
            }
            return; // Якщо об'єкт всередині outerRadius і активний, не переміщаємо
        }

        // Отримуємо нову позицію для спавну
        Vector2 spawnPosition = GetSpawnPosition();
        currentObject.transform.position = spawnPosition;

        // Додаємо випадкову швидкість при першому спавні
        Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Випадкова лінійна швидкість
            rb.linearVelocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)); 
            rb.angularVelocity = Random.Range(-10f, 10f); // Випадкова кутова швидкість
        }

        // Якщо не зберігаємо імпульс, обнуляємо швидкість
        if (!keepMomentum)
        {
            rb.linearVelocity = Vector2.zero; // Обнуляємо лінійну швидкість
            rb.angularVelocity = 0f;    // Обнуляємо кутову швидкість
        }

        currentObject.SetActive(true);

        if (enableLogs)
        {
            Debug.Log("Object spawned: " + currentObject.name);
        }
    }

    IEnumerator SpawnRoutine()
    {
        Debug.Log("Waiting for the first spawn...");
        yield return new WaitForSeconds(delaySeconds);

        while (true)
        {
            SpawnPrefabObject();
            yield return new WaitForSeconds(respawnTime);
        }
    }

    private void OnDrawGizmos()
    {
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
    }
}

using UnityEngine;

public class HandController2D : MonoBehaviour
{
      public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    public GameObject choppedPiecePrefab; // Add this line
    private BallManager ballManager; // Add this line

    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    private Vector3 direction;
    public Vector3 Direction => direction;

    private bool slicing;
    public bool Slicing => slicing;

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
        ballManager = FindObjectOfType<BallManager>(); // Add this line
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            StartSlice();
        } else if (Input.GetMouseButtonUp(0)) {
            StopSlice();
        } else if (slicing) {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            // Notify BallManager to remove the fruit and rearrange
            ballManager.RemoveBall(other.gameObject);

            // Instantiate chopped pieces (optional, if you still need this)
            Vector3 position = other.transform.position;
            Quaternion rotation = other.transform.rotation;

            Instantiate(choppedPiecePrefab, position + Vector3.right * 0.1f, rotation);
            Instantiate(choppedPiecePrefab, position + Vector3.left * 0.1f, rotation);
        }
    }
}

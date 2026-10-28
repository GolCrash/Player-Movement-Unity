
using UnityEngine;

public class HandcarMovement : MonoBehaviour
{
    [SerializeField] private RailPath railPath;
    [SerializeField, Min(0f)] private float speed = 3f;
    [SerializeField, Min(0f)] private float rotationSpeed = 180f;

    private float distanceTravelled;
    private bool isMoving;

    private void Start()
    {
        if (railPath == null)
        {
            Debug.LogError("HandcarMovement: RailPath не назначен.", this);

            enabled = false;
            return;
        }

        railPath.RebuildPath();

        if (!railPath.IsValid)
        {
            Debug.LogError("HandcarMovement: маршрут не содержит " + "достаточного количества корректных точек.", this);

            enabled = false;
            return;
        }

        distanceTravelled = 0f;
        isMoving = true;

        UpdatePositionAndRotation();
    }

    private void Update()
    {
        if (!isMoving)
            return;

        distanceTravelled += speed * Time.deltaTime;

        if (distanceTravelled >= railPath.TotalLength)
        {
            distanceTravelled = railPath.TotalLength;
            isMoving = false;
        }

        UpdatePositionAndRotation();
    }

    private void UpdatePositionAndRotation()
    {
        transform.position =  railPath.GetPositionAtDistance(distanceTravelled);

        Vector3 direction = railPath.GetDirectionAtDistance(distanceTravelled);

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
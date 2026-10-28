
using System.Collections.Generic;
using UnityEngine;

public class RailPath : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField, Min(2)] private int samplesPerSegment = 20;

    private readonly List<Vector3> sampledPoints = new();
    private float[] cumulativeDistances = System.Array.Empty<float>();

    public bool IsValid => sampledPoints.Count >= 2;

    public float TotalLength => cumulativeDistances.Length > 0 ? cumulativeDistances[cumulativeDistances.Length - 1] : 0f;

    private void Awake()
    {
        RebuildPath();
    }

    private void OnValidate()
    {
        samplesPerSegment = Mathf.Max(2, samplesPerSegment);
    }

    public void RebuildPath()
    {
        sampledPoints.Clear();
        cumulativeDistances = System.Array.Empty<float>();

        if (points == null || points.Length < 2)
            return;

        for (int i = 0; i < points.Length - 1; i++)
        {
            if (points[i] == null || points[i + 1] == null)
                return;
        }

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector3 p0 = points[Mathf.Max(i - 1, 0)].position;
            Vector3 p1 = points[i].position;
            Vector3 p2 = points[i + 1].position;
            Vector3 p3 = points[Mathf.Min(i + 2, points.Length - 1)].position;

            for (int j = 0; j < samplesPerSegment; j++)
            {
                float t = j / (float)samplesPerSegment;

                sampledPoints.Add(CatmullRom(p0, p1, p2, p3, t));
            }
        }

        sampledPoints.Add(points[points.Length - 1].position);

        cumulativeDistances = new float[sampledPoints.Count];

        for (int i = 1; i < sampledPoints.Count; i++)
        {
            cumulativeDistances[i] = cumulativeDistances[i - 1] + Vector3.Distance(sampledPoints[i - 1], sampledPoints[i]);
        }
    }

    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (2f * p1 + (-p0 + p2) * t + (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 + (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }

    public Vector3 GetPositionAtDistance(float distance)
    {
        if (!IsValid)
            return transform.position;

        distance = Mathf.Clamp(distance, 0f, TotalLength);

        int index = FindSegment(distance);

        float segmentLength =cumulativeDistances[index + 1] - cumulativeDistances[index];

        float t = segmentLength > 0.0001f ? (distance - cumulativeDistances[index]) / segmentLength : 0f;

        return Vector3.Lerp(sampledPoints[index], sampledPoints[index + 1], t);
    }

    public Vector3 GetDirectionAtDistance(float distance)
    {
        float offset = 0.1f;

        Vector3 before = GetPositionAtDistance(Mathf.Max(0f, distance - offset));

        Vector3 after = GetPositionAtDistance(Mathf.Min(TotalLength, distance + offset));

        return (after - before).normalized;
    }

    private int FindSegment(float distance)
    {
        int low = 0;
        int high = cumulativeDistances.Length - 2;

        while (low < high)
        {
            int mid = (low + high) / 2;

            if (cumulativeDistances[mid + 1] < distance)
                low = mid + 1;
            else
                high = mid;
        }

        return low;
    }

    private void OnDrawGizmos()
    {
        RebuildPath();

        if (!IsValid)
            return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < sampledPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(sampledPoints[i], sampledPoints[i + 1]);
        }

        Gizmos.color = Color.cyan;

        foreach (Vector3 point in sampledPoints)
        {
            
        }

        if (points == null)
            return;

        Gizmos.color = Color.white;

        foreach (Transform point in points)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.15f);
        }
    }
}

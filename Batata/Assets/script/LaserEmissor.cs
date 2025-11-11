using UnityEngine;

public class LaserEmissor : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int maxReflections = 5;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask laserMask;

    void Update()
    {
        CastLaser();
    }

    void CastLaser()
    {
        Vector2 direction = transform.right;
        Vector2 position = transform.position;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, position);

        int reflections = 0;

        while (reflections < maxReflections)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, maxDistance, laserMask);

            if (hit.collider != null)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                // Verifica se é espelho
                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector2.Reflect(direction, hit.normal);
                    position = hit.point + direction * 0.01f;
                    reflections++;
                }
                // Verifica se atingiu o receptor
                else if (hit.collider.CompareTag("Receiver"))
                {
                    hit.collider.GetComponent<LaserReceiver>()?.Ativar();
                    break;
                }
                else
                {
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, position + direction * maxDistance);
                break;
            }
        }
    }
}

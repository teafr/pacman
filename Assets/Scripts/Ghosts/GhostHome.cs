using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private IEnumerator ExitTransition()
    {
        SwitchState(Vector2.up, false);

        Vector3 position = Ghost.transform.position;

        float duration = 0.5f, elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, inside.position, elapsed / duration);
            newPosition.z = position.z;
            Ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
            newPosition.z = position.z;
            Ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        SwitchState(GetRandomDirection(), true);
        Ghost.Scatter.Enable();
    }

    private void SwitchState(Vector2 direction, bool isEnabled)
    {
        Ghost.Movement.SetDirection(direction, true);
        Ghost.Movement.Rigidbody.bodyType = isEnabled ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        Ghost.Movement.enabled = isEnabled;
    }

    private static Vector2 GetRandomDirection()
    {
        return new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0f);
    }
}

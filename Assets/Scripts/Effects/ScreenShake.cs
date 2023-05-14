using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool start = false;
    [SerializeField] public AnimationCurve curve;
    public float duration = 1f;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapedTime = 0f;

        while (elapedTime < duration)
        {
            elapedTime += Time.deltaTime;
            float stregth = curve.Evaluate(elapedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * stregth;
            yield return null;
        }
        transform.position = startPosition;
    }
    public void ShakeScreen()
    {
        start = true;
    }
}
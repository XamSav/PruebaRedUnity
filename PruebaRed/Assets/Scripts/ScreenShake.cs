using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float amount = 0.2f;
    public float shakeSpeed = 0.2f;
    public float shakeTime = 0.5f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        Vector3 origen = transform.localPosition;
        float endTime = Time.time + shakeTime;
        while(Time.time < endTime)
        {
            yield return StartCoroutine(MoveRandom(origen));
        }
        transform.localPosition = origen;
    }
    IEnumerator MoveRandom(Vector3 origen)
    {
        Vector3 random = Random.insideUnitSphere * amount;
        Vector3 target = origen + random;
        while (Vector3.Distance(transform.localPosition, target) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, shakeSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }
}

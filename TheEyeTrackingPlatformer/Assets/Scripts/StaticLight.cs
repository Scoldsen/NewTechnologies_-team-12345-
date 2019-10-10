using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class StaticLight : MonoBehaviour
{
    Light2D lamp;
    public GameObject enemy;

    public float maxDistance = 12;
    public float minDistance = 4;

    public float distance;

    private IEnumerator blinkCoroutine = null;

    void Start()
    {
        lamp = GetComponent<Light2D>();
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            lamp.intensity = 0;
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            lamp.intensity = 1;
            float waitTime = (distance - minDistance) / 5.0f;
            yield return new WaitForSeconds(Random.Range(0.1f, 1) * waitTime);
        }
    }

    void Update()
    {
        if (enemy == null) return;
        //distance = Mathf.Abs(enemy.transform.position.x - transform.position.x);
        distance = Vector2.Distance(transform.position, enemy.transform.position);
        int mode = distance < minDistance ? 2 : distance < maxDistance ? 1 : 0;

        switch (mode)
        {
            case 0:
                lamp.intensity = 1;
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }
                break;
            case 1:
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = Blinking();
                    StartCoroutine(blinkCoroutine);
                }
                break;
            case 2:
                lamp.intensity = 0;
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }
                break;
        }
    }
}

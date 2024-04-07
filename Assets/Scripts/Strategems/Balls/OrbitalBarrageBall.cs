using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OrbitalBarrageBall : StrategemBall
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float duration = 60f;
    [SerializeField] float radius = 50f;
    [SerializeField] float spawnHeight = 150f;
    [SerializeField] float rpm = 60f;

    float roundDelay;
    float timer = 0f;
    bool activated = false;

    protected override void Start()
    {
        base.Start();
        roundDelay = rpm / 60f;
    }

    private void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                activated = false;
            }
        }
    }

    protected override void Activate()
    {
        activated = true;
        StartCoroutine(BarrageCoroutine());
    }

    IEnumerator BarrageCoroutine()
    {
        while (activated)
        {
            yield return new WaitForSeconds(roundDelay);
            Vector2 randPos = Random.insideUnitCircle * radius;
            Vector3 spawnPoint = new Vector3(transform.position.x + randPos.x, spawnHeight, transform.position.z + randPos.y);
            Instantiate(projectilePrefab, randPos, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

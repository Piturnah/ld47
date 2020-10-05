using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject asteroid;
    public WorldBoundary boundary;

    private void Start() {
        StartCoroutine(SpawnAsteroid());
    }

    IEnumerator SpawnAsteroid() {
        yield return new WaitForEndOfFrame();
        while (true) {
            Instantiate(asteroid, new Vector2(Random.Range(-boundary.dimensions.x, boundary.dimensions.x), Random.Range(-boundary.dimensions.y, boundary.dimensions.y)) * 0.5f, Quaternion.identity);
            yield return new WaitForSeconds(7);
        }
    }
}

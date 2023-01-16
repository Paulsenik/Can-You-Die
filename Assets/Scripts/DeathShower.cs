using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathShower : MonoBehaviour {

    public static DeathShower singleton;

    public float spawnDelay;
    private float spawnDelayCount;
    public float MaxObjPer_10_Area = 2; // Spawnt eine bestimmte anzahl an Objekten pro 10 freie Bereicheinheiten

    public Transform spawnArea;
    public Transform usableArea;

    public GameObject obj;

    private bool isDestroyed = false;
    private float area;

    private void Awake() {
        if (singleton == null)
            singleton = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);

        area = usableArea.localScale.x * usableArea.localScale.y;
    }

    private void OnDestroy() {
        isDestroyed = true;
        DeathShowerObject.destroyAll();
    }

    void Update() {
        if (!isDestroyed) {
            spawnDelayCount += Time.deltaTime;

            //Debug.Log("MaxObjs: " + (area / 10 * MaxObjPer_10_Area) + " CurrentObjs: " + DeathShowerObject.objs.Count);
            if (area / 10 * MaxObjPer_10_Area > DeathShowerObject.objs.Count) {
                trySpawningObj();
            }

            checkForUsableArea();
        }
    }

    private void checkForUsableArea() {
        foreach (DeathShowerObject o in DeathShowerObject.objs) {
            if (!GameManager.collides(o.transform, usableArea.transform)) {
                Destroy(o.gameObject);
            }
        }
    }

    private void trySpawningObj() {
        if (spawnDelay <= spawnDelayCount) {
            spawnDelayCount = 0;
            spawnObj();
        }
    }

    private void spawnObj() {
        float randX = Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2);
        float randY = Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2);
        obj.transform.position = new Vector2(randX, randY);
        Instantiate(obj);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(spawnArea.position, spawnArea.localScale);
        Gizmos.DrawWireCube(usableArea.position, usableArea.localScale);
    }
}

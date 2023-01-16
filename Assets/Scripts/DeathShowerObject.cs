using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathShowerObject : MonoBehaviour {

    public static List<DeathShowerObject> objs = new List<DeathShowerObject>();

    public float lifeTime = 30f;
    private float lifeTimeCount = 0f;

    public float fallSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;
    private float finalRotSpeed;

    public float rotationDegree = 90;

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        objs.Add(this);
        finalRotSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }
    private void OnDestroy() {
        objs.Remove(this);
    }

    void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, transform.localEulerAngles.z + rotationDegree), Time.deltaTime * finalRotSpeed);
        transform.localPosition = new Vector2(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);

        lifeTimeCount += Time.deltaTime;
        if (lifeTime <= lifeTimeCount) {
            Destroy(gameObject);
        }
    }

    public static void destroyAll() {
        Debug.Log("dest");
        foreach (DeathShowerObject o in objs) {
            Destroy(o.gameObject);
        }
    }
}

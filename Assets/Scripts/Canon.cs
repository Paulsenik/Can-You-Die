using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Canon : MonoBehaviour {

    public Transform spawnPoint;
    public GameObject bullet;

    public float shootDelay;
    private float shootDelayCounter = 0;

    public bool overrideBulletSpeed = false;
    public float newBulletSpeed;

    [Header("ShootEvent")]
    public UnityEvent shootEvent;

    private void Start() {
        shoot();
    }

    void Update() {
        if (GameManager.singleton.isGameActive) {
            shootDelayCounter += Time.deltaTime;

            if (shootDelay < shootDelayCounter) {
                shootDelayCounter = 0f;
                shoot();
            }
        }
    }
    public void shoot() {
        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = spawnPoint.rotation;

        GameObject go = Instantiate(bullet);
        if (overrideBulletSpeed)
            go.GetComponent<Bullet>().speed = newBulletSpeed;

        shootEvent.Invoke();
    }
}

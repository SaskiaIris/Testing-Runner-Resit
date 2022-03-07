using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerWalk : MonoBehaviour {
    private Vector3 velocity;

    [SerializeField]
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start() {
        velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        if(this.gameObject.transform.position.x >= 1.5) {
            if(this.gameObject.transform.rotation.y != 0) {
                this.gameObject.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, (transform.eulerAngles + 180f * Vector3.up), 1f + Time.deltaTime * Time.timeScale);
            }
            StartCoroutine(WaitAfterTurning());
            velocity.x = -(speed * Time.deltaTime * Time.timeScale);
        } else if(this.gameObject.transform.position.x <= 0) {
            if(this.gameObject.transform.rotation.y >= 0 || this.gameObject.transform.rotation.y <= 0) {
                this.gameObject.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, (transform.eulerAngles + 180f * Vector3.up), 1f + Time.deltaTime * Time.timeScale);
            }
            StartCoroutine(WaitAfterTurning());
            velocity.x = speed * Time.deltaTime * Time.timeScale;
        }
        this.gameObject.transform.position += velocity;
    }

    private IEnumerator WaitAfterTurning() {
        yield return new WaitForSecondsRealtime(0.3f);
    }

}
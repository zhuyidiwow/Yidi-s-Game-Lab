using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour {

	public string color;
    public List<GameObject> startPoints = new List<GameObject>();

    void Start() {
        int i = 1;
        while (this.transform.Find("Point (" + i + ")") != null) {
            startPoints.Add(this.transform.Find("Point (" + i + ")").gameObject);
            i++;
        }
    }

    void Update() {
        bool areAllSameColor = true;

        foreach (GameObject point in startPoints) {
            Vector3 direction = point.transform.position - Camera.main.transform.position;
            direction = direction.normalized;

            Ray ray = new Ray(Camera.main.transform.position, direction);

            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Background")) ) {
                if (color != hit.collider.gameObject.GetComponent<Background>().color) {
                    areAllSameColor = false; // if any of these point gives a false, do not remove the thing
                    Debug.Log(hit.collider.gameObject.GetComponent<Background>().color);
                } else {
                    Debug.Log(hit.collider.gameObject.GetComponent<Background>().color);
                }
            } else {
                areAllSameColor = false;
                Debug.Log("No Background");
            }
        }

        if (areAllSameColor) {
            Destroy(this.gameObject);
        }
    }
}

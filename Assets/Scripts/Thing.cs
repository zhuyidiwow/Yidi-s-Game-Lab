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
        bool areAllBackgroundsInSameColor = true;

        foreach (GameObject point in startPoints) {
            Vector3 direction = point.transform.position - Camera.main.transform.position;
            direction = direction.normalized;

            Ray ray = new Ray(Camera.main.transform.position, direction);

            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Background")) ) {
                if (color != hit.collider.gameObject.GetComponent<Background>().color) {
                    areAllBackgroundsInSameColor = false; // if any of these points gives a false, do not remove the thing
                }
            } else {
                areAllBackgroundsInSameColor = false;
            }
        }

        if (areAllBackgroundsInSameColor) {
            Destroy(this.gameObject);
        }
    }
}

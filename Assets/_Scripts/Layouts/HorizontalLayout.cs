using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLayout : MonoBehaviour {

    [Header("Settings")]
    public float spaceBetweenChilds = 0f;
    public bool useAllAvailableSpace = false;

    private Camera cam;

    [ContextMenu("Update Position")]
    public void UpdateChildPositions () {
        if (transform.childCount == 0) {
            return;
        }

        if (cam == null) {
            cam = Camera.main;
        }

        float left = cam.ScreenToWorldPoint(Vector3.zero).x;
        float right = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, 0f)).x;

        if (useAllAvailableSpace) {
            UpdateChildPositionsScreenSize(left, right);
        } else {
            UpdateChildPositionsSpaceBetween(left, right);
        }
    }

    private void UpdateChildPositionsScreenSize (float left, float right) {
        Vector3 extent = transform.GetComponentInChildren<Renderer>().bounds.extents;
        left -= extent.x;
        right += extent.x;
        float diff = right - left;
        float delta = diff / (transform.childCount + 1);

        for (int i = 1; i <= transform.childCount; i++) {
            Transform curr = transform.GetChild(i - 1);
            Vector3 pos = new Vector3(left + (delta * i), transform.position.y, 0f);
            curr.position = pos;
        }

        transform.position = transform.position;
    }

    private void UpdateChildPositionsSpaceBetween (float left, float right) {
        Vector3 size = transform.GetComponentInChildren<Renderer>().bounds.size;
        Vector3 extent = size / 2f;

        float delta = size.x + spaceBetweenChilds;
        left = transform.position.x - ((delta * transform.childCount) / 2f) + (delta / 2f);

        for (int i = 0; i < transform.childCount; i++) {
            Transform curr = transform.GetChild(i);
            Vector3 pos = new Vector3(left + (delta * i), transform.position.y, 0f);
            curr.position = pos;
        }
    }
}

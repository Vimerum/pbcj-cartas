using UnityEngine;
using System.Collections;

public class VerticalLayout : MonoBehaviour {

    [Header("Settings")]
    public float size = 0f;
    public float spaceBetweenChilds = 0f;
    public float topMargin = 0f;
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

        float bottom = cam.ScreenToWorldPoint(Vector3.zero).y;
        float top = cam.ScreenToWorldPoint(new Vector3(0f, cam.pixelHeight, 0f)).y - topMargin;

        if (useAllAvailableSpace) {
            UpdateChildPositionsScreenSize(bottom, top);
        } else {
            UpdateChildPositionsSpaceBetween(bottom, top);
        }
    }

    private void UpdateChildPositionsScreenSize (float bottom, float top) {
        float extent = size / 2f;
        bottom -= extent;
        top += extent;
        float diff = top - bottom;
        float delta = diff / (transform.childCount + 1);

        for (int i = 1; i <= transform.childCount; i++) {
            Transform curr = transform.GetChild(i - 1);
            Vector3 pos = new Vector3(transform.position.x, bottom + (delta * i), 0f);
            curr.position = pos;
        }

        transform.position = transform.position;
    }

    private void UpdateChildPositionsSpaceBetween (float bottom, float top) {
        float extent = size / 2f;

        float delta = size + spaceBetweenChilds;
        bottom = bottom + ((top - bottom) / 2f) - ((delta * transform.childCount) / 2f) + (delta / 2f);

        for (int i = 0; i < transform.childCount; i++) {
            Transform curr = transform.GetChild(i);
            Vector3 pos = new Vector3(transform.position.x, bottom + (delta * i), 0f);
            curr.position = pos;
        }
    }
}

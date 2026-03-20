using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private float backgroundSize;

    void Start() {
        if (backgroundSize <= 0 && transform.childCount > 0)                    // Try auto-collect width
            backgroundSize = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;

        if (transform.childCount >= 2) {                                        // Auto-place second bg next to first one
            transform.GetChild(0).localPosition = Vector3.zero;
            transform.GetChild(1).localPosition = new Vector3(backgroundSize, 0, 0);
        }
    }

    void Update() {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);       // Continuous scrolling

        for (int i = 0; i < transform.childCount; i++) {                        // Looping
            Transform child = transform.GetChild(i);
            
            if (child.position.x <= -backgroundSize) {
                float offset = transform.childCount * backgroundSize;
                child.localPosition += new Vector3(offset, 0, 0);
            }
        }
    }
}

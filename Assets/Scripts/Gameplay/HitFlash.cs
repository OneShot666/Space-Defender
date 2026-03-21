using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour {
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    
    private Color _originalColor;
    private Coroutine _flashCoroutine;

    void Awake() {
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = spriteRenderer.color;
    }

    public void Flash() {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);            // Stop ongoing coroutine
        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine() {
        spriteRenderer.color = Color.red;                                     // Flash image -> show damage
        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = _originalColor;                                  // Redraw image as default after short time
        _flashCoroutine = null;
    }
}

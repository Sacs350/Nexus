using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorRestorer : MonoBehaviour
{
    public Volume globalVolume;      // the Global Volume from Step 12
    public float duration = 3f;      // seconds the fade takes

    private ColorAdjustments colorAdjustments;

    void Start()
    {
        globalVolume.profile.TryGet(out colorAdjustments);
        colorAdjustments.saturation.overrideState = true;   // make sure the setting is active
        colorAdjustments.saturation.value = -100f;          // start fully grey
    }

    // Call this to bring the color back
    public void RestoreColor()
    {
        StartCoroutine(FadeToColor());
    }


    System.Collections.IEnumerator FadeToColor()
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;              // goes 0 to 1
            colorAdjustments.saturation.value = Mathf.Lerp(-100f, 0f, progress);
            yield return null;                              // wait one frame
        }
        colorAdjustments.saturation.value = 0f;             // make sure it ends exactly at 0
    }

    // TEMPORARY test: press C to restore color
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
        {
            RestoreColor();
        }
    }
}

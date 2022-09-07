using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilledPointer : MonoBehaviour
{
    private Image image => transform.parent.GetComponent<Image>();
    private RectTransform rectTransform => GetComponent<RectTransform>();

    private void Update()
    {
        rectTransform.localEulerAngles = new Vector3(0, 0, (image.fillAmount * 360) * (image.fillClockwise ? -1 : 1));
    }
}

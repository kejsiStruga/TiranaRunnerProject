using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ScreensConfigs
{
    public class CanvasFade : MonoBehaviour
    {
        public Image img;
        public AnimationCurve curve;
        public void FadeMe()
        {
            StartCoroutine("FadeCanvas");
        }

        IEnumerator FadeCanvas()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            Color imgColor = img.color;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                float a = curve.Evaluate(t);
                imgColor.a -= Time.deltaTime;
                //skip to the next frame
                yield return 0;
            }
            //ne menyre qe kur te shfaqet modali mos te klikohet tek butonat e niveleve
            canvasGroup.interactable = false;
            yield return null;
        }
    }
}

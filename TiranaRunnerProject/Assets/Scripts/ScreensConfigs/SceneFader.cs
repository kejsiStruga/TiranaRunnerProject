using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public Image img;
	public AnimationCurve curve;
    public Button[] levelButtons;
    //public Canvas canvas; // komentuar Anxhela
	void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo (string scene)
    {
    //    img.color = new Vector4(1f, 1f, 1f, 1f);
    //    img.CrossFadeAlpha(0.0f, 1.0f, true);
		StartCoroutine(FadeOut(scene));
	}
    
    /*
        Perdorim Courutine sepse do ulet opacity dhe nuk duam qe kjo te ndodhe 
        brenda nje frame !! Sepse nuk do te ishte e dukshme 
     */
	IEnumerator FadeIn ()
	{
		float t = 1f;
		while (t > 0f)
		{
        //Kur behet fade in => -- ; duke u bazuar @ Time
            //t must be desiplayed on the Alpha channel (000) -> black
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
            //ne te kundert nuk del img !!
			//img.color = new Color (0f, 0f, 0f, a);
			yield return 0;
		}
	}

	IEnumerator FadeOut(string scene)
	{
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            if (levelButtons.Length > 0)
            {
                levelButtons[0].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, a);
                levelButtons[0].GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, a);
                levelButtons[1].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, a);
                levelButtons[1].GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, a);
                levelButtons[2].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, a);
                levelButtons[2].GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, a); 
            }

            yield return 0;
        }
        SceneManager.LoadScene(scene);
	}
}
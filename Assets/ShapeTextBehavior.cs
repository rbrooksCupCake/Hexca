using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShapeTextBehavior : MonoBehaviour
{
    public TextMeshProUGUI shapeText;
    public TextMeshProUGUI borderText;
    public string textToShow;
    public Material textMaterial;
    public float Speed;
    public float timeToType;
    public float timeToDisplay;

    IEnumerator TypeText()
    {
        shapeText.text = "";
        borderText.text = "";
        foreach (var character in textToShow)
        {
            shapeText.text += character;
            borderText.text += character;
            yield return new WaitForSeconds(timeToType);
        }
        yield return new WaitForSeconds(timeToDisplay);
        shapeText.text = "";
        borderText.text = "";
        gameObject.SetActive(false);
    }

    public void Activate(string text)
    {
        textToShow = text;
        StartCoroutine(TypeText());
    }

    private void OnDisable()
    {
        shapeText.text = "";
        borderText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        shapeText.color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1));
    }
}

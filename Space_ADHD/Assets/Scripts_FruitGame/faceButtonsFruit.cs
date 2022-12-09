using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class faceButtonsFruit : MonoBehaviour
{
    private Button button;

    [SerializeField] Button otherButton1;
    [SerializeField] Button otherButton2;

    private RawImage image1;
    private RawImage image2;
    private RawImage image3;    

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        image3 = this.GetComponent<RawImage>();
        image1 = otherButton1.GetComponent<RawImage>();
        image2 = otherButton2.GetComponent<RawImage>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        image3.color = new Color(image3.color.r, image3.color.g, image3.color.b, 1f);
        image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 0.5f);
        image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, 0.5f);
        switch (this.name)
        {
            case "Q1Green":
                submitButtonFruit.susValue = 3;
                break;
            case "Q2Green":
                submitButtonFruit.evalValue = 3;
                break;
            case "Q1Yellow":
                submitButtonFruit.susValue = 2;
                break;
            case "Q2Yellow":
                submitButtonFruit.evalValue = 2;
                break;
            case "Q1Red":
                submitButtonFruit.susValue = 1;
                break;
            case "Q2Red":
                submitButtonFruit.evalValue = 1;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

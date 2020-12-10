using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image mask;
    
    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaskFill(float fillAmount){
        mask.fillAmount = fillAmount;
    }
}

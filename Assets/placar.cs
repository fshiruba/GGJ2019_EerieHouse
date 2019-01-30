using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class placar : MonoBehaviour
{
    public int pontos;
    public GameObject TM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TM.GetComponent<TextMeshProUGUI>().text = pontos.ToString();
 
    }
}

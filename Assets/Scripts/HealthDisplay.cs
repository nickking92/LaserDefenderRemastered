using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    Text healthtext;
    Player p;
   
	// Use this for initialization
	void Start () {
        healthtext = GetComponent<Text>();
        p = FindObjectOfType<Player>();
        
	}
	
	// Update is called once per frame
	void Update () {
        healthtext.text = p.GetHealth().ToString();
        if (p.GetHealth()== 600)
        {
            healthtext.color = Color.yellow;
        }else 
        if (p.GetHealth()== 500)
        {
            healthtext.color = Color.red;
        }
    }
}

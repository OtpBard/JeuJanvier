using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Text buttonText = null;

   void Start()
   {
       GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuButtonSound");

        if (objs.Length >= 1)
        {
            GetComponent<Button>().onClick.AddListener(objs[0].GetComponent<AudioSource>().Play);
        }
   }
   public void OnMouseHover()
   {
       buttonText.color = new Color(0.103f, 0.138f, 0.165f);
   }

   public void OnMouseExit()
   {
       buttonText.color = new Color(255f, 238f, 218f);
   }
}

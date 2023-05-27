using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiitch : MonoBehaviour
{
   
    public GameObject[] background;
    public int pages;

    [SerializeField]
    TextSpeechController speechController;

    [SerializeField]
    public int maxPage = 11;

    void Start()
    {
        pages = 0;
        background[0].gameObject.SetActive(true);
    }
        

    private void _UpdatePage(int page){
      background[pages].gameObject.SetActive(false);
      pages = page;
      background[pages].gameObject.SetActive(true);
      if(speechController.autoSpeak)
         speechController.OnStartSpeak();
    }


    public void Next()
     {
         if(pages + 1 > maxPage) return;
         _UpdatePage(pages + 1);
     }
    
     public void Previous()
     {
         if(pages - 1 < 0) return;
         _UpdatePage(pages - 1);
     }

   
}

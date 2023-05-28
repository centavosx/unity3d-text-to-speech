using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;
using System.Text.RegularExpressions;
using TMPro;

public class TextSpeechController : MonoBehaviour
{
    
    [SerializeField]
    public string tag; //Gameobject tag, used for getting text mesh pro text that will be used for text to speech
    [SerializeField]
    public string lang = "en-US";  
    [Range(0.5f, 2)]
    public float pitch = 1f;
    [Range(0.5f, 2)]
    public float rate = 1f;

    public bool autoSpeak  = false;
    private bool _isSpeaking = false;


    void Start()
    {
        _isSpeaking = false;
        _Setting(lang, pitch, rate);

        //Callback whenever the speech has started or ended
        TextToSpeech.Instance.onStartCallBack = _OnStart;
        TextToSpeech.Instance.onDoneCallback = _OnEnd;
    }

    //Set isSpeaking to true if started
    private void _OnStart(){
        _isSpeaking = true;
    }

    //Set isSpeaking to false if ended
    private void _OnEnd(){
        _isSpeaking = false;
    }

    //Method for speak
    public void OnStartSpeak()
    {

        if(autoSpeak){
            OnStopSpeak();
        }

        if(_isSpeaking) return;
        
        string message = "";

        //Will search for all gameobject with the selected tag
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
            //Gameobject should be active
            if(item.activeSelf){
                TMP_Text component = item.GetComponent<TMP_Text>();
                if(!!item) message+=".\n "+component.text;
            }

        string newMessage = Regex.Replace(message, @"<[^>]+>|&nbsp;|&zwnj;|&raquo;|&laquo;", "").Trim();

        TextToSpeech.Instance.StartSpeak(newMessage);
    }

    //Switching auto speak
    public void SwitchAuto(){
        autoSpeak = !autoSpeak;
        
        if(autoSpeak) {
            OnStartSpeak();
        }else{
            OnStopSpeak();
        }

    }

    //Method to stop speak
    public void OnStopSpeak()
    {
        _isSpeaking = false;
        TextToSpeech.Instance.StopSpeak();
    }

    //Settings
    private void _Setting(string code, float pitch, float rate)
    {
        TextToSpeech.Instance.Setting(code, pitch, rate);
    }


}

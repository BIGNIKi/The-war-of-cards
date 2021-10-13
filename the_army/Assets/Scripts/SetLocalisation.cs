using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyNameSpace
{
    public class SetLocalisation : MonoBehaviour
    {
        [Header("Localisation ID")]
        public string localisationName;

        public Text myText;

        public void SetText(string text)
        {
            myText.text = text;
        }
    }
}
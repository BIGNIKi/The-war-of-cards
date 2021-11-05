using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyNameSpace
{
    public class HQredLight : MonoBehaviour
    {
        public CanvasGroup canGr;

        private int isUp = 1;

        [Range(0, 1)] public float speed = 0.5f;

        void Start()
        {

        }

        void Update()
        {
            canGr.alpha += isUp * speed * Time.deltaTime;
            if(canGr.alpha >= 1 || canGr.alpha <= 0) isUp = -isUp;
        }
    }
}
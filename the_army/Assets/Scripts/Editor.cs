using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyNameSpace
{
    public class Editor : MonoBehaviour
    {
        private Mode mode = Mode.Open;

        public GameObject blackPanel; //панель для затемнения переходов

        public GameObject editor;

        public void OpenCloseEdit() //opens or closes edit menu
        {
            blackPanel.SetActive(true); //because of it, a player cannot call this method several times quickly
            StartCoroutine(OpenCloseEditConcurrency());
        }

        private IEnumerator OpenCloseEditConcurrency()
        {
            Mode previousMode = mode;
            if(mode == Mode.Open)
            {
                StartCoroutine(SettingDecks());
                mode = Mode.Close;
            }
            else if(mode == Mode.Close)
            {
                mode = Mode.Open;
            }

            yield return AlphaChangeDark(false, previousMode);
            StartCoroutine(AlphaChangeDark(true, previousMode));
            yield break;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isClosing"></param> false - dark panel will open; true - dark panel will close
        /// <returns></returns>
        private IEnumerator AlphaChangeDark(bool isClosing, Mode localMode)
        {
            CanvasGroup canGr = blackPanel.transform.GetComponent<CanvasGroup>();
            while(true)
            {
                if(isClosing)
                {
                    canGr.alpha -= Time.deltaTime;
                    if(canGr.alpha <= 0) break;
                }
                else
                {
                    canGr.alpha += Time.deltaTime;
                    if(canGr.alpha >= 1)
                        break;
                }
                
                yield return null;
            }

            switch(localMode)
            {
                case Mode.Open:
                    editor.SetActive(true);
                    break;
                case Mode.Close:
                    editor.SetActive(false);
                    break;
            }

            if(isClosing) blackPanel.SetActive(false);

            yield break;
        } 

        private IEnumerator SettingDecks()
        {
            yield break;
        }

        public enum Mode //режимы кнопки (при нажатии нужно открыть меню или закрыть)
        {
            Open,
            Close
        }
    }
}
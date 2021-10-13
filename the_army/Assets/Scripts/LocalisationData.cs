using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyNameSpace
{
    public class LocalisationData : MonoBehaviour
    {
        public List<LocalisationStruct> data;

        public Language language;

        public SetLocalisation[] allObjectForSetLocalisation;

        void Start()
        {
            LoadLocalisationData();
            FillLocalisationData();
        }

        /// <summary>
        /// загрузка файла локализации в List data
        /// </summary>
        private void LoadLocalisationData()
        {
            TextAsset allData = Resources.Load<TextAsset>("Localisation");

            string[] data = allData.text.Split(new char[] { '\n' });

            for(int i = 1; i<data.Length-1; i++)
            {
                string[] row = data[i].Split(new char[] { ',' });

                LocalisationStruct newL = new LocalisationStruct
                {
                    localisationName = row[0],
                    english = row[1],
                    russian = row[2]
                };

                this.data.Add(newL);
            }
        }

        private void FillLocalisationData()
        {
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "atGame TestUI") //если основная игровая сцена
            {
                foreach(SetLocalisation sl in allObjectForSetLocalisation)
                {
                    string s = "";
                    foreach(LocalisationStruct ls in data)
                    {
                        if(ls.localisationName == sl.localisationName)
                        {
                            switch(language)
                            {
                                case Language.English:
                                    s = ls.english;
                                    break;
                                case Language.Russian:
                                    s = ls.russian;
                                    break;
                            }
                            break;
                        }
                    }
                    if(s.Equals(""))
                    {
                        Debug.LogError("Localisation isn't found.");
                    }
                    sl.SetText(s);
                }
            }
        }

        [Serializable]
        public class LocalisationStruct
        {
            public string localisationName;
            public string english;
            public string russian;
        }

        public enum Language
        {
            English,
            Russian
        }
    }
}

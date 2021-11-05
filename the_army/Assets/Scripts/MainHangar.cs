using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Text;

#pragma warning disable 0649

namespace MyNameSpace
{

    //������� - ������� � ���� ������ ��������: login + ��������, ��������������� ������������������
    //������ ���� (������� ������) ����� �� login'�, � ��������������� ������ ��������� � ���������, ��� ����� �� �������� ����� JSON'�

    public class MainHangar : MonoBehaviour
    {
        [Header("�������� ���������� � �������")]
        public MainInfo accountInfo;

        public GameObject prefOfShtab; //������ �����

        public DataSet dataset;

        public LocalisationData localization;

        public static Vector3 sizeOfSmallHQ = new Vector3(0.6f, 0.6f, 0.6f);

        //[SerializeField] - ���� ��� �������� ����� ��������� ����� ������, �� ��� ���� ����� ������������ � ���������
        //[Range(0, 100)] - �������� ������� �������� ��� ��������� ��������
        //[Space] - ������� ������ � ����������

        void Start()
        {
            LoadProfileDebugJson();
            //Util.ToBinaryAndCreateFile<MainInfo>(accountInfo);
            LoadMainStuffs();
            //LoadProfileDebugBinary();
            InitAllHeadQuarters();
        }

        /// <summary>
        /// �������� ������� ��� Debug mod'� �� JSON �����
        /// </summary>
        private void LoadProfileDebugJson()
        {
            if(Debug.isDebugBuild && File.Exists("./NewJson.json")) //���� ���� ���������� � �� � Debug ����
            {
                StreamReader sr = new StreamReader("./NewJson.json"); //�����, ������� ����� ������
                if(sr != null) //���� ��� ����
                {
                    string userInfoText = sr.ReadToEnd();
                    sr.Close(); //������� ����
                    accountInfo = JsonUtility.FromJson<MainInfo>(userInfoText);
                    return;
                }
            }
            Debug.LogError("Couldn't find a file.");
        }

        private void LoadProfileDebugBinary()
        {
            if(Debug.isDebugBuild && File.Exists("./Profile.dat")) //���� ���� ���������� � �� � Debug ����
            {
                FileStream fs = new FileStream("./Profile.dat", FileMode.OpenOrCreate);

                if(fs != null)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    accountInfo = (MainInfo)formatter.Deserialize(fs);
                    fs.Close();
                    return;
                }
            }
            Debug.LogError("Couldn't find a file.");
        }

        /// <summary>
        /// ��������� ��� �������, ����� �����, ����� � ������
        /// </summary>
        private void LoadMainStuffs()
        {
            GameObject.Find("Main Camera/Canvas/background/Profile/name").GetComponent<Text>().text = accountInfo.login;
            GameObject.Find("Main Camera/Canvas/UpPlashkaImg/moneyHere/Text").GetComponent<Text>().text = accountInfo.money.ToString();
            GameObject.Find("Main Camera/Canvas/UpPlashkaImg/freExpHere/Text").GetComponent<Text>().text = accountInfo.expi.ToString();
            GameObject.Find("Main Camera/Canvas/UpPlashkaImg/goldHere/Text").GetComponent<Text>().text = accountInfo.gold.ToString();
        }

        /// <summary>
        /// �������� ���� ������ ������ (����� �� ���� �����)
        /// </summary>
        private void InitAllHeadQuarters()
        {
            //���������� ���������� ���������� ��� ������
            GameObject sliderShtab = GameObject.Find("Main Camera/Canvas/background/sliderShtab (1)/sliderShtab");
            int countChild = sliderShtab.transform.childCount;
            for(int i = 0; i < countChild; i++)
            {
                Destroy(sliderShtab.transform.GetChild(i).gameObject);
            }

            for(int i = 0; i < accountInfo.headQuars.Count; i++)
            {
                DataSet.HeadquarterInfo headInfo = dataset.GetHeadquarterInfoByName(accountInfo.headQuars[i].name);
                bool isBig = false;
                if(i == 0) isBig = true;
                AddHeadQuarter(headInfo, isBig, accountInfo.headQuars[i]);
            }
            //� �� ����� ����� ��������� ������ ����, �� ��� ��� ���� ��� �� �������� ������ �������� ������������� ����� (�� �� ���������� � ��������� �������� �����)
            //������� ���������� ������ ������� �� ���� isBig
            //headSlider.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        private void AddHeadQuarter(DataSet.HeadquarterInfo headInf, bool isBig, Headquarter userHeadInf)
        {
            GameObject sliderShtab = GameObject.Find("Main Camera/Canvas/background/sliderShtab (1)/sliderShtab");
            GameObject neww = Instantiate(prefOfShtab);
            if(!isBig)
            {
                neww.GetComponent<RectTransform>().localScale = sizeOfSmallHQ; //���������� ��������� ���� ���������� �������
            }
            else
            {
                neww.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); //���������� ��������� ���� ���������� �������
            }

            neww.transform.SetParent(sliderShtab.transform, false);
            neww.name = headInf.name;
            neww.transform.GetComponent<Image>().sprite = dataset.GetHeadquarterSpriteByName(headInf.name); //main picture of headquarter
            Transform structure = neww.transform.Find("bodyPapkaImg/structure");

            switch(localization.language)
            {
                case LocalisationData.Language.Russian:
                    structure.Find("NameBaseText").GetComponent<Text>().text = headInf.info0rus;
                    structure.Find("discriptionBaseText").GetComponent<Text>().text = headInf.info1rus;
                    break;
                case LocalisationData.Language.English:
                    structure.Find("NameBaseText").GetComponent<Text>().text = headInf.info0eng;
                    structure.Find("discriptionBaseText").GetComponent<Text>().text = headInf.info1eng;
                    break;
            }
            structure.Find("sortToCentreFIST/powerText").GetComponent<Text>().text = headInf.power.ToString() + " + power from cards";
            structure.Find("sortToCentreEXP/expText").GetComponent<Text>().text = SplitStr(userHeadInf.exp.ToString(), 3);
            String tmp = headInf.nation; //����� �����
            if(tmp.Equals("USSR"))
            {
                Instantiate(dataset.prefOfFlags[0], neww.transform.Find("bodyPapkaImg/structure/flagImg")); //����� ������ ������ ����
            }
            else if(tmp.Equals("Germany"))
            {
                Instantiate(dataset.prefOfFlags[1], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
            }
            else if(tmp.Equals("USA"))
            {
                Instantiate(dataset.prefOfFlags[2], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
            }

            for(int i = 0; i<userHeadInf.decks.Count; i++)
            {
                if(userHeadInf.decks[i].isSelected)
                {
                    structure.Find("kolodaText").GetComponent<Text>().text = userHeadInf.decks[i].name;
                    break;
                }
                if(i == userHeadInf.decks.Count-1)
                {
                    Debug.LogError("There are no selected deck");
                }
            }
        }

        /// <summary>
        /// ��������� ������� � ������ ����� ������ maxSymbols ������� � �����
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxSymbols"></param>
        /// <returns></returns>
        private string SplitStr(string str, int maxSymbols)
        {
            var sb = new StringBuilder();
            var counter = str.Length;
            foreach(var element in str)
            {
                if(counter % maxSymbols == 0)
                {
                    sb.Append(" ");
                }

                sb.Append(element);
                counter--;
            }
            return sb.ToString();
        }

        private Headquarter GetUserHeadquarter(string name)
        {
            for(int i = 0; i < accountInfo.headQuars.Count; i++)
            {
                if(accountInfo.headQuars[i].name.Equals(name))
                {
                    return accountInfo.headQuars[i];
                }
                if(i == accountInfo.headQuars.Count - 1)
                {
                    Debug.LogError("Headquarter isn't found");
                }
            }
            return new Headquarter();
        }

        void Update()
        {

        }

        [Serializable]
        public struct Deck
        {
            public string name;
            public bool isSelected; //������� ��� ������ ��� ���
            public List<CardAndCount> cards;
        }

        [Serializable]
        public struct MainInfo
        {
            public string login;
            public int money;
            public int expi;
            public int gold;
            public List<Headquarter> headQuars;
            public List<Card> resCards;
        }

        [Serializable]
        public struct Headquarter 
        {
            public string name;
            public int power;
            public int exp; //���� �����
            public List<Deck> decks; //������ ��� ����� �����
        }

        [Serializable]
        public struct CardAndCount //��� ����� � ������� ����
        {
            public string name;
            public int count; //����������
        }

        [Serializable]
        public struct Card //��� � ���������� ������ ����� (� �������)
        {
            public string name;
            public int count; //��� �������� ��� � ���� ������������ ����� (���� 0 � ������ - �����������), ���� ��� Card'�, �� ��� ����� �� �����������
        }
    }
}
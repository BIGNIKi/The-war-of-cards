using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

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

        //[SerializeField] - ���� ��� �������� ����� ��������� ����� ������, �� ��� ���� ����� ������������ � ���������
        //[Range(0, 100)] - �������� ������� �������� ��� ��������� ��������
        //[Space] - ������� ������ � ����������

        void Start()
        {
            //LoadProfileDebugJson();
            LoadProfileDebugBinary();
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
                GameObject neww = Instantiate(prefOfShtab);
                if(i != 0)
                {
                    neww.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f); //������� ���� ������ ������, ��� ������ ���������� �����
                }
                neww.transform.SetParent(sliderShtab.transform, false);
                neww.name = accountInfo.headQuars[i].name;
                //TODO
            }
        }

        void Update()
        {

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
        public struct Headquarter //���������� � ������ ������
        {
            public string name;
            public int power;
            public int exp; //���� �����
            public List<CardAndCount> cards; //��� �����, ��������� ��� ����� �����
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
            public int count;
        }
    }
}
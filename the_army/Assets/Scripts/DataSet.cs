using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyNameSpace
{

    public class DataSet : MonoBehaviour
    {
        [Header("�������� ���������� � ���������� ������")]
        public HeadquarterInfo[] headquarterInfo;

        [Header("�������� ������")]
        public ImageWithName[] headquartersImg;

        public GameObject[] prefOfFlags; //������� ������������� ������ 0 - ����, 1 - ��������, 2 - ���

        public HeadquarterInfo GetHeadquarterInfoByName(string name)
        {
            DataSet.HeadquarterInfo headInfo = new DataSet.HeadquarterInfo();
            for(int i = 0; i < headquarterInfo.Length; i++)
            {
                if(name.Equals(headquarterInfo[i].name))
                {
                    headInfo = headquarterInfo[i];
                    break;
                }
                if(i == headquarterInfo.Length - 1)
                {
                    Debug.LogError("�� ������� info");
                }
            }
            return headInfo;
        }

        public Sprite GetHeadquarterSpriteByName(string name)
        {
            for(int i = 0; i < headquartersImg.Length; i++)
            {
                if(headquartersImg[i].name.Equals(name))
                {
                    return headquartersImg[i].img;
                }
                if(i == headquartersImg.Length-1)
                {
                    Debug.LogError("�� ������� img");
                }
            }
            return null;
        }

        [Serializable]
        public struct HeadquarterInfo //���������� � �����
        {
            public string name;
            public int expPrice;
            public int monPrice;
            public string info0rus;
            public string info1rus;
            public string info0eng;
            public string info1eng;
            public int power;
            public int hp;
            public int damage;
            public int plusFuel;
            public string nation;
        }

        [Serializable]
        public struct ImageWithName // ��� � ��������
        { 
            public string name;
            public Sprite img;
        }
    }
}
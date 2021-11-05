using System.Collections;
using UnityEngine;

namespace MyNameSpace
{
    public class SlideHQ : MonoBehaviour
    {
        private bool canSlideShtab = true;

        [Header("id выбранного штаба")]
        [SerializeField] private int selectedHQID = 0;

        public GameObject sliderHQ;
        public GameObject leftSlideBtn;
        public GameObject rightSlideBtn;

        private float distanceBetweenHQ = -1f; //расстояние на которое надо смещать штабы для эффекта смены штаба

        [Header("Скорость слайдинга")]
        [Range(0, 2)] public float speedOfSlide;

        /// <summary>
        /// Слайдим штаб -1-влево, 1-вправо
        /// </summary>
        /// <param name="id"></param>
        public void LeftRightSlide(int id) 
        {
            if(distanceBetweenHQ < 0f) //если расстояние не проинициализировано
            {
                SetDistanceBetweenHQ();
            }
            if(canSlideShtab)
            {
                canSlideShtab = false;
                selectedHQID += id;
                if(id == -1) //left
                {
                    rightSlideBtn.SetActive(true);
                    if(selectedHQID == 0) leftSlideBtn.SetActive(false);
                }
                else //right
                {
                    leftSlideBtn.SetActive(true);
                    if(sliderHQ.transform.childCount == selectedHQID+1) rightSlideBtn.SetActive(false);
                }
                StartCoroutine(SmoothSlide(id));
            }
        }

        private void SetDistanceBetweenHQ()
        {
            Vector2 leftX = sliderHQ.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
            Vector2 rightX = sliderHQ.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition;
            float width = sliderHQ.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

            float dwidth = width - width * MainHangar.sizeOfSmallHQ.x; //delta width (на сколько изменится ширина после уменьшения размера)
            float center = rightX.x - dwidth; //новый центр правого штаба
            float leftEdge = center - width * MainHangar.sizeOfSmallHQ.x / 2; //левый край правого штаба
            float newCenter = leftEdge + width / 2; //центр правого штаба после увеличения
            distanceBetweenHQ = newCenter - leftX.x; //получили нужную дистанцию
        }

        private IEnumerator SmoothSlide(int multy)
        {
            Vector2 from = sliderHQ.GetComponent<RectTransform>().anchoredPosition;
            Vector2 to = new Vector2(from.x - distanceBetweenHQ * multy, from.y);

            Vector3 startScale = new Vector3(1, 1, 1);
            Vector3 finishScale = MainHangar.sizeOfSmallHQ;

            float progress = 0;
            while((sliderHQ.GetComponent<RectTransform>().anchoredPosition - to).sqrMagnitude > 0)
            {
                progress += speedOfSlide * Time.deltaTime;
                sliderHQ.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(from, to, progress);
                sliderHQ.transform.GetChild(selectedHQID - multy).GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, finishScale, progress);
                sliderHQ.transform.GetChild(selectedHQID).GetComponent<RectTransform>().localScale = Vector3.Lerp(finishScale, startScale, progress);

                yield return null; //продолжить после прохода итерации цикла Update
            }

            canSlideShtab = true;
            yield break;
        }
    }
}
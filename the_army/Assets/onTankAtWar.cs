using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class onTankAtWar : MonoBehaviour {
    public const float timeOnBullet = 0.15f; //время на полет пули и инерцию карт

    public int hp; //здоровье
    public int damage; //урон
    public int countOfPosibleMove; //число возможных передвижений
    public int idField; //id поля на котором стоит карта
    public int plusFuel; //даёт топлива

    public bool canAttack; //может атаковать?
    public bool isAmaSelected; //выбрана ли именно эта карта
    public bool isAmaHeadQuarter; //это штаб?

    public Text textHp;
    public Text textDamage;

    public Image frameImgGreen; //подсветка краев карты зеленый
    public Image frameImgYellow; //подсветка краев карты желтый

    [Header("Чья карта")]
    public bool isIFriendly; //true - наша карта, false - карта противника
    [Header("Двигаюсь ли я сейчас?")]
    public bool isAmaMove; //true - карта двигается, false - карта не двигается
    [Header("Могу ли дать ответный выстрел")]
    public bool canBackDamage; //true - можно дать ответный выстрел, false - нельзя

    public atPlayerWindow aPW; //ссылка на основной скрипт

    public string type; //название типа карты

    //public char idDirectionCell; //направление клетки относительно карты, которая стреляет

    public void clickOnTankToGo() { //нажал на танк, чтобы походить
        if(aPW == null) {
            aPW = GameObject.Find("Main Camera").GetComponent<atPlayerWindow>(); //получили основной скрипт
        }
        
        if(!isIFriendly) { //если это не наша карта
            if(aPW.isAmaTurn) { //если наш ход
                if(aPW.selectedCardMoveOrAttack) { //если уже выбрана некоторая карта для атаки или движения
                    if(!isAmaHeadQuarter && frameImgGreen.enabled == true) { //если красная подсветка включена(карта выделена и по ней можно стрельнуть)
                        aPW.selectedCardMoveOrAttack.GetComponent<onTankAtWar>().isAmaMove = true; //мы совершаем некоторое действие, на карту нажать невозможно, пока действие не завершится
                        attackCard(); //срабатывает атака по карте
                    } else if(isAmaHeadQuarter && frameImgGreen.enabled == true) { //если это вражеский штаб
                        aPW.selectedCardMoveOrAttack.GetComponent<onTankAtWar>().isAmaMove = true; //мы совершаем некоторое действие, на карту нажать невозможно, пока действие не завершится
                        attackCard();
                    }
                }
            }
            return;
        }
        if(!aPW.isAmaTurn) {
            return;
        }

        //надо, чтобы можно было атаковать или двигаться и
        //не была выбрана карта для атаки или движения и
        //не выбрана карта в руке и карта сейчас не двигается
        int countAround, freeCellAround;
        if(isAmaHeadQuarter) {
            countAround = 1;
            freeCellAround = 0;
        } else {
            countAround = aPW.countAroundYou(idField, false); //считает число противников рядом с нашей картой
            freeCellAround = aPW.freePlaceToMove(idField, countOfPosibleMove); //число свободных клеток, куда можно походить
        }
        //Debug.Log("countAround = " + countAround + " : freeCellAround = " + freeCellAround);
        if(((canAttack && countAround > 0) || (countOfPosibleMove > 0 && freeCellAround > 0)) && aPW.isMoveOrAttackCard == false && aPW.canITouchCardAtWar && !isAmaMove) {
            if(!isAmaHeadQuarter) {
                GameObject tempCell = this.transform.parent.gameObject;
                for(int i = 0; i < aPW.cellsGO.Length; i++) { //нормальным способом получаем id клетки, в которой стоит карта
                    if(tempCell.name == aPW.cellsGO[i].name) {
                        idField = i;
                        break;
                    }
                }

                if(aPW.countAroundYou(idField, false) == 0 && countOfPosibleMove == 0) { //если рядом с этой картой никого нет и ходить она уже не может
                    return;
                }
            }

            aPW.selectedCardMoveOrAttack = this.gameObject; //указали выбранную карту
            aPW.isMoveOrAttackCard = true;
            aPW.canITouchCardAtWar = false;

/*            for(int i = 0; i < aPW.cardFrames.Length; i++) {
                if(aPW.cardFrames[i].name == "Yellow") {
                    frameImgGreen.sprite = aPW.cardFrames[i].img;
                    break;
                }
            }*/
            frameImgGreen.enabled = false; //зеленая подсветка выкл
            frameImgYellow.enabled = true; //желтая вкл

            isAmaSelected = true;
            if(countOfPosibleMove > 0) {
                aPW.selectNeededCellsToMove(idField, countOfPosibleMove, false); //выделение клеток зеленым
            }
            if(canAttack) { //если может атаковать
                if(!isAmaHeadQuarter) { //если обычная карта
                    aPW.selectEnemyCard(idField, true); //подсвечиваем карты противника в которые можно бахнуть
                } else { //если штаб
                    aPW.sellectEnemyCardForHQ();
                }
            }
            
        } else if (isAmaSelected) { //если нажали на выбранную, чтобы отменить выбор
            if(!isAmaHeadQuarter) { //если обычная карта
                cancelSelection(this.gameObject, idField, idField);
            } else { //если штаб
                aPW.unSellectCardForHQ();
                aPW.isMoveOrAttackCard = false;
                aPW.canITouchCardAtWar = true;
/*                for(int i = 0; i < aPW.cardFrames.Length; i++) {
                    if(aPW.cardFrames[i].name == "Green") {
                        frameImgGreen.sprite = aPW.cardFrames[i].img;
                        break;
                    }
                }*/
                frameImgGreen.enabled = true; //зеленая подсветка вкл
                frameImgYellow.enabled = false; //желтая выкл

                isAmaSelected = false;
                aPW.selectedCardMoveOrAttack = null;
            }
                
        }
    }

    public IEnumerator waitForFrame() {


        yield return 0; //ждем кадр (полезно, чтобы дождаться удаления объекта)

        yield break;
    }

    public IEnumerator cancelSellectionParall(onTankAtWar oTAW, GameObject me, int idCellFrom, int idCellTo) {
        yield return null; //ждём кадр, чтобы объекты удалились

        aPW.selectEnemyCard(idCellFrom, false); //убираем подсветку карт противника в которые могли бахнуть
        int countAround = aPW.countAroundYou(idCellTo, false); //считает число противников рядом с нашей картой
        int freeCellAround = aPW.freePlaceToMove(idCellTo, oTAW.countOfPosibleMove); //число свободных клеток, куда можно походить
        //Debug.Log(freeCellAround + " idCellTo = " + idCellTo + " countPosibleMove = " + oTAW.countOfPosibleMove);
        if((countAround == 0 || !oTAW.canAttack) && (oTAW.countOfPosibleMove == 0 || freeCellAround == 0)) { //карта не может не стрелять, не ходить
                                                                                                             //Debug.Log("Ну я сработал");
            me.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
            oTAW.frameImgGreen.enabled = false;
        }


        yield break;
    }

    public void cancelSelection(GameObject me, int idCellFrom, int idCellTo) { //отменяет выделение карты me, idCell - номер клетки, вокруг которой выделение
        onTankAtWar oTAW = me.GetComponent<onTankAtWar>();
        aPW.isMoveOrAttackCard = false;
        aPW.canITouchCardAtWar = true;

        oTAW.frameImgGreen.enabled = true; //зеленая подсветка вкл
        oTAW.frameImgYellow.enabled = false; //желтая выкл

        oTAW.isAmaSelected = false;
        if(!aPW.selectedCardMoveOrAttack.GetComponent<onTankAtWar>().isAmaHeadQuarter) { //если атаковал не штаб
            aPW.selectNeededCellsToMove(idCellFrom, oTAW.countOfPosibleMove, true); //убирает выделение клеток

            StartCoroutine(cancelSellectionParall(oTAW, me, idCellFrom, idCellTo)); //так надо, чтобы прошёл кадр и удалилсь клетки с подсветкой

/*            aPW.selectEnemyCard(idCellFrom, false); //убираем подсветку карт противника в которые могли бахнуть
            int countAround = aPW.countAroundYou(idCellTo, false); //считает число противников рядом с нашей картой
            int freeCellAround = aPW.freePlaceToMove(idCellTo, oTAW.countOfPosibleMove); //число свободных клеток, куда можно походить
            Debug.Log(freeCellAround + " idCellTo = " + idCellTo + " countPosibleMove = " + oTAW.countOfPosibleMove);
            if((countAround == 0 || !oTAW.canAttack) && (oTAW.countOfPosibleMove == 0 || freeCellAround == 0)) { //карта не может не стрелять, не ходить
                //Debug.Log("Ну я сработал");
                me.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
                oTAW.frameImgGreen.enabled = false;
            }*/

            if(!oTAW.canAttack) { //если не может атаковать
                me.transform.Find("attackImg/disable").gameObject.SetActive(true);
            }
        } else { //если атаковал штаб
            aPW.unSellectCardForHQ(); //снимает выделение со всеx карт(красные подсветки вырубятся)
            me.transform.Find("shtabImg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            oTAW.frameImgGreen.enabled = false;
            me.transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(true); //показываем, что не может атаковать
        }
        aPW.selectedCardMoveOrAttack = null;
    }

    void attackCard() { //атакуем карту
        pointerExit(); //делает курсор обычным
        GameObject goThatAttack = aPW.selectedCardMoveOrAttack; //карта, которая стреляет
        goThatAttack.GetComponent<onTankAtWar>().canAttack = false; //больше не может стрелять
        cancelSelection(goThatAttack, goThatAttack.GetComponent<onTankAtWar>().idField, goThatAttack.GetComponent<onTankAtWar>().idField); //снимает выделение с карты, которая атакует + с клеток и карт, которые рядом

        int from = -1;
        if(!goThatAttack.GetComponent<onTankAtWar>().isAmaHeadQuarter) { //если стрелял не штаб
            for(int i = 0; i<aPW.cellsGO.Length; i++) {
                if(aPW.cellsGO[i].name == goThatAttack.transform.parent.name) {
                    from = i;

                    break;
                }
                if(i == aPW.cellsGO.Length-1) {
                    Debug.Log("Не бывает такого");
                }
            }
           // from = goThatAttack.transform.parent.GetSiblingIndex();
        } else { //если штаб
            from = 0;
        }
        int to = -1;
        if(!isAmaHeadQuarter) { //если стреляли не в штаб
            for(int i = 0; i < aPW.cellsGO.Length; i++) {
                if(aPW.cellsGO[i].name == this.transform.parent.name) {
                    to = i;

                    break;
                }
                if(i == aPW.cellsGO.Length - 1) {
                    Debug.Log("Не бывает такого");
                }
            }
            //to = this.transform.parent.GetSiblingIndex(); //номера клеток откуда и куда стреляем
        } else { //если бахнули в штаб
            to = 14;
        }

        char direction = directionAttack(from, to); //определяем направление стрельбы
        char directionBack = directionAttack(to, from); //обратное направление
        //Debug.Log(direction.ToString());
        StartCoroutine(moveCardWhenAttack(goThatAttack, direction)); //двигаем карту при стрельбе, которая стреляла

        StartCoroutine(attackAnim(goThatAttack, direction, directionBack, this.gameObject)); //полёт пули во врага + смещенией карты после попадания + эффект взрыва + вылет урона(анимация) + обработка ответного выстрела (в конце)

        aPW.sendInfoAboutMoveNoCoroutine(from, to, "NULL", false, false); //инфа о ходе улетает на сервер
    }

    public void attackCardFromServer(int idFrom, int idTo) { //атака карты, полученная с сервера      
        GameObject goThatAttack; //карта, которая стреляет

        //надо следить за подсветкой карты, возможностью ходить и стрелять
        if(idFrom != 14) { //карта
            goThatAttack = aPW.cellsGO[idFrom].transform.GetChild(0).gameObject;
            goThatAttack.transform.Find("attackImg/disable").gameObject.SetActive(true); //показываем, что не может стрелять
            goThatAttack.GetComponent<onTankAtWar>().canAttack = false; //не может стрелять
            if(goThatAttack.GetComponent<onTankAtWar>().countOfPosibleMove == 0) { //не может ходить
                goThatAttack.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
            }
        } else { //штаб
            goThatAttack = aPW.cellsGO[idFrom].gameObject;
            goThatAttack.transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(true); //показываем, что не может стрелять
            goThatAttack.transform.Find("shtabImg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //сделали штаб темнее (нет доступных действий)
        }

        if(goThatAttack.GetComponent<onTankAtWar>().aPW == null) {
            goThatAttack.GetComponent<onTankAtWar>().aPW = GameObject.Find("Main Camera").GetComponent<atPlayerWindow>(); //получили основной скрипт
        }

        int from = -1;
        if(!goThatAttack.GetComponent<onTankAtWar>().isAmaHeadQuarter) { //если стрелял не штаб
            for(int i = 0; i < aPW.cellsGO.Length; i++) {
                if(aPW.cellsGO[i].name == goThatAttack.transform.parent.name) {
                    from = i;

                    break;
                }
                if(i == aPW.cellsGO.Length - 1) {
                    Debug.Log("Не бывает такого");
                }
            }
            //from = goThatAttack.transform.parent.GetSiblingIndex();
        } else { //если штаб
            from = 14;
        }
        int to = -1;
        if(!isAmaHeadQuarter) { //если стреляли не в штаб
            for(int i = 0; i < aPW.cellsGO.Length; i++) {
                if(aPW.cellsGO[i].name == this.transform.parent.name) {
                    to = i;

                    break;
                }
                if(i == aPW.cellsGO.Length - 1) {
                    Debug.Log("Не бывает такого");
                }
            }
            //to = this.transform.parent.GetSiblingIndex(); //номера клеток откуда и куда стреляем
        } else { //если бахнули в штаб
            to = 0;
        }

        char direction = directionAttack(from, to); //определяем направление стрельбы
        char directionBack = directionAttack(to, from); //обратное направление

        StartCoroutine(moveCardWhenAttack(goThatAttack, direction)); //двигаем карту при стрельбе, которая стреляла

        StartCoroutine(attackAnim(goThatAttack, direction, directionBack, this.gameObject)); //полёт пули во врага + смещенией карты после попадания + эффект взрыва + вылет урона(анимация) + обработка ответного выстрела (в конце)
    }

    //передаём карту, которая стреляет в эту карту(на которой висит скрипт) + символ, обозначающий куда полетит пуля
    IEnumerator moveCardWhenAttack(GameObject goThatAttack, char whereTo) {
        if(goThatAttack.GetComponent<onTankAtWar>().isAmaHeadQuarter) {
            goThatAttack = goThatAttack.transform.Find("shtabImg").gameObject;
        }
        Transform parentOfGoThatAttack = goThatAttack.transform.parent;
        //goThatAttack.transform.SetParent(aPW.objectWarPlace.transform.Find("map"), true); //открепили карту от клетки

        Vector3 startPosition = goThatAttack.GetComponent<RectTransform>().anchoredPosition;
        Vector3 finishPosition = new Vector3(); //чтобы компилятор не ругался (в switch оно якобы может не изменится)

        switch(whereTo) {
            case 'w':
                finishPosition = new Vector3(startPosition.x, startPosition.y - 22.5f, startPosition.z);
                break;
            case 's':
                finishPosition = new Vector3(startPosition.x, startPosition.y + 22.5f, startPosition.z);
                break;
            case 'd':
                finishPosition = new Vector3(startPosition.x - 22.5f, startPosition.y, startPosition.z);
                break;
            case 'a':
                finishPosition = new Vector3(startPosition.x + 22.5f, startPosition.y, startPosition.z);
                break;
            case 'e':
                finishPosition = new Vector3(startPosition.x - 22.5f, startPosition.y - 22.5f, startPosition.z);
                break;
            case 'q':
                finishPosition = new Vector3(startPosition.x + 22.5f, startPosition.y - 22.5f, startPosition.z);
                break;
            case 'c':
                finishPosition = new Vector3(startPosition.x - 22.5f, startPosition.y + 22.5f, startPosition.z);
                break;
            case 'z':
                finishPosition = new Vector3(startPosition.x + 22.5f, startPosition.y + 22.5f, startPosition.z);
                break;
        }

        float curTime = 0, timeOfTravel = timeOnBullet, normalizedValue;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            //двигаем карту
            goThatAttack.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, finishPosition, normalizedValue);

            yield return null;
        }

        curTime = 0; timeOfTravel = timeOnBullet;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            //двигаем карту
            goThatAttack.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(finishPosition, startPosition, normalizedValue);

            yield return null;
        }

        //goThatAttack.transform.SetParent(parentOfGoThatAttack, true); //положили карту под клетку

        yield break;
    }

    //принимает номера клеток (из какой клетки стреляем, в какую)
    char directionAttack(int from, int to) { //возвращает направление атаки
        int tMF = to - from;
        if(tMF == 5 || tMF == 10) { //вверх
            return 'w';
        }
        if(tMF == -10 || tMF == -5) {
            return 's'; //вниз
        }
        if((0 <= to && to <= 4 && 0 <= from && from <= 4) || (5 <= to && to <= 9 && 5 <= from && from <= 9) || (10 <= to && to <= 14 && 10 <= from && from <= 14)) {
            if(tMF > 0) {
                return 'd'; //вправо
            }
            if(tMF < 0) {
                return 'a'; //влево
            }
        }
        if((from >= 0 && from < 4 && (to > from+5 && to <= 9 || to > from + 10 && to <= 14)) || (from >= 5 && from < 9 && to > from + 5 && to <= 14)) {
            return 'e'; //вверх + вправо
        }
        if((from > 0 && from <= 4 && to < from + 5 && to > 4) || (from > 5 && from <= 9 && to < from + 5 && to > 9)) {
            return 'q'; //вверх + влево
        }
        if((from >= 10 && from < 14 && to > from - 5 && to > 5) || (from >= 5 && from < 9 && to > from - 5 && to > 0)) {
            return 'c'; //вниз + вправо
        }
        if((from > 10 && from <= 14 && to < from - 5 && to >= 5) || (from > 5 && from <= 9 && to < from - 5 && to >= 0)) {
            return 'z'; //вниз + влево
        }
        return 'x'; //хуета, не можеть быть такого вывода
    }

    public IEnumerator attackAnim(GameObject goThatAttack, char whereTo, char whereToBack, GameObject whomAttack) { //этот метод нужно запускать на карте, которую атакуют и передавать сюда карту, которая атакует
/*        if(whereTo == whereToBack) {
            Debug.Log(this.transform.parent.GetSiblingIndex().ToString());
        }*/

        GameObject bullet = Instantiate(aPW.prefOfBullet);
        bullet.transform.SetParent(goThatAttack.transform); //засунули под карту, которая стреляет
        bullet.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //ставим в самый центр относительно центра карты
        bullet.transform.SetParent(aPW.objectWarPlace.transform, true); //открепляем от карты с сохранением текущей позиции

        GameObject pointLikeBullet = Instantiate(aPW.prefOfBullet); //этот объект будет использоваться в качестве точки, куда лететь и поварачиваться
        pointLikeBullet.GetComponent<Image>().enabled = false; //чтобы она не отображалась как пуля
        pointLikeBullet.transform.SetParent(whomAttack.transform); //засунули под карту, в которую стреляют
        pointLikeBullet.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //ставим в самый центр относительно центра карты
        pointLikeBullet.transform.SetParent(aPW.objectWarPlace.transform, true); //открепляем от карты с сохранением текущей позиции

       /* if(whereTo == whereToBack) {
            yield break;
        }*/

        Vector2 from = bullet.GetComponent<RectTransform>().anchoredPosition;
        Vector2 to = pointLikeBullet.GetComponent<RectTransform>().anchoredPosition;
        float tgd = (to.y - from.y) / (to.x - from.x); //тангенс угла
        float d = Mathf.Atan(tgd) * Mathf.Rad2Deg; //угол поворота

        bullet.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, d); //повернули пулю в нужную сторону

        float curTime = 0, timeOfTravel = timeOnBullet, normalizedValue;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            bullet.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(from, to, normalizedValue); //плавный полет пули

            yield return null;
        }

        Destroy(bullet);
        Destroy(pointLikeBullet);

        Transform whoIAm; //сама карта (для штаба она отличается)
        if(!isAmaHeadQuarter) {
            whoIAm = whomAttack.transform;
        } else {
            whoIAm = whomAttack.transform.Find("shtabImg");
        }

        StartCoroutine(callExplosion(whoIAm.gameObject)); //симуляция взрыва на том, кого атаковали (происходит сразу после того, как пуля долетела)
       

        Transform parentOfGoThatAttack = whoIAm.transform.parent;
        //whoIAm.transform.SetParent(aPW.objectWarPlace.transform.Find("map"), true); //открепили карту от клетки

        Vector3 startPosition = whoIAm.GetComponent<RectTransform>().anchoredPosition;
        Vector3 finishPosition = new Vector3(); //чтобы компилятор не ругался (в switch оно якобы может не изменится)
        Vector3 finishPosition1 = new Vector3(); //чтобы компилятор не ругался (в switch оно якобы может не изменится)


        float offset = 11.25f;
        switch(whereTo) {
            case 'w':
                finishPosition = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x, startPosition.y - offset, startPosition.z);
                break;
            case 's':
                finishPosition = new Vector3(startPosition.x, startPosition.y - offset, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z);
                break;
            case 'd':
                finishPosition = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x - offset, startPosition.y, startPosition.z);
                break;
            case 'a':
                finishPosition = new Vector3(startPosition.x - offset, startPosition.y, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
                break;
            case 'e':
                finishPosition = new Vector3(startPosition.x + offset, startPosition.y + offset, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x - offset, startPosition.y - offset, startPosition.z);
                break;
            case 'q':
                finishPosition = new Vector3(startPosition.x - offset, startPosition.y + offset, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x + offset, startPosition.y - offset, startPosition.z);
                break;
            case 'c':
                finishPosition = new Vector3(startPosition.x + offset, startPosition.y - offset, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x - offset, startPosition.y + offset, startPosition.z);
                break;
            case 'z':
                finishPosition = new Vector3(startPosition.x - offset, startPosition.y - offset, startPosition.z);
                finishPosition1 = new Vector3(startPosition.x + offset, startPosition.y + offset, startPosition.z);
                break;
        }

        curTime = 0; timeOfTravel = timeOnBullet;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            //двигаем карту
            whoIAm.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, finishPosition, normalizedValue);

            yield return null;
        }
        if(whereTo != whereToBack && aPW.isAmaTurn || whereTo == whereToBack && !aPW.isAmaTurn) { //(если не ответный урон и стреляли мы) или (ответный и стрелял соперник)
            StartCoroutine(animHealth(whoIAm.gameObject, goThatAttack.GetComponent<onTankAtWar>().damage, parentOfGoThatAttack, isAmaHeadQuarter, 'u')); //симуляци вылета урона + убирание карты при смерти
        } else if(whereTo == whereToBack && aPW.isAmaTurn || whereTo != whereToBack && !aPW.isAmaTurn) { //(ответный урон и наш ход) или (прямой выстрел соперника)
            StartCoroutine(animHealth(whoIAm.gameObject, goThatAttack.GetComponent<onTankAtWar>().damage, parentOfGoThatAttack, isAmaHeadQuarter, 'd')); //симуляци вылета урона + убирание карты при смерти
        }
        //StartCoroutine(animHealth(whoIAm.gameObject, goThatAttack.GetComponent<onTankAtWar>().damage, parentOfGoThatAttack, isAmaHeadQuarter)); //симуляци вылета урона + убирание карты при смерти

        //здесь whoIAm не нужен!!!!! Так как у штаба в whoIAm лежит штука, на которой не висит onTankAtWar
        //this.hp -= goThatAttack.GetComponent<onTankAtWar>().damage; //отнимаем хп-шки
/*
        if(whereTo == whereToBack) {
            Debug.Log(whomAttack.name + " " + goThatAttack.name);
        }*/

        whomAttack.GetComponent<onTankAtWar>().hp -= goThatAttack.GetComponent<onTankAtWar>().damage; //отнимаем хп-шки
        if(whomAttack.GetComponent<onTankAtWar>().hp < 0) {
            whomAttack.GetComponent<onTankAtWar>().hp = 0;
        }
        //this.textHp.text = hp.ToString(); //обновляем текст с хп
        whomAttack.GetComponent<onTankAtWar>().textHp.text = whomAttack.GetComponent<onTankAtWar>().hp.ToString(); //обновляем текст с хп

        curTime = 0; timeOfTravel = timeOnBullet;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            //двигаем карту
            whoIAm.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(finishPosition, finishPosition1, normalizedValue);

            yield return null;
        }

        curTime = 0; timeOfTravel = timeOnBullet;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            //двигаем карту
            whoIAm.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(finishPosition1, startPosition, normalizedValue);

            yield return null;
        }


        //если могу дать ответный урон 
        if(canBackDamage && (!isIFriendly && aPW.isAmaTurn || isIFriendly && !aPW.isAmaTurn) && !goThatAttack.GetComponent<onTankAtWar>().isAmaHeadQuarter) {
            canBackDamage = false;
            this.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
            //StartCoroutine(backDamage(goThatAttack, whereToBack)); //ответный урон
            //Debug.Log(goThatAttack.transform.parent.GetSiblingIndex().ToString());
            //Debug.Break();
            //goThatAttack.GetComponent<onTankAtWar>().StartCoroutine(backDamage(whomAttack, whereToBack, goThatAttack));
            this.transform.Find("attackImg/disable").gameObject.SetActive(true); //показывает, что не может контратаковать
            goThatAttack.GetComponent<onTankAtWar>().startBackDamage(whomAttack, whereToBack, goThatAttack);
        } else {
            goThatAttack.GetComponent<onTankAtWar>().isAmaMove = false; //карту снова можно выбрать
            aPW.reckeckPossibilitiesOfCards();
        }
        

        



        yield break;
    }

    public void startBackDamage(GameObject goThatAttack, char direction, GameObject whomAttack) {
        StartCoroutine(backDamage(goThatAttack, direction, whomAttack));
    }

    public IEnumerator backDamage(GameObject goThatAttack, char direction, GameObject whomAttack) { //whomAttack - кого атаковали
        StartCoroutine(moveCardWhenAttack(goThatAttack, direction)); //двигаем карту при стрельбе, которая стреляла

        //StartCoroutine(attackAnim(this.gameObject, direction, direction)); //полёт пули во врага + смещенией карты после попадания + эффект взрыва + вылет урона(анимация) + обработка ответного выстрела (в конце)

        yield return goThatAttack.GetComponent<onTankAtWar>().StartCoroutine(attackAnim(goThatAttack, direction, direction, whomAttack));

        whomAttack.GetComponent<onTankAtWar>().isAmaMove = false; //карту снова можно выбрать

        aPW.reckeckPossibilitiesOfCards();
        yield break;
    }

    IEnumerator animDieCard(GameObject who, Transform parentOfGoThatAttack, char downOrUp) { //анимация смерти карты
        if(!isAmaHeadQuarter) { //если это не штаб
            //Transform parentOfGoThatAttack = who.transform.parent;
            GameObject newGO = Instantiate(aPW.prefOfAnimWhenDie, parentOfGoThatAttack);

            //who.GetComponent<CanvasGroup>().alpha = 0; //карту скрыли

            float sec = 0;
            while(sec <= aPW.animWhenDie.length) {
                sec += Time.deltaTime;
                who.GetComponent<CanvasGroup>().alpha = 1 - sec/aPW.animWhenDie.length;
                yield return null;
            }
            who.GetComponent<CanvasGroup>().alpha = 0;

            if(downOrUp == 'u') {
                newGO.transform.SetParent(aPW.objectWarPlace.transform.Find("upRightAngle"), true); //это нужно, чтобы карта улетала в правый верхний угол
            } else if(downOrUp == 'd') {
                newGO.transform.SetParent(aPW.objectWarPlace.transform.Find("downLeftAngle"), true); //это нужно, чтобы карта улетала в нижний левый угол
            } else {
                Debug.Log("ХУЕТА");
            }       

            Vector3 startPositionNew = newGO.GetComponent<RectTransform>().anchoredPosition;
            Vector3 finishPositionNew = Vector3.zero;

            newGO.GetComponent<moveAnimDie>().startMove(startPositionNew, finishPositionNew);

            if(who.GetComponent<onTankAtWar>().isIFriendly) { //наша карта
                aPW.numOfCardAtGarbage++;
                aPW.garbageCardText.text = aPW.numOfCardAtGarbage.ToString();
            } else { //если не наша карта
                aPW.cardsAtGarbageEnemy++;
                aPW.garbageCardEnemyText.text = aPW.cardsAtGarbageEnemy.ToString();
            }

            /*            aPW.cardsAtGarbageEnemy++;
                        aPW.garbageCardEnemyText.text = aPW.cardsAtGarbageEnemy.ToString();*/
            //Destroy(newGO.gameObject);

            //Debug.Log(who.name + " : " + who.GetComponent<onTankAtWar>().isIFriendly);

            if(who.GetComponent<onTankAtWar>().isIFriendly) {
                aPW.plusFuel -= who.GetComponent<onTankAtWar>().plusFuel; //теперь эта карта не даёт топлива
                aPW.objectWarPlace.transform.Find("map/Button(0,0)/plusFuel/Text").GetComponent<Text>().text = aPW.plusFuel.ToString();
            } else {
                aPW.plusFuelEnemy -= who.GetComponent<onTankAtWar>().plusFuel; //теперь эта карта не даёт топлива
                aPW.objectWarPlace.transform.Find("map/Button(4,2)/plusFuel/Text").GetComponent<Text>().text = aPW.plusFuelEnemy.ToString();
                //Debug.Log("Отнял");
            }
            

            Destroy(who.gameObject);

            yield break;
        }

        yield break;
    }

    IEnumerator animHealth(GameObject who, int countOfDamage, Transform parentOfGoThatAttack, bool isAmaHQ, char downOrUp) { //симуляция вылета урона
        GameObject anim = Instantiate(aPW.prefOfAnimHealth); //анимация выбивания урона
        anim.transform.Find("Text").GetComponent<Text>().text = countOfDamage.ToString();
        anim.transform.SetParent(who.transform, false); //кладем под того, кто атакован
        Destroy(anim.gameObject, aPW.animHealth.length); //удаляем эффект из-под карты

        float sec = 0;
        while(sec<aPW.animHealth.length) {
            sec += Time.deltaTime;
            yield return null;
        }

        if(!isAmaHQ) {
            if(who.GetComponent<onTankAtWar>().hp == 0) {
                StartCoroutine(animDieCard(who, parentOfGoThatAttack, downOrUp));
            }
        } else { //если штаб умер
            if(this.GetComponent<onTankAtWar>().hp == 0) {
                if(isIFriendly) { //мы проиграли
                    aPW.startOpenResult(false);
                    //aPW.objectWinInfo.SetActive(true); 
                } else { //противник проиграл
                    aPW.startOpenResult(true);
                    //aPW.objectWinInfo.SetActive(true);
                }
            }
        }
        /*else {
            who.transform.SetParent(parentOfGoThatAttack, true); //положили карту под клетку
        }*/
        

        yield break;
    }
    
    IEnumerator callExplosion(GameObject who) { //симуляция взрыва после выстрела
        GameObject anim = Instantiate(aPW.prefOfExplosionAnim);
        anim.transform.SetParent(who.transform, false);
        Destroy(anim.gameObject, aPW.explosionClip.length);

        yield break;
    }

    public void pointerOn() { //навели курсор на карту
        if(aPW == null) {
            aPW = GameObject.Find("Main Camera").GetComponent<atPlayerWindow>(); //получили основной скрипт
        }
        if(isIFriendly && (canAttack || countOfPosibleMove > 0)) { //если моя карта которая может атаковать или стрелять
            Cursor.SetCursor(aPW.cursors[1], Vector2.zero, CursorMode.Auto); //ставлю вместо курсора пальчик
        } else if(!isIFriendly && isAmaHeadQuarter) { //если утыкаемся во вражеский штаб
            if(this.transform.Find("shtabImg").Find("backLight").GetComponent<Image>().enabled == true) {
                Cursor.SetCursor(aPW.cursors[0], new Vector2(16, 16), CursorMode.Auto); //ставлю вместо курсора красную цель
                StartCoroutine(predictionOfDamage());
            }
        } else if(!isIFriendly && this.transform.Find("backLight").GetComponent<Image>().enabled == true) { //если вражеская карта и она подсвечена
            Cursor.SetCursor(aPW.cursors[0], new Vector2(16, 16), CursorMode.Auto); //ставлю вместо курсора красную цель
            StartCoroutine(predictionOfDamage());
        }
    }

    public IEnumerator predictionOfDamage() { //отабражает красную штуку с отображением дамага, который будет получен
        this.transform.Find("damagePredicion/Text").GetComponent<Text>().text = "-" + aPW.selectedCardMoveOrAttack.GetComponent<onTankAtWar>().damage.ToString();

        CanvasGroup canGr = this.transform.Find("damagePredicion").GetComponent<CanvasGroup>();

        float curTime = 0, timeOfTravel = 0.25f, normalizedValue;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            canGr.alpha = normalizedValue;

            yield return null;
        }

        yield break;
    }

    public IEnumerator offPredictionOfDamage() { //скрывает красную штуку с отображением дамага, который будет получен
        CanvasGroup canGr = this.transform.Find("damagePredicion").GetComponent<CanvasGroup>();

        float curTime = 0, timeOfTravel = 0.25f, normalizedValue;
        while(curTime < timeOfTravel) {
            curTime += Time.deltaTime;
            normalizedValue = curTime / timeOfTravel; // we normalize our time

            canGr.alpha = 1 - normalizedValue;

            yield return null;
        }

        yield break;
    }

    public void pointerExit() { //убрали курсор с карты
        if(!isIFriendly && isAmaHeadQuarter && this.transform.Find("shtabImg").Find("backLight").GetComponent<Image>().enabled == true) {
            StartCoroutine(offPredictionOfDamage()); //скрываю предсказывающую наносимый урон фигню
        } else if(!isIFriendly && !isAmaHeadQuarter && this.transform.Find("backLight").GetComponent<Image>().enabled == true) { //если вражеская карта и она подсвечена
            StartCoroutine(offPredictionOfDamage()); //скрываю предсказывающую наносимый урон фигню
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //ставлю стандартный курсор
    }
}

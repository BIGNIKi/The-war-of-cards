using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class atPlayerWindow : MonoBehaviour {
	public Text money; //текст с серебром
	public Text expi; //текст со свободным опытом
	public Text gold; //текст с голдой
	public Text nameOfProfile; //имя профиля
	public Text timeTextWhenFind; //текст со временем при поиске
	public Text myTime; //моё время в бою
	public Text enemyTime; //время противника в бою
	public Text stepTime; //время на ход
	public Text countOfCardEnemyText; //число карт в колоде соперника
	public Text garbageCardEnemyText; //карты противника в бито
	public Text garbageCardText; //наши карты в бито
	public Text countOfCardText; //число карт в нашей колоде

	public Animator animPanelChat; //аниматор кнопки с друзьями

	public Button chatButton; //кнопка с друзьями

	public GameObject backgroundObj; //объект где основной экран меню
	public GameObject prefOfWindowChatBttn; //префаб окна чата
	public GameObject objectAtResearch; //объект, который нужно включить на экране исследований
	public GameObject objectAtEdit; //объект, оторый нужно включить на экране редактора колод
	public GameObject objectFindProccess; //объект, который нужно включить при поиске соперника
	public GameObject objectWarPlace; //объект, который является игровым полем
	public GameObject darkImg; //затемнение в дереве исследований
	public GameObject sliderShtab; //слайд штабов
	public GameObject leftShtabBtn;
	public GameObject rightShtabBtn;
	public GameObject prefOfShtab; //префаб штаба
	public GameObject darkPanelFromMM; //темная панель из главного меню
	public GameObject contentCollection; //все танки в коллекции
	public GameObject contenColoda; //все танки в колоде
	public GameObject prefOfCardAtEditor; //префаб карты в редакторе колод
	public GameObject prefOfCardAtSelection; //префаб карты при выборе карт в бою
	public GameObject prefOfEnemyBackCard; //префаб задней части карт противника
	public GameObject prefOfCardAtWar; //префаб карты на поле сражения(уже поставленной в ячейку)
	public GameObject prefOfLightUnderCell; //префаб подсветки ячейки 
	public GameObject prefOfBullet; //префаб снаряда
	public GameObject selectedCardAtWar; //карта, на которую нажали в бою(карта в руке, не на поле)
	[Header("Кнопки на которые можно вывести карты")]
	public GameObject[] gButton; //кнопки под вывод карты
	public GameObject selectedCardMoveOrAttack; //карта, на которую нажали в бою(карте на поле, не в руке)
	public GameObject[] cellsGO; //объекты ячеек в бою в представлении id = x + 5*y
	public GameObject prefOfArrow; //префаб стрелки, которая отображается при движении танка
	public GameObject prefOfExplosionAnim; //префаб анимации взрыва
	public GameObject prefOfAnimHealth; //префаб с анимацией вылетающего урона
	public GameObject buttonOfEndTurn; //кнопка конца хода
	public GameObject prefOfAnimWhenDie; //префаб с анимацией смерти карты
	public GameObject[] prefOfFlags; //префабы развивающихся флагов 0 - СССР, 1 - Германия, 2 - США
	public GameObject objectWinInfo; //объект с информацией - выиграли/проиграли, сколько заработали

	public Image darkPanel; //панель для затемнения переходов
	public CanvasGroup darkPanelNew; //панель с общим альфа каналом
	public Text darkPanelText; //текст во время загрузки

	public Transform sizeFilterWithWindowChatBttn; //объект, под которым сортируются все окна чата

	public int canOpenNewWindow = 0; //проверка, завершила ли темная панель затемнять экран
									 //public int openWindowFinder = 0; //проверка, какое окно нужно открыть при поиске соперника
	public int numberShtabNow = 0; //номер штаба сейчас
	public int shtabAtAll; //всего штабов у игрока
	public int lastIdAtResearch; //id последней карточки, на которую нажали в исследованиях
	[Header("Число карт игрока на данной колоде(в меню)\nЧисло оставшихся карт(в бою)")]
	public int numOfCard; //число карт игрока на данной колоде
	public int numOfCardAtGarbage = 0; //число карт в "бито"
	[Header("Топлива у штаба")]
	public int fuelNow = 5;
	[Header("Дается топлива за ход")]
	public int plusFuel = 5;
	[Header("Номер хода")]
	public int numberOfMove = 0;
	[Header("Топлива у штаба соперника")]
	public int fuelNowEnemy = 5;
	[Header("Дается топлива за ход сопернику")]
	public int plusFuelEnemy = 5;
	[Header("Число карт в бито у противника")]
	public int cardsAtGarbageEnemy = 0;
	[Header("Число карт у противника в колоде")]
	public int cardsEnemy;


	[Header("Карты игрока, которые исследованы и/или куплены")]
	public resTech techs; //вся наша исследованная техника
	[Header("Данные о танках с сервера")]
	public paramUnits parUn; //цены техники

	public Sprite plaska0; //плашка исследований без галочки
	public Sprite plaska1; //плашка с галкой
	public Sprite coin0; //значок опыта
	public Sprite coin1; //значок денег
						 //public Sprite[] cardsImg; //картинки карточек
	public Sprite fonWhenWin; //фон, когда мы выиграли
	public Sprite fonWhenDefeat; //фон, когда мы проиграли

	[Header("Спрайты танков в высоком разрешении")]
	public imageWithName[] cardsImgNew;
	[Header("Спрайты танков в низком разрешении")]
	public imageWithName[] cardsImgLow;
	[Header("Рамки для карт на поле боя")]
	public imageWithName[] cardFrames;
	[Header("Картинки штабов")]
	public imageWithName[] shtabsImg; //картинки штабов
	[Header("Спрайты типов карт")]
	public imageWithName[] typesImg;
	[Header("Спрайты флагов для карт")]
	public imageWithName[] flagsImg;
	[Header("Малые спрайты флагов для карт в бою")]
	public imageWithName[] flagsImgSmall;
	[Header("Спрайты аватаров для штабов")]
	public imageWithName[] spritesAvatars;
	[Header("Спрайты флагов во время поиска")]
	public imageWithName[] flagsFinders;

	public Sprite[] backLightsForCardAtWar; //подсветки для карт на экране боя (карты, которые снизу при выборе чем походить)
	[Header("Картинки типов карточек в бою")]
	public Sprite[] typesAtWarMiddleImg; //картинки типов карточек
	[Header("Иконки урона")]
	public Sprite[] damageIconsImg; //иконки урона
	public Sprite[] twoSpritesForArrow; //два спрайта для стролочки (во время движения танка) - красного цвета
	public Sprite okantovkaEnemy; //красная окантовка противника

	public Profile mainInfoProfile; //вся информация о профиле

	[Header("Информация о штабах и колодах игрока")]
	public shtabsOfUser infoAboutShtab; //информация о штабах и колодах игрока

	public bool canSlideShtab = true; //могу слайдить штабы?
	public bool shouldCalculateTime = false; //должно ли время идти (когда ищем соперника)
	public bool isEnemyFound = false; //нашли ли соперника
	public bool canITouchCardAtWar = true; //можно ли нажать на карту в выборе карт (нельзя когда карта уже выбрана)
	public bool isOutputCard = false; //нажали ли мы на карту с целью вывести?
	public bool isMoveOrAttackCard = false; //нажали ли мы на карту которой можно атаковать или походить
	public bool isAmaTurn; //я хожу сейчас?
	private bool isInProccesStartCheckOnError = false; //исполняется ли уже startCheckOnError()
	private bool isBattleFinished = false; //кончился ли бой


	private int isUpdateInfoProfileFine = 0; //нормально ли выполнилось updateInfoProfile() 0 - не проверил 1 - успех 2 - ошибка
	private int isUpdateShtabInfoParalFine = 0; //нормально ли выполнилось updateShtabInfoParal()
	private int isGetInfoCardsFine = 0; //нормально ли выполнилось getInfoCards()
	private String errorTextUpdate; //текст ошибки
	private int isEditorOpenedFine = 0; //завершилось ли создание карт, когда вошли в редактор 0 - нет, 1 - успех
	private int isSaveInfoTanksFine = 0; //отправились ли данные на сервер успешно?
	private int isGoodStartFind = 0; //успешно ли начали поиск соперника?
	private int isErrorProccessFind = -1; //успешно ли отправили запрос о поиска соперника на сервер -1 - проблемы нет 0 - запрос на этапе отправки 1 - успешно 2 - ошибка
	private int shouldCheckEnemyReady = 0; // 1 - идет проверка, 0 - можно проверять, 2 - можно начинать бой 

	[Header("Информация о штабах с сервера")]
	public infoAboutAllShtab iAAS; //Информация о штабах с сервера

	public unitParam cardThatWeWantBuy; //карта, которую исследуем или покупаем

	[Header("Инфа о сопернике")]
	public enemyClass tempEnemy;

	[Header("Свободные карты во время боя")]
	public List<freeCard> freeCardsAtWar; //свободные карты во время боя

	[Header("Число минут в бою мои")]
	public int minutesMyAtWar;
	[Header("Число секунд в бою мои")]
	public float secondsMyAtWar;
	[Header("Число минут в бою соперника")]
	public int minutesEnemyAtWar;
	[Header("Число секунд в бою соперника")]
	public float secondsEnemyAtWar;
	[Header("Число минут в бою на ход")]
	public int minutesStepAtWar;
	[Header("Число секунд в бою на ход")]
	public float secondsStepAtWar;

	public Texture2D[] cursors; //массив текстур курсоров

	public AnimationClip explosionClip; //он нужен, чтобы получить его длительность и удалить объект через это число времени
	public AnimationClip animHealth; //он нужен, чтобы получить его длительность и удалить объект через это число времени
	public AnimationClip animWhenDie; //он нужен, чтобы получить его длительность и удалить объект через это число времени

	public Material redEnemyCardMaterial; //материал для подсветки вражеской карты, которую атакуем

	void Start() {

		//startChekOnError(); //проверка на ошибки + загрузка всей информации с сервера (как о профиле так и остальные данные)
	}

	void Update() {

	}

	public void openCloseChat() { //кнопка, где друзья

		if (animPanelChat.GetBool("isi")) {
			animPanelChat.SetBool("isi", false);
			chatButton.enabled = true;
		} else {
			animPanelChat.SetBool("isi", true);
			chatButton.enabled = false;
		}
	}

	public void openChatPanel(Animator downMenu) { //свернуть развернуть чат
		if (downMenu.GetBool("isi")) {
			downMenu.SetBool("isi", false);
		} else {
			downMenu.SetBool("isi", true);
		}
	}

	public void closeChatPanel(GameObject downMenu) { //закрыть чат
		Destroy(downMenu);
	}

	public void openGeneralChat() { //открывает общий чат
		if (GameObject.Find("Main Camera/Canvas/downPolosa/general") != null) {
			return;
		}
		GameObject neww = Instantiate(prefOfWindowChatBttn);
		neww.transform.SetParent(sizeFilterWithWindowChatBttn, false);
		neww.name = "general";
		neww.transform.Find("closeBtn").GetComponent<Button>().onClick.AddListener(delegate { closeChatPanel(neww); });
		neww.transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(delegate { openChatPanel(neww.transform.Find("panel").GetComponent<Animator>()); });
		neww.transform.Find("panel/upPanel/closeOnAlways").GetComponent<Button>().onClick.AddListener(delegate { openChatPanel(neww.transform.Find("panel").GetComponent<Animator>()); });
	}

	public void openResearch() { //открывает окно исследований
		if (canOpenNewWindow == 0) {
			darkPanel.enabled = true;
			canOpenNewWindow = 1;
			StartCoroutine(aphaChangeDark(0));
			StartCoroutine(updateInfoProfile()); //получить всю инфу юзера
			StartCoroutine(giveInfoResearch()); //инфа об исследованиях юзера
		} else if (canOpenNewWindow == 2) {
			darkPanel.enabled = true;
			canOpenNewWindow = 3;
			StartCoroutine(aphaChangeDark(0));
		}
	}

	public void startOpenResult(bool isWeWin) { //вызов этого метода ознаменует конец битвы
		isBattleFinished = true;
		StartCoroutine(resultsOpenParal(isWeWin));
	}
	public IEnumerator resultsOpenParal(bool isWeWin) { //плавно откроет окно результатов боя
		objectWinInfo.SetActive(true);

		int coinsGot, expGot; //серебра и опыта получено

		if(isWeWin) { //если мы выиграли
			objectWinInfo.transform.Find("fonWinDefeat").GetComponent<Image>().sprite = fonWhenWin; //поменяли фон
			objectWinInfo.transform.Find("backRamka/winOrNotText").GetComponent<Text>().text = "ПОБЕДА!";
			objectWinInfo.transform.Find("backRamka/descriptive").GetComponent<Text>().text = "Полная победа. Штаб противника уничтожен.";

			coinsGot = 3000;
			expGot = 250;
		} else {
			objectWinInfo.transform.Find("fonWinDefeat").GetComponent<Image>().sprite = fonWhenDefeat; //поменяли фон
			objectWinInfo.transform.Find("backRamka/winOrNotText").GetComponent<Text>().text = "ПОРАЖЕНИЕ";
			objectWinInfo.transform.Find("backRamka/descriptive").GetComponent<Text>().text = "Мы потерпели поражение. Наш штаб уничтожен.";

			coinsGot = 1000;
			expGot = 50;
		}

		int freeExp = (int)(expGot * 0.05f); //число свободного опыта

		objectWinInfo.transform.Find("backRamka/filterFinalPr/textCoins").GetComponent<Text>().text = coinsGot.ToString(); //число заработанных монет в шапке
		objectWinInfo.transform.Find("backRamka/filterFinalPr/textExpCommon").GetComponent<Text>().text = expGot.ToString(); //число заработанного опыта в шапке

		objectWinInfo.transform.Find("backRamka/withoutText/StandExpCountText").GetComponent<Text>().text = expGot.ToString(); //базовый опыт за бой
		objectWinInfo.transform.Find("backRamka/withoutText/StandAccruedText").GetComponent<Text>().text = expGot.ToString(); //итого начислено на штаб
		objectWinInfo.transform.Find("backRamka/withoutText/StandFreeText").GetComponent<Text>().text = freeExp.ToString(); //свободный опыт

		objectWinInfo.transform.Find("backRamka/withText/DonateExpCountText").GetComponent<Text>().text = ((int)(expGot*1.5f)).ToString(); //базовый опыт с премом
		objectWinInfo.transform.Find("backRamka/withText/DonateAccruedText").GetComponent<Text>().text = ((int)(expGot * 1.5f)).ToString(); //итого начислено с премом
		objectWinInfo.transform.Find("backRamka/withText/DonateFreeText").GetComponent<Text>().text = ((int)(expGot * 1.5f * 0.05f)).ToString(); //свободный опыт с премом

		objectWinInfo.transform.Find("backRamka/withoutText (1)/StandExpCountText").GetComponent<Text>().text = coinsGot.ToString(); //базовые кредиты за бой
		objectWinInfo.transform.Find("backRamka/withoutText (1)/StandAccruedText").GetComponent<Text>().text = "0"; //авто ремонт штаба
		objectWinInfo.transform.Find("backRamka/withoutText (1)/StandFreeText").GetComponent<Text>().text = coinsGot.ToString(); //итого

		objectWinInfo.transform.Find("backRamka/withText (1)/DonateExpCountText").GetComponent<Text>().text = ((int)(coinsGot * 1.5f)).ToString(); //базовые кредиты с премом
		objectWinInfo.transform.Find("backRamka/withText (1)/DonateAccruedText").GetComponent<Text>().text = "0"; //авто ремонт штаба с премом
		objectWinInfo.transform.Find("backRamka/withText (1)/DonateFreeText").GetComponent<Text>().text = ((int)(coinsGot * 1.5f)).ToString(); //итого кредитов с премом

		float time = 0;
		while(objectWinInfo.GetComponent<CanvasGroup>().alpha < 1) {
			objectWinInfo.GetComponent<CanvasGroup>().alpha = time;
			time += Time.deltaTime * 1f;
			yield return null; //продолжить после прохода итерации цикла Update
		}

		yield break;
	}

	public IEnumerator aphaChangeDark(int param) { //затемнуху будет регулировать
												   //параметр: 0 - исследования, 1 - редактор колод, 2 - поиск соперника, 3 - поле боя, 4 - ожидание подключения во время поиска соперника

		if(canOpenNewWindow == 1) { //отрываем
			if(param == 0) { //исследования
				darkPanelText.text = "Загрузка...";
			} else if(param == 1) {
				darkPanelText.text = "Загрузка...";
			} else if(param == 2) {
				darkPanelText.text = "Отправка запроса на сервер...";
			} else if(param == 3) {
				darkPanelText.text = "Загрузка...";
			} else if(param == 4) {
				darkPanelText.text = "Ожидание стабильного интернет-соединения...";
			}
		} else if(canOpenNewWindow == 3) {
			if(param == 0) {
				darkPanelText.text = "Отправка данных на сервер...";
			} else if(param == 1) {
				darkPanelText.text = "Отправка данных на сервер...";
			} else if(param == 2) {
				darkPanelText.text = "Отзываем поиск соперника...";
			}
		}

		while(darkPanelNew.alpha < 1) {
			darkPanelNew.alpha += Time.deltaTime;
			yield return null;
		}

		if (canOpenNewWindow == 1) {
			if (param == 0) {
				objectAtResearch.SetActive(true);
			} else if (param == 1) {
				objectAtEdit.SetActive(true);
			} else if (param == 2) {
				objectFindProccess.SetActive(true);
			} else if (param == 3) {
				objectWarPlace.gameObject.SetActive(true);
			}

		} else if (canOpenNewWindow == 3) {
			if (param == 0) {
				objectAtResearch.SetActive(false);
			} else if (param == 1) {
				objectAtEdit.SetActive(false);
			} else if (param == 2) {
				objectFindProccess.SetActive(false);
			}
		}

		//цикл ниже работает как задержка, пока редактор строит коллекцию и колоду игрока
		if(param == 1 && canOpenNewWindow == 1) { //если открываем редактор колод
			while(isEditorOpenedFine == 0) { //пока редактор колод еще не открылся
				yield return null; //продолжить после прохода итерации цикла Update
			}
			isEditorOpenedFine = 0;
		}

		if(param == 1 && canOpenNewWindow == 3) { //закрываем редактор
			while(isSaveInfoTanksFine == 0) { //пока данные еще не отправились на сервер
				yield return null; //продолжить после прохода итерации цикла Update
			}
			if(isSaveInfoTanksFine == 2) { //ошибка отправки данных на сервер
				if(darkPanelFromMM.name == "objForDebug") { //если запускали сцену из редактора (не через главное меню)
					Debug.LogError(errorTextUpdate);
					yield break;
				}

				Transform smth = darkPanelFromMM.transform.parent;
				smth.GetComponent<dontDestr>().enabled = true;
				smth.GetComponent<dontDestr>().goBackAtMenu(errorTextUpdate);
				yield break;
			}
			isSaveInfoTanksFine = 0;
		}

		if(param == 2 && canOpenNewWindow == 1) { //если пытаемся начать поиск соперника
			while(isGoodStartFind == 0) { //пока еще в попытках отправить запрос
				yield return null; //продолжить после прохода итерации цикла Update
			}
			if(isGoodStartFind == 2) { //не удалось начать поиск
				if(darkPanelFromMM.name == "objForDebug") { //если запускали сцену из редактора (не через главное меню)
					Debug.LogError(errorTextUpdate);
					yield break;
				}

				Transform smth = darkPanelFromMM.transform.parent;
				smth.GetComponent<dontDestr>().enabled = true;
				smth.GetComponent<dontDestr>().goBackAtMenu(errorTextUpdate);
				yield break;
			}
			isGoodStartFind = 0;
		}

		if(param == 4 && canOpenNewWindow == 1) { //ожидание подключения во время поиска соперника
			while(isErrorProccessFind == 0) { //пока еще в попытках отправить запрос
				yield return null; //продолжить после прохода итерации цикла Update
			}
			if(isErrorProccessFind == 1) { //успешно
				isErrorProccessFind = 0;
			} else if(isErrorProccessFind == 2) { //ошибка
				if(darkPanelFromMM.name == "objForDebug") { //если запускали сцену из редактора (не через главное меню)
					Debug.LogError(errorTextUpdate);
					yield break;
				}

				Transform smth = darkPanelFromMM.transform.parent;
				smth.GetComponent<dontDestr>().enabled = true;
				smth.GetComponent<dontDestr>().goBackAtMenu(errorTextUpdate);
				yield break;
			}
		}

		while(darkPanelNew.alpha > 0) {
			darkPanelNew.alpha -= Time.deltaTime;
			yield return null;
		}

		darkPanel.enabled = false;
		if (canOpenNewWindow == 1) {
			canOpenNewWindow = 2;
		} else if (canOpenNewWindow == 3) {
			canOpenNewWindow = 0;
		}


		yield break;
	}

	public void openEdit() { //открывает редактор колод
		if (canOpenNewWindow == 0) { //открывает
			darkPanel.enabled = true;
			canOpenNewWindow = 1;
			StartCoroutine(aphaChangeDark(1));
			StartCoroutine(whenEntireAtEditor()); //вхождение в редактор


		} else if (canOpenNewWindow == 2) { //закрывает
			darkPanel.enabled = true;
			canOpenNewWindow = 3;
			StartCoroutine(aphaChangeDark(1));
			StartCoroutine(whenExitFromEditor());
		}
	}

	private void createCardInEditor(unitParam statsOfNowCard, GameObject content, int countOfCards) { //создание карты в редакторе
		GameObject neww2 = Instantiate(prefOfCardAtEditor);
		neww2.transform.SetParent(content.transform, false);
		neww2.transform.SetSiblingIndex(content.transform.childCount - 2);
		//neww2.name = infoAboutShtab.shtabs[numberShtabNow].cards[index].name;
		neww2.name = statsOfNowCard.name;
		neww2.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text = statsOfNowCard.power.ToString();
		for(int k = 0; k < cardsImgNew.Length; k++) { //ставим основной спрайт
			if(cardsImgNew[k].name == neww2.name) {
				neww2.transform.Find("mainImg").GetComponent<Image>().sprite = cardsImgNew[k].img;
				break;
			}
			if(k == cardsImgNew.Length-1) {
				Debug.LogError("Нет нужной картинки карты");
			}
		}
		//if(content == contentCollection) { //пишем количество карт
			neww2.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + countOfCards.ToString();
		//}/* else {
		//	neww2.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + infoAboutShtab.shtabs[numberShtabNow].cards[index].count.ToString();
		//}*/
		
		neww2.transform.Find("papkaImg").GetComponent<Button>().onClick.AddListener(delegate { collectionOrColoda(neww2.gameObject); }); //добавляем событие реагирования на нажатие по карте

		//neww2.transform.Find("papkaImg").GetComponent<objectMain>().id = 1;

		neww2.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = countOfCards;

		//if(content == contentCollection) { //записали число карт в некий скрипт
			//neww2.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = countOfCards;
		//} else {
			//neww2.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = infoAboutShtab.shtabs[numberShtabNow].cards[index].count;
			//neww2.transform.Find("papkaImg").GetComponent<objectMain>().id = 1; //пометка карты, что она в колоде
		//}

		if(content == contenColoda) {
			neww2.transform.Find("papkaImg").GetComponent<objectMain>().id = 1; //пометка карты, что она в колоде
		}

		//neww2.transform.Find("papkaImg").GetComponent<objectMain>().name = infoAboutShtab.shtabs[numberShtabNow].cards[index].name; //карта должна хранить своё имя

		neww2.transform.Find("fuelCosts/Text").GetComponent<Text>().text = statsOfNowCard.needFuel.ToString(); //цена по топливу

		
		if(statsOfNowCard.russianName.Length > 11) { //пишем имя 12 символов макс, дальше многоточие
			neww2.transform.Find("name").GetComponent<Text>().text = statsOfNowCard.russianName.Substring(0, 11) + "...";
		} else {
			neww2.transform.Find("name").GetComponent<Text>().text = statsOfNowCard.russianName;
		}
		//neww2.transform.Find("name").GetComponent<Text>().text = statsOfNowCard.russianName. //отображение имени в самой игре

		if(statsOfNowCard.type == "OC") { //если приказ
			neww2.transform.Find("damageImg").gameObject.SetActive(false);
			neww2.transform.Find("hpImg").gameObject.SetActive(false);
		} else {
			neww2.transform.Find("damageImg/Text").GetComponent<Text>().text = statsOfNowCard.damage.ToString();
			neww2.transform.Find("hpImg/Text").GetComponent<Text>().text = statsOfNowCard.hp.ToString();
		}

		if(statsOfNowCard.plusFuel > 0) {
			neww2.transform.Find("plusFuel").gameObject.SetActive(true);
			neww2.transform.Find("plusFuel/Text").GetComponent<Text>().text = statsOfNowCard.plusFuel.ToString();
		} else {
			neww2.transform.Find("plusFuel").gameObject.SetActive(false);
		}

		for(int i = 0; i<typesImg.Length; i++) { //находим нужную картинку под тип карты
			if(typesImg[i].name == statsOfNowCard.type) {
				neww2.transform.Find("typeImg").GetComponent<Image>().sprite = typesImg[i].img;
				break;
			}
			if(i == typesImg.Length-1) {
				Debug.LogError("Не нашёл нужный тип карты");
			}
		}

		for(int i = 0; i < flagsImg.Length; i++) { //находим нужный флаг для нации карты
			if(flagsImg[i].name == statsOfNowCard.nation) {
				neww2.transform.Find("flagImg").GetComponent<Image>().sprite = flagsImg[i].img;
				break;
			}
			if(i == flagsImg.Length-1) {
				Debug.LogError("Не нашёл нужный флаг");
			}
		}

		neww2.transform.Find("infoText").GetComponent<Text>().text = statsOfNowCard.info; //краткая информация о карте
	}

	public IEnumerator whenEntireAtEditor() { //работает по вхождению в редактор
		//yield return updateInfoProfile(); //получить всю инфу юзера
		//yield return giveInfoResearch(); //(все харки танков с сервера)

		//следующие 2 цикла удаляют все карты в области коллекции и колоды
		int countChild = contentCollection.transform.childCount;
		for (int i = 0; i < countChild - 1; i++) {
			Destroy(contentCollection.transform.GetChild(i).gameObject);
		}
		countChild = contenColoda.transform.childCount;
		for (int i = 0; i < countChild - 1; i++) {
			Destroy(contenColoda.transform.GetChild(i).gameObject);
		}

		int countInDeck = infoAboutShtab.shtabs[numberShtabNow].cards.Count; //число НЕПОВТОРЯЮЩИХСЯ карт в колоде
		for(int i = 0; i<countInDeck; i++) { //создаем эти карты в колоде
			unitParam statsOfNowCard = new unitParam();
			for(int j = 0; j < parUn.unit.Length; j++) { //надо найти такую карту в списке (где статки всех карт)
				if(parUn.unit[j].name == infoAboutShtab.shtabs[numberShtabNow].cards[i].name) {
					statsOfNowCard = parUn.unit[j];
					break;
				}
				if(j == parUn.unit.Length - 1) {
					Debug.LogError("Карта в редакторе не нашлась");
				}
			}

			createCardInEditor(statsOfNowCard, contenColoda, infoAboutShtab.shtabs[numberShtabNow].cards[i].count); //создаст карту в колоде
		}

		int countInCollection = techs.units.Count; //число НЕПОВТОРЯЮЩИХСЯ карт, которые исследованы и/или куплены игроком
		for(int i = 0; i<countInCollection; i++) { //создаем эти карты в коллекции
			unitParam statsOfNowCard = new unitParam();
			for(int j = 0; j < parUn.unit.Length; j++) { //надо найти такую карту в списке (где статки всех карт)
				if(parUn.unit[j].name == techs.units[i].name) {
					statsOfNowCard = parUn.unit[j];
					break;
				}
				if(j == parUn.unit.Length - 1) {
					Debug.LogError("Карта в редакторе не нашлась");
				}
			}

			int countOfThatCardsAtDeck = 0; //число экземпляров этой карты в колоде
			for(int j = 0; j<infoAboutShtab.shtabs[numberShtabNow].cards.Count; j++) {
				if(infoAboutShtab.shtabs[numberShtabNow].cards[j].name == techs.units[i].name) {
					countOfThatCardsAtDeck = infoAboutShtab.shtabs[numberShtabNow].cards[j].count;
					break;
				}
				/*if(j == infoAboutShtab.shtabs[numberShtabNow].cards.Count - 1) {
					Debug.Log("Карта в редакторе не нашлась");
				}*/
			}

			int numchik = techs.units[i].count - countOfThatCardsAtDeck; //число карт в коллекции
			if(numchik > 0) {
				createCardInEditor(statsOfNowCard, contentCollection, numchik); //создаст карту в коллекции
			}
			
		}

		isEditorOpenedFine = 1; //редактор успешно отработал

		/*int countOfTanks = infoAboutShtab.shtabs[numberShtabNow].cards.Count; //количество карт у данного штаба игрока
		for (int i = 0; i < countOfTanks; i++) { //надо генерить те карты в редакторе колод
			unitParam statsOfNowCard = new unitParam();
			for (int j = 0; j < parUn.unit.Length; j++) { //надо найти такую карту в списке (где статки всех карт)
				if (parUn.unit[j].name == infoAboutShtab.shtabs[numberShtabNow].cards[i].name) {
					statsOfNowCard = parUn.unit[j];
					break;
				}
				if (j == parUn.unit.Length - 1) {
					Debug.LogError("Карта в редакторе не нашлась");
				}
			}


			if (infoAboutShtab.shtabs[numberShtabNow].cards[i].countCollection > 0) { //создание карты в коллекции
				GameObject neww1 = Instantiate(prefOfCardAtEditor);
				neww1.transform.SetParent(contentCollection.transform, false);
				//Debug.Log(contentCollection.transform.childCount - 1);
				neww1.transform.SetSiblingIndex(contentCollection.transform.childCount - 2);
				neww1.name = infoAboutShtab.shtabs[numberShtabNow].cards[i].name;
				neww1.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text = statsOfNowCard.power.ToString();
				for (int k = 0; k < cardsImgNew.Length; k++) {
					if (cardsImgNew[k].name == neww1.name) {
						neww1.transform.Find("mainImg").GetComponent<Image>().sprite = cardsImgNew[k].img;
						break;
					}
				}
				neww1.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + infoAboutShtab.shtabs[numberShtabNow].cards[i].countCollection.ToString();
				neww1.transform.Find("papkaImg").GetComponent<Button>().onClick.AddListener(delegate { collectionOrColoda(neww1.gameObject); });
				neww1.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = infoAboutShtab.shtabs[numberShtabNow].cards[i].countCollection;
				neww1.transform.Find("fuelCosts/Text").GetComponent<Text>().text = statsOfNowCard.needFuel.ToString();
				neww1.transform.Find("name").GetComponent<Text>().text = statsOfNowCard.russianName;
				neww1.transform.Find("damageImg/Text").GetComponent<Text>().text = statsOfNowCard.damage.ToString();
				neww1.transform.Find("hpImg/Text").GetComponent<Text>().text = statsOfNowCard.hp.ToString();
				if (statsOfNowCard.plusFuel > 0) {
					neww1.transform.Find("plusFuel").gameObject.SetActive(true);
					neww1.transform.Find("plusFuel/Text").GetComponent<Text>().text = statsOfNowCard.plusFuel.ToString();
				} else {
					neww1.transform.Find("plusFuel").gameObject.SetActive(false);
				}

			}
			if (infoAboutShtab.shtabs[numberShtabNow].cards[i].countColoda > 0) { //создание карты в колоде
				GameObject neww2 = Instantiate(prefOfCardAtEditor);
				neww2.transform.SetParent(contenColoda.transform, false);
				neww2.transform.SetSiblingIndex(contenColoda.transform.childCount - 2);
				neww2.name = infoAboutShtab.shtabs[numberShtabNow].cards[i].name;
				neww2.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text = statsOfNowCard.power.ToString();
				for (int k = 0; k < cardsImgNew.Length; k++) {
					if (cardsImgNew[k].name == neww2.name) {
						neww2.transform.Find("mainImg").GetComponent<Image>().sprite = cardsImgNew[k].img;
						break;
					}
				}
				neww2.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + infoAboutShtab.shtabs[numberShtabNow].cards[i].countColoda.ToString();
				neww2.transform.Find("papkaImg").GetComponent<Button>().onClick.AddListener(delegate { collectionOrColoda(neww2.gameObject); });
				neww2.transform.Find("papkaImg").GetComponent<objectMain>().id = 1;
				neww2.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = infoAboutShtab.shtabs[numberShtabNow].cards[i].countColoda;
				neww2.transform.Find("fuelCosts/Text").GetComponent<Text>().text = statsOfNowCard.needFuel.ToString();
				neww2.transform.Find("name").GetComponent<Text>().text = statsOfNowCard.russianName;
				neww2.transform.Find("damageImg/Text").GetComponent<Text>().text = statsOfNowCard.damage.ToString();
				neww2.transform.Find("hpImg/Text").GetComponent<Text>().text = statsOfNowCard.hp.ToString();
				if (statsOfNowCard.plusFuel > 0) {
					neww2.transform.Find("plusFuel").gameObject.SetActive(true);
					neww2.transform.Find("plusFuel/Text").GetComponent<Text>().text = statsOfNowCard.plusFuel.ToString();
				} else {
					neww2.transform.Find("plusFuel").gameObject.SetActive(false);
				}
			}
		}*/


		yield break;
	}

	public IEnumerator whenExitFromEditor() { //работает при выходе из редактора
		for (int i = infoAboutShtab.shtabs[numberShtabNow].cards.Count-1; i >= 0; i--) {
			infoAboutShtab.shtabs[numberShtabNow].cards.RemoveAt(i);
		}

		for (int i = 0; i < contenColoda.transform.childCount; i++) {
			if(contenColoda.transform.GetChild(i).name == "plusImg") {
				break;
			}

			cardForChangeColoda newCFCC = new cardForChangeColoda();
			newCFCC.name = contenColoda.transform.GetChild(i).name;
			newCFCC.count = contenColoda.transform.GetChild(i).transform.Find("papkaImg").GetComponent<objectMain>().countOfCard;
			infoAboutShtab.shtabs[numberShtabNow].cards.Add(newCFCC);
			
		}

		mainInfoProfile.shtabs = JsonUtility.ToJson(infoAboutShtab);
		StartCoroutine(saveInfoTanks());

		yield break;
	}

	private IEnumerator getInfoCards() { //получает всю инфу о характеристиках карт с сервера
		WWWForm form1 = new WWWForm();
		UnityWebRequest www1 = UnityWebRequest.Post("http://thearmynations.ru/main/allParamResearch.php", form1);
		yield return www1.SendWebRequest(); //ждет, пока не отправит
		if(www1.error != null || www1.isNetworkError || www1.isHttpError) {
			isGetInfoCardsFine = 2; //произошла ошибка 
			errorTextUpdate = www1.error;
			yield break;
		}
		parUn = JsonUtility.FromJson<paramUnits>("{\"unit\":" + www1.downloadHandler.text + "}"); //получили все характеристики карт с сервера


		isGetInfoCardsFine = 1; //ошибки не было

		yield break;
	}

	private IEnumerator giveInfoResearch() { //дает инфу об исследованиях
		//WWWForm form = new WWWForm();
		//form.AddField("Login", mainInfoProfile.login);
		//UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/getUserInfo.php", form);
		//yield return www.SendWebRequest(); //ждет, пока не отправит
		//Profile pr = JsonUtility.FromJson<Profile>(www.downloadHandler.text);
		//techs = JsonUtility.FromJson<resTech>(pr.info_tanks);

		//techs = JsonUtility.FromJson<resTech>(mainInfoProfile.info_tanks); //парсим инфу о картах, записывая её в соответствующую структуру

/*		WWWForm form1 = new WWWForm();
		UnityWebRequest www1 = UnityWebRequest.Post("http://thearmynations.ru/main/allParamResearch.php", form1);
		yield return www1.SendWebRequest(); //ждет, пока не отправит
		parUn = JsonUtility.FromJson<paramUnits>("{\"unit\":" + www1.downloadHandler.text + "}"); //получили все характеристики карт с сервера*/

		objectAtResearch.transform.Find("fieldUssr0/panelExp/filterPrice/textCoin").GetComponent<Text>().text = infoAboutShtab.shtabs[0].exp.ToString();
		for (int i = 0; i < parUn.unit.Length; i++) {
			settingCardResearchNew(parUn.unit[i]);
		}
		yield break;
	}

	public void settingCardResearchNew(unitParam unit) {
		int index = -1;
		for (int i = 0; i < techs.units.Count; i++) {
			if (unit.name == techs.units[i].name) {
				index = i;
				break;
			}
		}
		if (index >= 0) { //нашли у клиента информацию о данной карте
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/countImg/Text").GetComponent<Text>().text = "x" + techs.units[index].count;
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/countImg/Text").gameObject.SetActive(true);
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/countImg/closed").gameObject.SetActive(false);
			if (techs.units[index].count != 3) { //есть, но не максимум
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price").GetComponent<Image>().sprite = plaska0;
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").gameObject.SetActive(true);
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").GetComponent<Text>().text = unit.monPrice.ToString();
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Image").gameObject.SetActive(true);
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Image").GetComponent<Image>().sprite = coin1;
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").GetComponent<Text>().color = new Color(181 / 255f, 182 / 255f, 181 / 255f);
			} else { //максиум карт
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price").GetComponent<Image>().sprite = plaska1;
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").gameObject.SetActive(false);
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Image").gameObject.SetActive(false);
			}
		} else { //у клиента такой карты нет
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/countImg/Text").gameObject.SetActive(false);
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/countImg/closed").gameObject.SetActive(true);
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price").GetComponent<Image>().sprite = plaska0;
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").gameObject.SetActive(true);
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").GetComponent<Text>().text = unit.expPrice.ToString();
			if (unit.expPrice <= infoAboutShtab.shtabs[0].exp + mainInfoProfile.expi) {
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").GetComponent<Text>().color = new Color(131 / 255f, 224 / 255f, 57 / 255f);
			} else {
				objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Text").GetComponent<Text>().color = new Color(178 / 255f, 13 / 255f, 13 / 255f);
			}
			objectAtResearch.transform.Find("fieldUssr0/" + unit.name + "/price/Image").gameObject.SetActive(true);
		}
	}

	public IEnumerator changeAlpha() { //анимация затемнения на карте в исследованиях
		float time = 0;
		while (darkImg.GetComponent<CanvasGroup>().alpha < 1) {
			darkImg.GetComponent<CanvasGroup>().alpha = time;
			time += Time.deltaTime * 1f;
			yield return null; //продолжить после прохода итерации цикла Update
		}
		yield break;
	}

	public void clickOnCardResearch(int id) { //нажатие на карту в исследованиях
		lastIdAtResearch = id;

		darkImg.SetActive(true);
		darkImg.GetComponent<CanvasGroup>().alpha = 0;
		for (int i = 0; i < cardsImgNew.Length; i++) {
			if (cardsImgNew[i].name == parUn.unit[id].name) {
				darkImg.transform.Find("mainImg").GetComponent<Image>().sprite = cardsImgNew[i].img;
				break;
			}
			if (i == cardsImgNew.Length - 1) {
				Debug.LogError("Не нашел картинку для карточки в исследованиях");
			}
		}
		//darkImg.transform.Find("mainImg").GetComponent<Image>().sprite = cardsImg[id];
		darkImg.transform.Find("fuelCosts/Text").GetComponent<Text>().text = parUn.unit[id].needFuel.ToString();
		darkImg.transform.Find("name").GetComponent<Text>().text = parUn.unit[id].russianName;
		darkImg.transform.Find("discript").GetComponent<Text>().text = parUn.unit[id].info;
		darkImg.transform.Find("damageImg/Text").GetComponent<Text>().text = parUn.unit[id].damage.ToString();
		darkImg.transform.Find("hpImg/Text").GetComponent<Text>().text = parUn.unit[id].hp.ToString();
		if (parUn.unit[id].plusFuel == 0) {
			darkImg.transform.Find("plusFuel").GetComponent<Image>().enabled = false;
		} else {
			darkImg.transform.Find("plusFuel").GetComponent<Image>().enabled = true;
			darkImg.transform.Find("plusFuel/Text").GetComponent<Text>().text = parUn.unit[id].plusFuel.ToString();
		}
		darkImg.transform.Find("label/power/Text").GetComponent<Text>().text = "<color=#FFFFFF><size=10>" + parUn.unit[id].power + "</size></color><color=#909090> - Сила карты</color>";
		darkImg.transform.Find("label/costs/textExp").GetComponent<Text>().text = "<color=#83E039><size=10>" + parUn.unit[id].expPrice + "</size></color> - <color=#909090>Стоимость исследования</color>";
		darkImg.transform.Find("label/costs/textCoin").GetComponent<Text>().text = "<color=#B5B6B5><size=10>" + parUn.unit[id].monPrice + "</size></color> <color=#909090>- Стоимость покупки</color> ";
		bool finishCorrect = false; //закончил заподлнять большую карточку
		cardThatWeWantBuy = parUn.unit[id]; //записали инфо о карте, которую сейчас хотим купить или исследовать
		for (int i = 0; i < techs.units.Count; i++) {
			if (parUn.unit[id].name == techs.units[i].name) {
				if (techs.units[i].count == 3) { //максимальное количество карт уже куплено
					darkImg.transform.Find("buying/UPtext").GetComponent<Text>().text = "Куплено карт: 3";
					darkImg.transform.Find("buying/filterPrice").gameObject.SetActive(false);
					darkImg.transform.Find("buying/midText").GetComponent<Text>().enabled = false;
					darkImg.transform.Find("buying/filterFinalPr").gameObject.SetActive(false);
					darkImg.transform.Find("buying/researchBtn").gameObject.SetActive(false);
					darkImg.transform.Find("buying/buyBtn").gameObject.SetActive(false);
				} else {
					darkImg.transform.Find("buying/filterPrice/ExpImg").GetComponent<Image>().sprite = coin1;
					darkImg.transform.Find("buying/filterPrice/textCoin").GetComponent<Text>().text = parUn.unit[id].monPrice.ToString();
					darkImg.transform.Find("buying/filterPrice").gameObject.SetActive(true);
					darkImg.transform.Find("buying/midText").GetComponent<Text>().enabled = false;
					darkImg.transform.Find("buying/filterFinalPr").gameObject.SetActive(false);
					darkImg.transform.Find("buying/researchBtn").gameObject.SetActive(false);
					if (mainInfoProfile.money >= parUn.unit[id].monPrice) { //если хватает денег
						darkImg.transform.Find("buying/UPtext").GetComponent<Text>().text = "Доступна покупка (куплено " + techs.units[i].count + "/3):";
						darkImg.transform.Find("buying/buyBtn").gameObject.SetActive(true);
						darkImg.transform.Find("buying/filterPrice/textCoin").GetComponent<Text>().color = new Color(181 / 255f, 182 / 255f, 181 / 255f);
					} else { //не хватает денег
						darkImg.transform.Find("buying/UPtext").GetComponent<Text>().text = "Покупка невозможна (куплено " + techs.units[i].count + "/3):";
						darkImg.transform.Find("buying/buyBtn").gameObject.SetActive(false);
						darkImg.transform.Find("buying/filterPrice/textCoin").GetComponent<Text>().color = new Color(178 / 255f, 13 / 255f, 13 / 255f);
					}
				}



				finishCorrect = true;
				break;
			}
		}
		if (!finishCorrect) { //не нашли карту у клиента
			darkImg.transform.Find("buying/filterPrice/ExpImg").GetComponent<Image>().sprite = coin0;
			darkImg.transform.Find("buying/filterPrice").gameObject.SetActive(true);
			darkImg.transform.Find("buying/filterPrice/textCoin").GetComponent<Text>().text = parUn.unit[id].expPrice.ToString();
			darkImg.transform.Find("buying/midText").GetComponent<Text>().enabled = true;
			darkImg.transform.Find("buying/filterFinalPr").gameObject.SetActive(true);
			darkImg.transform.Find("buying/buyBtn").gameObject.SetActive(false);

			if (parUn.unit[id].expPrice <= infoAboutShtab.shtabs[0].exp + mainInfoProfile.expi) { //если хватает опыта
				darkImg.transform.Find("buying/UPtext").GetComponent<Text>().text = "Доступно исследование:";
				darkImg.transform.Find("buying/midText").GetComponent<Text>().text = "Будет потрачено:";
				darkImg.transform.Find("buying/filterPrice/textCoin").GetComponent<Text>().color = new Color(131 / 255f, 224 / 255f, 57 / 255f);
				if (infoAboutShtab.shtabs[0].exp >= parUn.unit[id].expPrice) { //если хватает опыта (без свободного)
					darkImg.transform.Find("buying/filterFinalPr/textExpBase").GetComponent<Text>().text = parUn.unit[id].expPrice.ToString();
					darkImg.transform.Find("buying/filterFinalPr/textExpCommon").GetComponent<Text>().text = "0";
				} else { //со свободным
					darkImg.transform.Find("buying/filterFinalPr/textExpBase").GetComponent<Text>().text = infoAboutShtab.shtabs[0].exp.ToString();
					darkImg.transform.Find("buying/filterFinalPr/textExpCommon").GetComponent<Text>().text = (parUn.unit[id].expPrice - infoAboutShtab.shtabs[0].exp).ToString();
				}
				darkImg.transform.Find("buying/researchBtn").gameObject.SetActive(true);
			} else { //если не хватает опыта
				darkImg.transform.Find("buying/UPtext").GetComponent<Text>().text = "Исследование недоступно:";
				darkImg.transform.Find("buying/filterPrice/textCoin").GetComponent<Text>().color = new Color(178 / 255f, 13 / 255f, 13 / 255f);
				darkImg.transform.Find("buying/midText").GetComponent<Text>().text = "Есть в наличии:";
				darkImg.transform.Find("buying/filterFinalPr/textExpBase").GetComponent<Text>().text = infoAboutShtab.shtabs[0].exp.ToString();
				darkImg.transform.Find("buying/filterFinalPr/textExpCommon").GetComponent<Text>().text = mainInfoProfile.expi.ToString();
				darkImg.transform.Find("buying/researchBtn").gameObject.SetActive(false);
				//darkImg.transform.Find("buying/midText").GetComponent<Text>().enabled = false;
				//darkImg.transform.Find("buying/filterFinalPr/").gameObject.SetActive(false);
			}

		}
		StartCoroutine(changeAlpha()); //анимация затемнения на карте в исследованиях
	}

	public void closeCardResearch() { //закрытие карты в исследованиях
		StartCoroutine(closeChangeAlpha());
	}

	public IEnumerator closeChangeAlpha() { //анимация исчезания карты исследований
		float time = 1;
		while (darkImg.GetComponent<CanvasGroup>().alpha > 0) {
			darkImg.GetComponent<CanvasGroup>().alpha = time;
			time -= Time.deltaTime * 1f;
			yield return null; //продолжить после прохода итерации цикла Update
		}
		darkImg.SetActive(false);
		yield break;
	}

	public void startChekOnError() { //запустит ожидание выполнения обновлений загрузки с сервера и запустит сами эти обновления
		if(!isInProccesStartCheckOnError) { //если такой процесс сейчас не запущен, то запускаем
			//Debug.Log("Only one");
			isInProccesStartCheckOnError = true;
			StartCoroutine(updateInfoProfile()); //загружает данные клиента users
			StartCoroutine(updateShtabInfoParal()); //подгружает с сервера данные о штабах
			StartCoroutine(getInfoCards()); //получаем всю инфу о картах карт с сервера
			StartCoroutine(checkOnError());
		}
	}

	public IEnumerator checkOnError() { //проверка на ошибки в updateInfoProfile() и updateShtabInfoParal()
		while(isUpdateInfoProfileFine  == 0 || isUpdateShtabInfoParalFine == 0 || isGetInfoCardsFine == 0) { //пока не проверили на ошибки
			if(isUpdateInfoProfileFine == 2 || isUpdateShtabInfoParalFine == 2 || isGetInfoCardsFine == 2) { //словили ошибку
				break;
			}
			yield return null;
		}
		if(isUpdateInfoProfileFine == 2 || isUpdateShtabInfoParalFine == 2 || isGetInfoCardsFine == 2) { //нашли ошибку
			if(darkPanelFromMM.name == "objForDebug") { //если запускали сцену из редактора (не через главное меню)
				Debug.LogError(errorTextUpdate);
				isInProccesStartCheckOnError = false; //пометили процесс startChekOnError() как неактивный
				yield break;
			}
			
			Transform smth = darkPanelFromMM.transform.parent;
			smth.GetComponent<dontDestr>().enabled = true;
			smth.GetComponent<dontDestr>().goBackAtMenu(errorTextUpdate);
			yield break;
		}

		isInProccesStartCheckOnError = false; //пометили процесс startChekOnError() как неактивный
		isUpdateInfoProfileFine = 0;
		isUpdateShtabInfoParalFine = 0;
		isGetInfoCardsFine = 0;
		darkPanelFromMM.SetActive(false);
		yield break;
	}

	public void startUpdate() {  //загружает данные клиента users
		StartCoroutine(updateInfoProfile());
	}

	public IEnumerator updateInfoProfile() { //загружает данные клиента users
		/*		float timeiii = 0;
				while(timeiii <= 5f) {
					timeiii += Time.deltaTime;
					yield return null;
				}*/
		//Debug.Log("Дошёл");

		//Debug.Log(mainInfoProfile.login);
		WWWForm form = new WWWForm();
		form.AddField("Login", mainInfoProfile.login);
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/getUserInfo.php", form);

		yield return www.SendWebRequest(); //ждет, пока не отправит
		if (www.error != null || www.isNetworkError || www.isHttpError) {
			isUpdateInfoProfileFine = 2; //произошла ошибка 
			errorTextUpdate = www.error;
			yield break;
		}

		mainInfoProfile = JsonUtility.FromJson<Profile>(www.downloadHandler.text);
		nameOfProfile.text = mainInfoProfile.login;
		money.text = mainInfoProfile.money.ToString();
		expi.text = mainInfoProfile.expi.ToString();
		gold.text = mainInfoProfile.gold.ToString();
		infoAboutShtab = JsonUtility.FromJson<shtabsOfUser>(mainInfoProfile.shtabs);
		techs = JsonUtility.FromJson<resTech>(mainInfoProfile.info_tanks); //парсим инфу о картах, записывая её в соответствующую структуру (здесь будут только исследованные и купленные карты)


		isUpdateInfoProfileFine = 1; //ошибки не было

		yield break;
	}

	public void leftRightSlide(int id) { //слайдим штаб 0-влево, 1-вправо
		if (canSlideShtab) {
			if (id == 0 && numberShtabNow > 0) {
				canSlideShtab = false;
				numberShtabNow--;
				StartCoroutine(smoothSlide(-1));
				rightShtabBtn.SetActive(true);
				if (numberShtabNow == 0) {
					leftShtabBtn.SetActive(false);
				}
			}
			if (id == 1 && (shtabAtAll - numberShtabNow - 1) > 0) {
				canSlideShtab = false;
				numberShtabNow++;
				StartCoroutine(smoothSlide(1));
				leftShtabBtn.SetActive(true);
				if (numberShtabNow + 1 == shtabAtAll) {
					rightShtabBtn.SetActive(false);
				}
			}

			for(int w = 0; w < iAAS.iAOS.Length; w++) { //ставим название штаба
				if(iAAS.iAOS[w].name == infoAboutShtab.shtabs[numberShtabNow].name) {
					backgroundObj.transform.Find("Profile/NameBaseText").GetComponent<Text>().text = iAAS.iAOS[w].info0;

					for(int b = 0; b < flagsFinders.Length; b++) { //настраиваем флаг позади аватара
						if(flagsFinders[b].name == iAAS.iAOS[w].nation) {
							backgroundObj.transform.Find("Profile/flagImg").GetComponent<Image>().sprite = flagsFinders[b].img;

							break;
						}
						if(b == spritesAvatars.Length - 1) {
							Debug.Log("Не нашёл флаг");
						}
					}

					break;
				}
				if(w == iAAS.iAOS.Length - 1) {
					Debug.Log("Не нашел название штаба");
				}
			}

			for(int w = 0; w < spritesAvatars.Length; w++) { //ищем и ставим аватар
				if(spritesAvatars[w].name == infoAboutShtab.shtabs[numberShtabNow].name) {
					backgroundObj.transform.Find("Profile/avaProfile").GetComponent<Image>().sprite = spritesAvatars[w].img;

					break;
				}
				if(w == spritesAvatars.Length - 1) {
					Debug.Log("Не нашёл аватар");
				}
			}
		}
	}

	public IEnumerator smoothSlide(int multy) { //реализация плавного движения штабов
												//int need = sliderShtab.GetComponent<HorizontalLayoutGroup>().padding.left - 225 * multy;
		Vector2 from = sliderShtab.GetComponent<RectTransform>().anchoredPosition;
		Vector2 to = new Vector2(from.x - 225 * multy, from.y);

		Vector3 startScale = new Vector3(1, 1, 1);
		Vector3 finishScale = new Vector3(0.6f, 0.6f, 0.6f);

		float progress = 0;
		float progressMe = 0;
		while ((sliderShtab.GetComponent<RectTransform>().anchoredPosition - to).sqrMagnitude > 0.01f) {
			progress += 1 * Time.deltaTime;
			progressMe += 1 * Time.deltaTime;
			if (multy == 1) {
				sliderShtab.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(from, to, progress);
				sliderShtab.transform.GetChild(numberShtabNow - 1).GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, finishScale, progressMe);
				sliderShtab.transform.GetChild(numberShtabNow).GetComponent<RectTransform>().localScale = Vector3.Lerp(finishScale, startScale, progressMe);
			} else {
				sliderShtab.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(from, to, progress);
				sliderShtab.transform.GetChild(numberShtabNow + 1).GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, finishScale, progressMe);
				sliderShtab.transform.GetChild(numberShtabNow).GetComponent<RectTransform>().localScale = Vector3.Lerp(finishScale, startScale, progressMe);
			}


			yield return null; //продолжить после прохода итерации цикла Update
		}
		canSlideShtab = true;
		yield break;
	}

	public void updateShtabInfo() { //подгружает с сервера данные о штабах
		StartCoroutine(updateShtabInfoParal());
	}

	public IEnumerator updateShtabInfoParal() { //подгружает с сервера данные о штабах
		WWWForm form = new WWWForm();
		WWWForm form1 = new WWWForm();
		form1.AddField("Login", mainInfoProfile.login);
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/updateShtabInfo.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит
		UnityWebRequest www1 = UnityWebRequest.Post("http://thearmynations.ru/main/getInfoAbouShtabsClient.php", form1);
		yield return www1.SendWebRequest(); //ждет, пока не отправит
		if (www.error != null) {
			Debug.Log(www.error);
			isUpdateShtabInfoParalFine = 2; //произошла ошибка
			errorTextUpdate = www.error;
			yield break;
		}
		if (www1.error != null) {
			Debug.Log(www.error);
			isUpdateShtabInfoParalFine = 2; //произошла ошибка
			errorTextUpdate = www.error;
			yield break;
		}
		//iAAS - инфа о всех штабах, которые есть в игре (тоесть это не штабы игрока)
		iAAS = JsonUtility.FromJson<infoAboutAllShtab>("{\"iAOS\":" + www.downloadHandler.text + "}");

		//здесь инфа о штабах игрока
		infoAboutShtab = JsonUtility.FromJson<shtabsOfUser>(www1.downloadHandler.text);

		int countChild = sliderShtab.transform.childCount; //получает число штабов под слайдером штабов
		for (int i = 0; i < countChild; i++) { //и все их удаляет
			Destroy(sliderShtab.transform.GetChild(i).gameObject);
		}

		if (infoAboutShtab.shtabs.Count == 1) { //если штаб всего один, выключает правый слайдер
			rightShtabBtn.SetActive(false);
		}

		//создание всех штабов
		for (int i = 0; i < infoAboutShtab.shtabs.Count; i++) {
			GameObject neww = Instantiate(prefOfShtab);
			if(i != 0) {
				neww.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f); //размеры иконки всех кроме того, который выбран сейчас должны быть маленькими
			}
			neww.transform.SetParent(sliderShtab.transform, false);
			neww.name = infoAboutShtab.shtabs[i].name;
			int idAtBase = 0;
			for (; idAtBase < iAAS.iAOS.Length; idAtBase++) { //находит индекс в базе данных
				if (infoAboutShtab.shtabs[i].name == iAAS.iAOS[idAtBase].name) {
					break;
				}
			}
			for (int j = 0; j < shtabsImg.Length; j++) {
				if (shtabsImg[j].name == neww.name) {
					neww.transform.GetComponent<Image>().sprite = shtabsImg[j].img;
					break;
				}
			}
			neww.transform.Find("bodyPapkaImg/structure/NameBaseText").GetComponent<Text>().text = iAAS.iAOS[idAtBase].info0;
			neww.transform.Find("bodyPapkaImg/structure/discriptionBaseText").GetComponent<Text>().text = iAAS.iAOS[idAtBase].info1;
			neww.transform.Find("bodyPapkaImg/structure/kolodaText").GetComponent<Text>().text = iAAS.iAOS[idAtBase].info2;
			neww.transform.Find("bodyPapkaImg/structure/sortToCentreFIST/powerText").GetComponent<Text>().text = infoAboutShtab.shtabs[i].power.ToString();
			neww.transform.Find("bodyPapkaImg/structure/sortToCentreEXP/expText").GetComponent<Text>().text = infoAboutShtab.shtabs[i].exp.ToString();
			String tmp = iAAS.iAOS[idAtBase].nation; //нация штаба
			if(tmp.Equals("USSR")) { //здесь генерю нужный флаг
				Instantiate(prefOfFlags[0], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
			} else if(tmp.Equals("Germany")) {
				Instantiate(prefOfFlags[1], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
			} else if(tmp.Equals("USA")) {
				Instantiate(prefOfFlags[2], neww.transform.Find("bodyPapkaImg/structure/flagImg"));
			}
		}
		shtabAtAll = infoAboutShtab.shtabs.Count; //количество штабов

		//возможно костыльная фигня, стоит переписать когда будет > 1 штаба
/*		numOfCard = 0;
		for (int i = 0; i < infoAboutShtab.shtabs[numberShtabNow].cards.Count; i++) { //посчитает число карт в колоде (НЕ в коллекции!)
			numOfCard += infoAboutShtab.shtabs[numberShtabNow].cards[i].countColoda;
		}*/

		isUpdateShtabInfoParalFine = 1; //ошибки не было

		yield break;
	}

	public void pressResearchCard() { //нажал исследовать карту
		if (cardThatWeWantBuy.expPrice <= infoAboutShtab.shtabs[0].exp) { //хватает опыта штаба
			infoAboutShtab.shtabs[0].exp -= cardThatWeWantBuy.expPrice;
		} else { //не хватает опыта штаба
			int waste = cardThatWeWantBuy.expPrice - infoAboutShtab.shtabs[0].exp;
			infoAboutShtab.shtabs[0].exp = 0; //опыт штаба отняли
			mainInfoProfile.expi -= waste; //свободный опыт отняли
		}
		unit newUnit = new unit();
		newUnit.count = 0;
		newUnit.name = cardThatWeWantBuy.name;
		techs.units.Add(newUnit);
		mainInfoProfile.info_tanks = JsonUtility.ToJson(techs); //инфу о новом исследовании запомнили

		cardForChangeColoda newCard = new cardForChangeColoda();
		newCard.name = cardThatWeWantBuy.name;
		//newCard.count = 0;
		//newCard.countCollection = 0;
		//newCard.countColoda = 0;
		newCard.count = 0;
		infoAboutShtab.shtabs[0].cards.Add(newCard);
		mainInfoProfile.shtabs = JsonUtility.ToJson(infoAboutShtab); //штаб теперь тоже знает о новой карте

		clickOnCardResearch(lastIdAtResearch); //обновили экран исследования
		StartCoroutine(saveInfoTanks()); //запомили на сервере

		expi.text = mainInfoProfile.expi.ToString();
		objectAtResearch.transform.Find("fieldUssr0/panelExp/filterPrice/textCoin").GetComponent<Text>().text = infoAboutShtab.shtabs[0].exp.ToString();
		for (int i = 0; i < parUn.unit.Length; i++) {
			settingCardResearchNew(parUn.unit[i]);
		}
		updateShtabInfo(); //обновляет всю инфу о штабах на гл экране (спорная фича, только в серву зря обращаюсь)

	}

	public IEnumerator saveInfoTanks() { //сохраняю main инфу on server
		WWWForm form = new WWWForm();
		form.AddField("Login", mainInfoProfile.login);
		form.AddField("Money", mainInfoProfile.money);
		form.AddField("Expi", mainInfoProfile.expi);
		form.AddField("Gold", mainInfoProfile.gold);
		form.AddField("InfoTanks", mainInfoProfile.info_tanks);
		form.AddField("Shtabs", mainInfoProfile.shtabs);

		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/saveResearchInfo.php", form);
		yield return www.SendWebRequest(); //ждет, пока отправит
										   //Debug.Log("Сохранил");

/*		float timePassed = 0;
		while((www.error != null || www.isNetworkError || www.isHttpError) && timePassed<= 15f) { //15 секунд на то, чтобы восстановить интернет-соединение
			darkPanelText.text = "Попытка подключения к серверу...";
			Debug.Log(timePassed.ToString());

			timePassed += Time.deltaTime;

			form = new WWWForm();
			form.AddField("Login", mainInfoProfile.login);
			form.AddField("Money", mainInfoProfile.money);
			form.AddField("Expi", mainInfoProfile.expi);
			form.AddField("Gold", mainInfoProfile.gold);
			form.AddField("InfoTanks", mainInfoProfile.info_tanks);
			form.AddField("Shtabs", mainInfoProfile.shtabs);
			www = UnityWebRequest.Post("http://thearmynations.ru/main/saveResearchInfo.php", form);

			yield return www.SendWebRequest();

			yield return null;
		}*/

		if(www.error != null || www.isNetworkError || www.isHttpError) {
			isSaveInfoTanksFine = 2; //произошла ошибка 
			errorTextUpdate = www.error;
			yield break;
		}

		isSaveInfoTanksFine = 1; //всё успешно
		yield break;
	}

	public void pressBuyCard() { //нажал купить карту
		mainInfoProfile.money -= cardThatWeWantBuy.monPrice;
		for (int i = 0; i < techs.units.Count; i++) {
			if (techs.units[i].name == cardThatWeWantBuy.name) {
				techs.units[i].count++;

				break;
			}
		}
		mainInfoProfile.info_tanks = JsonUtility.ToJson(techs); //инфу о новом исследовании запомнили

		for (int i = 0; i < infoAboutShtab.shtabs[0].cards.Count; i++) {
			if (infoAboutShtab.shtabs[0].cards[i].name == cardThatWeWantBuy.name) {
				//ПЕРЕДЕЛЫВАТЬ строку ниже закомментил, так как countCollection больше не существует
				//infoAboutShtab.shtabs[0].cards[i].countCollection++;
				break;
			}
		}
		mainInfoProfile.shtabs = JsonUtility.ToJson(infoAboutShtab); //штаб теперь тоже знает о новой карте

		clickOnCardResearch(lastIdAtResearch); //обновили экран исследования
		StartCoroutine(saveInfoTanks()); //запомили на сервере

		money.text = mainInfoProfile.money.ToString();
		for (int i = 0; i < parUn.unit.Length; i++) {
			settingCardResearchNew(parUn.unit[i]);
		}
	}

	public void collectionOrColoda(GameObject me) { //нажатие на карту в реадакторе колод
		if (me.transform.Find("papkaImg").GetComponent<objectMain>().id == 0) { //когда карта лежит в коллекции
			for (int i = 0; i < contenColoda.transform.childCount - 1; i++) { //поиск совпадающей карты в коллекции
				if (contenColoda.transform.GetChild(i).name == me.name) { //нашли такую же карту
					me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard--;
					me.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard.ToString();
					if (me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard <= 0) {
						Destroy(me);
					}
					contenColoda.transform.GetChild(i).transform.Find("papkaImg").GetComponent<objectMain>().countOfCard++;
					contenColoda.transform.GetChild(i).transform.Find("countImg/Text").GetComponent<Text>().text = "x" + contenColoda.transform.GetChild(i).transform.Find("papkaImg").GetComponent<objectMain>().countOfCard;
					return;
				}
			}

			GameObject neww1 = Instantiate(prefOfCardAtEditor);
			neww1.transform.SetParent(contenColoda.transform, false);
			neww1.transform.SetSiblingIndex(contenColoda.transform.childCount - 2);
			neww1.name = me.name;
			neww1.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text = me.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text; //здесь пишется сила карты
			neww1.transform.Find("mainImg").GetComponent<Image>().sprite = me.transform.Find("mainImg").GetComponent<Image>().sprite;
			neww1.transform.Find("countImg/Text").GetComponent<Text>().text = "x1";
			neww1.transform.Find("papkaImg").GetComponent<Button>().onClick.AddListener(delegate { collectionOrColoda(neww1.gameObject); });
			neww1.transform.Find("papkaImg").GetComponent<objectMain>().id = 1;
			neww1.transform.Find("fuelCosts/Text").GetComponent<Text>().text = me.transform.Find("fuelCosts/Text").GetComponent<Text>().text;
			neww1.transform.Find("name").GetComponent<Text>().text = me.transform.Find("name").GetComponent<Text>().text;
			neww1.transform.Find("damageImg/Text").GetComponent<Text>().text = me.transform.Find("damageImg/Text").GetComponent<Text>().text;
			neww1.transform.Find("hpImg/Text").GetComponent<Text>().text = me.transform.Find("hpImg/Text").GetComponent<Text>().text;
			if (me.transform.Find("plusFuel").gameObject.activeSelf == true) {
				neww1.transform.Find("plusFuel").gameObject.SetActive(true);
				neww1.transform.Find("plusFuel/Text").GetComponent<Text>().text = me.transform.Find("plusFuel/Text").GetComponent<Text>().text;
			} else {
				neww1.transform.Find("plusFuel").gameObject.SetActive(false);
			}
			neww1.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = 1;
			me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard--;
			me.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard.ToString();
			if (me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard <= 0) {
				Destroy(me);
			}
		} else { //когда карта в колоде
			for (int i = 0; i < contentCollection.transform.childCount - 1; i++) { //поиск совпадающей карты в коллекции
				if (contentCollection.transform.GetChild(i).name == me.name) { //нашли такую же карту
					me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard--;
					me.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard.ToString();
					if (me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard <= 0) {
						Destroy(me);
					}
					contentCollection.transform.GetChild(i).transform.Find("papkaImg").GetComponent<objectMain>().countOfCard++;
					contentCollection.transform.GetChild(i).transform.Find("countImg/Text").GetComponent<Text>().text = "x" + contentCollection.transform.GetChild(i).transform.Find("papkaImg").GetComponent<objectMain>().countOfCard;
					return;
				}
			}

			GameObject neww1 = Instantiate(prefOfCardAtEditor);
			neww1.transform.SetParent(contentCollection.transform, false);
			neww1.transform.SetSiblingIndex(contentCollection.transform.childCount - 2);
			neww1.name = me.name;
			neww1.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text = me.transform.Find("downPlaska/moneyHere/Text").GetComponent<Text>().text; //здесь пишется сила карты
			neww1.transform.Find("mainImg").GetComponent<Image>().sprite = me.transform.Find("mainImg").GetComponent<Image>().sprite;
			neww1.transform.Find("countImg/Text").GetComponent<Text>().text = "x1";
			neww1.transform.Find("papkaImg").GetComponent<Button>().onClick.AddListener(delegate { collectionOrColoda(neww1.gameObject); });
			neww1.transform.Find("papkaImg").GetComponent<objectMain>().id = 0;
			neww1.transform.Find("fuelCosts/Text").GetComponent<Text>().text = me.transform.Find("fuelCosts/Text").GetComponent<Text>().text;
			neww1.transform.Find("name").GetComponent<Text>().text = me.transform.Find("name").GetComponent<Text>().text;
			neww1.transform.Find("damageImg/Text").GetComponent<Text>().text = me.transform.Find("damageImg/Text").GetComponent<Text>().text;
			neww1.transform.Find("hpImg/Text").GetComponent<Text>().text = me.transform.Find("hpImg/Text").GetComponent<Text>().text;
			if (me.transform.Find("plusFuel").gameObject.activeSelf == true) {
				neww1.transform.Find("plusFuel").gameObject.SetActive(true);
				neww1.transform.Find("plusFuel/Text").GetComponent<Text>().text = me.transform.Find("plusFuel/Text").GetComponent<Text>().text;
			} else {
				neww1.transform.Find("plusFuel").gameObject.SetActive(false);
			}
			neww1.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard = 1;
			me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard--;
			me.transform.Find("countImg/Text").GetComponent<Text>().text = "x" + me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard.ToString();
			if (me.transform.Find("papkaImg").GetComponent<objectMain>().countOfCard <= 0) {
				Destroy(me);
			}
		}

	}

	public void exitToMainMenuAfterBattle() { //выход в главное меню после боя
		StartCoroutine(exitToMainMenuAfterBattleParall());
	}

	public IEnumerator exitToMainMenuAfterBattleParall() {
		darkPanel.enabled = true;

		darkPanelText.text = "Загрузка...";

		while(darkPanelNew.alpha < 1) {
			darkPanelNew.alpha += Time.deltaTime;
			yield return null;
		}

		for(int i = 1; i<cellsGO.Length-1; i++) { //обойдет клетки с 1-ой по 13-ю включительно
			if(cellsGO[i].transform.childCount != 0) {
				Destroy(cellsGO[i].transform.GetChild(0).gameObject);
			}
		}
		for(int i = objectWarPlace.transform.Find("myCards").transform.childCount-1; i>= 0; i--) { //удалит все карты у себя в руке
			Destroy(objectWarPlace.transform.Find("myCards").transform.GetChild(i).gameObject);
		}
		for(int i = objectWarPlace.transform.Find("enemyCards").transform.childCount-1; i>=0; i--) { //удалит карты из руки противника
			Destroy(objectWarPlace.transform.Find("enemyCards").transform.GetChild(i).gameObject);
		}
		objectWarPlace.SetActive(false);
		objectWinInfo.SetActive(false);
		objectWinInfo.GetComponent<CanvasGroup>().alpha = 0;
		objectFindProccess.SetActive(false);


		while(darkPanelNew.alpha > 0) {
			darkPanelNew.alpha -= Time.deltaTime;
			yield return null;
		}

		darkPanel.enabled = false;

		isBattleFinished = false; //восстановление значения в стандартное состояние
		tempEnemy = null; //в стандартное значение
		canOpenNewWindow = 0; //в стандартное значение
		isEnemyFound = false; //в стандартное значение

		objectFindProccess.transform.Find("stopBtn").gameObject.GetComponent<CanvasGroup>().alpha = 1;
		objectFindProccess.transform.Find("stopBtn").gameObject.GetComponent<Button>().enabled = false;
		objectFindProccess.transform.Find("mapa").gameObject.GetComponent<CanvasGroup>().alpha = 1;
		objectFindProccess.transform.Find("flagImgEnemy").gameObject.GetComponent<CanvasGroup>().alpha = 0;
		objectFindProccess.transform.Find("crack").gameObject.GetComponent<CanvasGroup>().alpha = 0;

		cellsGO[0].GetComponent<onTankAtWar>().canAttack = true; //нашему штабу восстанавливаем возможность стрелять
		yield break;
	}

	public void startFindWar() { //поиск соперника по бою
		if (canOpenNewWindow == 0) {
			darkPanel.enabled = true;
			canOpenNewWindow = 1;
			StartCoroutine(aphaChangeDark(2));
			shouldCalculateTime = true;
			objectFindProccess.transform.Find("flagImg/nameOfProfile").GetComponent<Text>().text = mainInfoProfile.login; //ставим своё имя профиля
			//objectFindProccess.transform.Find("flagImg/nameOfShtab").GetComponent<Text>().text = infoAboutShtab.shtabs[numberShtabNow].info; //ставим имя штаба

			for(int w = 0; w<iAAS.iAOS.Length; w++) { //ставим название штаба
				if(iAAS.iAOS[w].name == infoAboutShtab.shtabs[numberShtabNow].name) {
					objectFindProccess.transform.Find("flagImg/nameOfShtab").GetComponent<Text>().text = iAAS.iAOS[w].info0;

					for(int b = 0; b < flagsFinders.Length; b++) { //настраиваем флаг позади аватара
						if(flagsFinders[b].name == iAAS.iAOS[w].nation) {
							objectFindProccess.transform.Find("flagImg").GetComponent<Image>().sprite = flagsFinders[b].img;

							break;
						}
						if(b == spritesAvatars.Length - 1) {
							Debug.Log("Не нашёл флаг");
						}
					}

					break;
				}
				if(w == iAAS.iAOS.Length-1) {
					Debug.Log("Не нашел название штаба");
				}
			}

			for(int w = 0; w<spritesAvatars.Length; w++) { //ищем и ставим аватар
				if(spritesAvatars[w].name == infoAboutShtab.shtabs[numberShtabNow].name) {
					objectFindProccess.transform.Find("flagImg/faceId").GetComponent<Image>().sprite = spritesAvatars[w].img;

					break;
				}
				if(w == spritesAvatars.Length-1) {
					Debug.Log("Не нашёл аватар");
				}
			}

			numOfCard = 0;
			for (int i = 0; i < infoAboutShtab.shtabs[numberShtabNow].cards.Count; i++) { //посчитает число карт в колоде (НЕ в коллекции!)
				numOfCard += infoAboutShtab.shtabs[numberShtabNow].cards[i].count;
			}
			//StartCoroutine(giveInfoResearch()); //инфа об исследованиях юзера
			StartCoroutine(findEnemyParal());
		} else {
			darkPanel.enabled = true;
			canOpenNewWindow = 3;
			shouldCalculateTime = false;
			StartCoroutine(deleteCellWhenFind());
			StartCoroutine(aphaChangeDark(2));
		}
	}

	public IEnumerator timeWillGoOn() { //запускает таймеры (которые во время боя)
		secondsMyAtWar = 0; //число секунд мои до конца минуты
		minutesMyAtWar = 15; //число минут до конца боя мои
		secondsEnemyAtWar = 0;
		minutesEnemyAtWar = 15;
		minutesStepAtWar = 2; //число минут на ход
		secondsStepAtWar = 0;
		if(isAmaTurn) {
			myTime.color = new Color(0.7529412f, 0.7568628f, 0.7411765f); //делаю свое время белым
			stepTime.color = new Color(0.259167f, 0.6037736f, 0.3227867f); //сделал время на ход зеленым
		} else {
			enemyTime.color = new Color(0.7529412f, 0.7568628f, 0.7411765f); //делаю время противника белым
			stepTime.color = new Color(0.6698113f, 0.1737718f, 0.1789934f); //сделал время на ход красным
			buttonOfEndTurn.gameObject.SetActive(false); //оключает кнопку конца хода
		}
		while(true) {
			switch(isAmaTurn) {
				case true:
					if(secondsMyAtWar <= 0) {
						minutesMyAtWar--;
						secondsMyAtWar = 60;
					}
					if(secondsStepAtWar <= 0) {
						minutesStepAtWar--;
						secondsStepAtWar = 60;
					}
					secondsMyAtWar -= Time.deltaTime;
					secondsStepAtWar -= Time.deltaTime;
					myTime.text = minutesMyAtWar.ToString("00") + ":" + ((int)secondsMyAtWar).ToString("00");
					stepTime.text = minutesStepAtWar.ToString("00") + ":" + ((int)secondsStepAtWar).ToString("00");

					yield return null;
					break;

				case false:
					if(secondsEnemyAtWar <= 0) {
						minutesEnemyAtWar--;
						secondsEnemyAtWar = 60;
					}
					if(secondsStepAtWar <= 0) {
						minutesStepAtWar--;
						secondsStepAtWar = 60;
					}
					secondsEnemyAtWar -= Time.deltaTime;
					secondsStepAtWar -= Time.deltaTime;
					enemyTime.text = minutesEnemyAtWar.ToString("00") + ":" + ((int)secondsEnemyAtWar).ToString("00");
					stepTime.text = minutesStepAtWar.ToString("00") + ":" + ((int)secondsStepAtWar).ToString("00");

					yield return null;
					break;
			}

			if(isBattleFinished) { //если бой кончился
				break;
			}
		}
		yield break;
	}

	private void createNewCardForMeAtWar() { //создаёт карту в руке для себя (не противнику)
		int srandom = UnityEngine.Random.Range(0, freeCardsAtWar.Count); //случайный номер карты которую хотим сгенерить
		GameObject neww = Instantiate(prefOfCardAtSelection);
		neww.transform.SetParent(objectWarPlace.transform.Find("myCards"), false); //полужил туда, где карты игрока
		neww.name = freeCardsAtWar[srandom].name;

		for(int j = 0; j < cardsImgLow.Length; j++) { //замена картинки карты
			if(cardsImgLow[j].name == neww.name) {
				neww.GetComponent<Image>().sprite = cardsImgLow[j].img;
				break;
			}
			if(j == cardsImgLow.Length-1) {
				Debug.Log("Спрайт танка не найден");
			}
		}
		for(int j = 0; j < parUn.unit.Length; j++) { //нашли всю инфу о данной карте
			if(neww.name == parUn.unit[j].name) {
				neww.GetComponent<rememberPos>().uP = parUn.unit[j];
				neww.transform.Find("fuelCost/Text").GetComponent<Text>().text = parUn.unit[j].needFuel.ToString();
				neww.transform.Find("nameTxt").GetComponent<Text>().text = parUn.unit[j].russianName;
				if(parUn.unit[j].type == "OC") { //если это карта - приказ
					neww.transform.Find("attackImg").gameObject.SetActive(false);
					neww.transform.Find("hpImg").gameObject.SetActive(false);
				} else {
					neww.transform.Find("attackImg/Text").GetComponent<Text>().text = parUn.unit[j].damage.ToString();
					neww.transform.Find("hpImg/Text").GetComponent<Text>().text = parUn.unit[j].hp.ToString();
					if(parUn.unit[j].plusFuel != 0) {
						neww.transform.Find("plusFuel").gameObject.SetActive(true);
						neww.transform.Find("plusFuel/Text").GetComponent<Text>().text = parUn.unit[j].plusFuel.ToString();
					}
				}

				for(int w = 0; w < typesImg.Length; w++) { //выставляю тип карты
					if(typesImg[w].name == parUn.unit[j].type) {
						neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesImg[w].img;
						break;
					}
					if(w == typesImg.Length - 1) {
						Debug.Log("Не нашел спрайт типа карты");
					}
				}

				neww.transform.Find("description").GetComponent<Text>().text = parUn.unit[j].info; //описание карты в студию

				for(int w = 0; w<flagsImgSmall.Length; w++) { //настраиваем национальный флаг
					if(parUn.unit[j].nation == flagsImgSmall[w].name) {
						neww.transform.Find("flagImg").GetComponent<Image>().sprite = flagsImgSmall[w].img;

						break;
					}
					if(w == flagsImgSmall.Length-1) {
						Debug.Log("Не нашёл флаг");
					}
				}

				break;
			}
			if(j == parUn.unit.Length-1) {
				Debug.Log("Не нашел информацию о карте");
			}
		}
		if(isAmaTurn == false) { //если сейчас хожу не я
			neww.transform.Find("backLight").gameObject.SetActive(false); //убрал зеленый орнамент
			neww.transform.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнил картинку карты
		}


		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener(delegate { pointerOn(neww.transform.Find("btn").transform.parent.gameObject); });
		neww.transform.Find("btn").GetComponent<EventTrigger>().triggers.Add(entry);

		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerExit;
		entry.callback.AddListener(delegate { pointerExit(neww.transform.Find("btn").transform.parent.gameObject); });
		neww.transform.Find("btn").GetComponent<EventTrigger>().triggers.Add(entry);

		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener(delegate { pointerClick(neww.transform.Find("btn").transform.parent.gameObject); });
		neww.transform.Find("btn").GetComponent<EventTrigger>().triggers.Add(entry);



		freeCardsAtWar[srandom].count--;
		if(freeCardsAtWar[srandom].count <= 0) {
			freeCardsAtWar.RemoveAt(srandom);
		}

		objectWarPlace.transform.Find("myCards").GetComponent<HorizontalLayoutGroup>().enabled = true; //временное решение - включает сортировку карт в руке
	}

	public IEnumerator findEnemyParal() { //поиск соперника с подключением к серверу
		WWWForm form = new WWWForm();
		form.AddField("Login", mainInfoProfile.login);
		infoToEnemyMain temp = new infoToEnemyMain();
		temp.countOfCard = numOfCard; //чтобы записать в таблице поиска main инфу о себе
		temp.nameOfShtab = infoAboutShtab.shtabs[numberShtabNow].name; //отправляем имя штаба
		form.AddField("infoToEnemyMain", JsonUtility.ToJson(temp)); //отправила на сервер логин и число карт
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/wantFindEnemy.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит

		if(www.error != null || www.isNetworkError || www.isHttpError) {
			isGoodStartFind = 2; //всё плохо

			errorTextUpdate = www.error;
			if(darkPanelFromMM.name == "objForDebug") { //если запускали сцену из редактора (не через главное меню)
				Debug.LogError(errorTextUpdate);
				yield break;
			}

			Transform smth = darkPanelFromMM.transform.parent;
			smth.GetComponent<dontDestr>().enabled = true;
			smth.GetComponent<dontDestr>().goBackAtMenu(errorTextUpdate);
			yield break;
		}
		isGoodStartFind = 1; //всё успешно

		if (www.downloadHandler.text == "Complete") { //когда добавил в tablefindenemy
			float serverTime = 0;
			float seconds = 0;
			float minute = 0;
			while (shouldCalculateTime == true && isEnemyFound == false) { //пока не нашли соперника
				seconds += Time.deltaTime;
				serverTime += Time.deltaTime;
				if (seconds >= 60) {
					minute++;
					seconds = 0;
				}
				if (serverTime >= 1.5f) {
					serverTime = 0;
					StartCoroutine(moveCell());
					//StartCoroutine(aLotOfRequest());

					if(isErrorProccessFind == -1) {
						isErrorProccessFind = 0;
						StartCoroutine(aLotOfRequest());
					}

					//yield return aLotOfRequest();
				}
				timeTextWhenFind.text = "ПОИСК ПРОТИВНИКА: " + minute.ToString() + ":" + ((int)seconds).ToString("00");
				yield return null;
			}

			if (isEnemyFound == true) { //нашли соперника
				shouldCalculateTime = false;
				float curTime = 0, timeOfTravel = 1f, normalizedValue;
				GameObject oFPSB = objectFindProccess.transform.Find("stopBtn").gameObject;
				oFPSB.GetComponent<Button>().enabled = false;
				GameObject oFPM = objectFindProccess.transform.Find("mapa").gameObject;
				GameObject oFPFIE = objectFindProccess.transform.Find("flagImgEnemy").gameObject;
				GameObject oFPC = objectFindProccess.transform.Find("crack").gameObject;
				while (curTime < timeOfTravel) {
					curTime += Time.deltaTime;
					normalizedValue = curTime / timeOfTravel; // we normalize our time
					oFPSB.GetComponent<CanvasGroup>().alpha = 1 - normalizedValue;
					oFPM.GetComponent<CanvasGroup>().alpha = 1 - normalizedValue;
					oFPFIE.GetComponent<CanvasGroup>().alpha = normalizedValue;
					oFPC.GetComponent<CanvasGroup>().alpha = normalizedValue;
					yield return null;
				}
				float newTime = 0;
				//canOpenNewWindow = 0;
				while (newTime < 3) {
					newTime += Time.deltaTime;
					//после этого таймера надо переходить на экран боя
					yield return null;
				}
				darkPanel.enabled = true;
				canOpenNewWindow = 1;
				StartCoroutine(aphaChangeDark(3)); //откроет красивенько окно боя


				int howManyCardWeNeed = 6; //сколько карт нужно положить в руку
				if (howManyCardWeNeed > tempEnemy.enemy.infoEnemy.countOfCard) { //если у игрока меньше 6 карт с собой
					howManyCardWeNeed = tempEnemy.enemy.infoEnemy.countOfCard;
				}
				for (int i = 0; i< howManyCardWeNeed; i++) { //генерация задника карт противника
					GameObject neww = Instantiate(prefOfEnemyBackCard);
					neww.transform.SetParent(objectWarPlace.transform.Find("enemyCards"), false);
				}
				tempEnemy.enemy.infoEnemy.countOfCard -= howManyCardWeNeed;
				//objectWarPlace.transform.Find("map/Button(4,2)/cardsImg/atAll").GetComponent<Text>().text = tempEnemy.enemy.infoEnemy.countOfCard.ToString();
				cardsEnemy = tempEnemy.enemy.infoEnemy.countOfCard;
				countOfCardEnemyText.text = cardsEnemy.ToString(); //выставили число карт соперника

				howManyCardWeNeed = 6;
				if (howManyCardWeNeed > numOfCard) { //если у игрока меньше 6 карт с собой
					howManyCardWeNeed = numOfCard;
				}
				//Debug.Log("Всего карт: " + howManyCardWeNeed.ToString());

				for (int i = 0; i< howManyCardWeNeed; i++) { //генерим префабы карт в руку
					createNewCardForMeAtWar();
				}
				if(isAmaTurn) { //если я сейчас хожу
					cellsGO[14].transform.Find("shtabImg/downPolosa").GetComponent<Image>().enabled = false; //выключили красную херню выше штаба противника
					reCheckAllCardAtWar(); //см описание функции(подсветка карт, которые можно вывести на поле)
					cellsGO[14].transform.Find("shtabImg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили штаб противника
					cellsGO[14].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(true); //включили изображение факта, что штаб противника не может контратаковать
					cellsGO[0].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(false);
					cellsGO[0].transform.Find("shtabImg").GetComponent<Image>().color = new Color(1, 1, 1, 1);
					buttonOfEndTurn.gameObject.SetActive(true);
					cellsGO[0].transform.Find("shtabImg/backlight").GetComponent<Image>().enabled = true; //включили зеленый орнамент(анимированный) на нашем штабе
					//cellsGO[14].transform.Find("shtabImg/backLight").GetComponent<Image>().enabled = false; //выключили орнамент противнику
				} else {
					cellsGO[0].transform.Find("downPolosa").GetComponent<Image>().enabled = false; //выключили зеленую херню под своим штабом
					cellsGO[0].transform.Find("shtabImg/backlight").GetComponent<Image>().enabled = false; //выключили зеленый орнамент(анимированный) на нашем штабе
					//cellsGO[14].transform.Find("shtabImg/backLight").GetComponent<Image>().enabled = true; //включили орнамент противнику
					cellsGO[0].transform.Find("shtabImg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили свой штаб
					cellsGO[14].transform.Find("shtabImg").GetComponent<Image>().color = new Color(1, 1, 1, 1);
					cellsGO[0].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(true); //включили изображение факта, что наш штаб не может контратаковать
					cellsGO[14].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(false);
					buttonOfEndTurn.gameObject.SetActive(false);
				}

				numOfCard -= howManyCardWeNeed;
				//objectWarPlace.transform.Find("map/Button(0,0)/cardsImg/atAll").GetComponent<Text>().text = numOfCard.ToString();
				countOfCardText.text = numOfCard.ToString(); // число наших карт в колоде
				
				StartCoroutine(timeWillGoOn()); //запускает таймер у того, кто ходит
			}
			if (!shouldCalculateTime) {
				//objectWarPlace.transform.Find("myCards").GetComponent<HorizontalLayoutGroup>().
				yield break;
			}
		}

		yield break;
	}

	public IEnumerator moveCell() { //двигает цель
		float x = UnityEngine.Random.Range(-209f, 105);
		float y = UnityEngine.Random.Range(-65f, 145);
		float curTime = 0, timeOfTravel = 1f, normalizedValue;
		Vector3 startPosition = objectFindProccess.transform.Find("mapa/target").GetComponent<RectTransform>().anchoredPosition;
		while (curTime < timeOfTravel) {
			curTime += Time.deltaTime;
			normalizedValue = curTime / timeOfTravel; // we normalize our time 
			objectFindProccess.transform.Find("mapa/target").GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, new Vector3(x, y, startPosition.z), normalizedValue);
			yield return null;
		}

		yield break;
	}

	public IEnumerator deleteCellWhenFind() { //удаление игрока из поиска противника
		WWWForm form = new WWWForm();
		form.AddField("Login", mainInfoProfile.login);
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/deleteCellFindEnemy.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит

		yield break;
	}

	public IEnumerator checkNewMove() { //чекает ходы аппонента
		float serverTime = 0;
		while(true) { //пока не нашли соперника
			serverTime += Time.deltaTime;
			if(serverTime >= 1f) { //каждую секунду чекает на новый ход аппонента
				serverTime = 0;

				WWWForm form = new WWWForm();
				form.AddField("Name", tempEnemy.enemy.namePlace); //имя комнаты
				form.AddField("Step", numberOfMove); //номер хода

				UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/checkNewMove.php", form);
				yield return www.SendWebRequest(); //ждет, пока не отправит
				//Debug.Log(www.downloadHandler.text);
				if(www.downloadHandler.text == "NONE") { //если противник еще не передал ход
														 //Debug.Log("Словил NONE");
				} else if(www.downloadHandler.text == "1") { //ход закончен противником
					numberOfMove++;
					endTurn();
					yield break;
				} else { //если получили ход
					templateOfMove tOM = JsonUtility.FromJson<templateOfMove>(www.downloadHandler.text); //структура описания кода
					if(tOM.cardThatUse != "NULL") { //если вывели карту
													//tOM.toAttack - номер клетки, куда вывели
													//tOM.cardThatUse - имя карты, которую вывели на поле
						StartCoroutine(createEnemyCardAtField(tOM.toAttack, tOM.cardThatUse));
					} else if(tOM.isCardMove) { //карта двигалась?
						int number = (14 - tOM.toAttack) - (14 - tOM.whoAttack); //id куда идет - откуда идет
						switch(number) {
							case 5: //идет вверх
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'w';
								break;
							case -1: //идет влево
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'a';
								break;
							case 1: //идет вправо
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'd';
								break;
							case -5: //идет вниз
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 's';
								break;
							case 6: //вверх вправо
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'e';
								break;
							case 4: //вверх влево
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'q';
								break;
							case -4: //вниз вправо
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'c';
								break;
							case -6: //вниз вправо
								cellsGO[14 - tOM.toAttack].GetComponent<memForCells>().idDirectionCell = 'z';
								break;
						}
						GameObject tempCard = cellsGO[14 - tOM.whoAttack].transform.GetChild(0).gameObject; //ссылка на карту соперника, которая сейчас походит
						tempCard.transform.SetParent(objectWarPlace.transform.Find("map"), true); //открепили карту от клетки 
						StartCoroutine(moveCard(cellsGO[14 - tOM.toAttack], tempCard));

					} else if(!tOM.isCardMove && tOM.cardThatUse == "NULL") { //карта стреляла
						tOM.toAttack = 14 - tOM.toAttack;
						tOM.whoAttack = 14 - tOM.whoAttack;
						if(tOM.toAttack != 0) {
							cellsGO[tOM.toAttack].transform.GetChild(0).GetComponent<onTankAtWar>().attackCardFromServer(tOM.whoAttack, tOM.toAttack);
						} else {
							cellsGO[tOM.toAttack].GetComponent<onTankAtWar>().attackCardFromServer(tOM.whoAttack, tOM.toAttack);
						}						
					}
					numberOfMove++;
				}
			}
			if(isBattleFinished) { //если бой кончился
				break;
			}
			yield return null;
		}
		yield break;
	}

	public IEnumerator createEnemyCardAtField(int idCell, String nameOfCard) { //создаёт карту противника на поле
		GameObject me = cellsGO[14-idCell]; //получили клетку, в которую ставим
		GameObject neww = Instantiate(prefOfCardAtWar);
		neww.transform.SetParent(me.transform, false); //сделали дочерним
		unitParam tempUP = new unitParam();
		for(int j = 0; j < parUn.unit.Length; j++) { //нашли всю инфу о данной карте
			if(nameOfCard == parUn.unit[j].name) {
				tempUP = parUn.unit[j];
				break;
			}
		}
		neww.name = tempUP.name;
		for(int i = 0; i < cardsImgLow.Length; i++) { //поменяли картинку
			if(neww.name == cardsImgLow[i].name) {
				neww.GetComponent<Image>().sprite = cardsImgLow[i].img;
				break;
			}
		}
		neww.transform.Find("nameTxt").GetComponent<Text>().text = tempUP.russianName;
		neww.transform.Find("attackImg/Text").GetComponent<Text>().text = tempUP.damage.ToString();
		neww.transform.Find("attackImg/Text").GetComponent<Text>().color = new Color(0.8392157f, 0.2039216f, 0.2627451f); //красный цвет текста, где написан урон
		neww.transform.Find("hpImg/Text").GetComponent<Text>().text = tempUP.hp.ToString();
		neww.transform.Find("attackImg").GetComponent<Image>().sprite = damageIconsImg[0]; //красная иконка урона

		neww.transform.Find("backLight").GetComponent<Image>().enabled = false; //спрятали обводку

		neww.transform.Find("backLight").GetComponent<Image>().sprite = cardFrames[2].img; //сделали красную обводку
		neww.transform.Find("backLight").GetComponent<Image>().material = redEnemyCardMaterial;
		neww.transform.Find("backLight").GetComponent<Image>().color = new Color(1, 0, 0.305809f, 1);
		neww.transform.Find("light").GetComponent<Image>().sprite = okantovkaEnemy; //сделали окантовку противнику

		//neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesAtWarMiddleImg[0]; //выставит значок красного легкого танка

/*		if(tempUP.type == "LT") { //легкий танк
			neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesAtWarMiddleImg[0]; //выставит значок красного легкого танка
			neww.GetComponent<onTankAtWar>().countOfPosibleMove = 1; //это для ЛТ, а при добавлении новых карт придется менять эту строку
		}*/

/*		switch (tempUP.type) {
			case "LT":
				neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesAtWarMiddleImg[0]; //выставит значок красного легкого танка
				neww.GetComponent<onTankAtWar>().countOfPosibleMove = 1; //это для ЛТ, а при добавлении новых карт придется менять эту строку

				break;
			case "MT":
				neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesAtWarMiddleImg[1]; //выставит значок красного легкого танка
				neww.GetComponent<onTankAtWar>().countOfPosibleMove = 0; //это для ЛТ, а при добавлении новых карт придется менять эту строку
				//neww.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили ()
				break;
		}*/

		for(int k = 0; k < flagsImgSmall.Length; k++) {
			if(flagsImgSmall[k].name == tempUP.nation) {
				neww.transform.Find("flagImg").GetComponent<Image>().sprite = flagsImgSmall[k].img; //поменяли спрайт флага

				break;
			}
			if(k == flagsImgSmall.Length - 1) {
				Debug.Log("Не нашёл спрайт флага");
			}
		}

		if(tempUP.plusFuel > 0) {
			neww.transform.Find("plusFuel").gameObject.SetActive(true);
			neww.transform.Find("plusFuel/Text").GetComponent<Text>().text = tempUP.plusFuel.ToString();
		}

		Destroy(objectWarPlace.transform.Find("enemyCards").GetChild(0).gameObject); //убиваем карту в руке соперника

		fuelNowEnemy -= tempUP.needFuel;
		objectWarPlace.transform.Find("map/Button(4,2)/fuelImg/Text").GetComponent<Text>().text = fuelNowEnemy.ToString();

		plusFuelEnemy += tempUP.plusFuel;
		objectWarPlace.transform.Find("map/Button(4,2)/plusFuel/Text").GetComponent<Text>().text = plusFuelEnemy.ToString();

		neww.GetComponent<onTankAtWar>().hp = tempUP.hp;
		neww.GetComponent<onTankAtWar>().damage = tempUP.damage;
		//neww.GetComponent<onTankAtWar>().countOfPosibleMove = 1; //это для ЛТ, а при добавлении новых карт придется менять эту строку
		neww.GetComponent<onTankAtWar>().canAttack = true;
		neww.GetComponent<onTankAtWar>().type = tempUP.type; //тип карты
		neww.GetComponent<onTankAtWar>().plusFuel = tempUP.plusFuel; //прирост ресурсов карты
		neww.GetComponent<onTankAtWar>().idField = 14 - idCell;
		neww.GetComponent<onTankAtWar>().isIFriendly = false; //карта противника

		if(tempUP.type == "LT") {
			neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesAtWarMiddleImg[0]; //выставит значок красного легкого танка
			neww.GetComponent<onTankAtWar>().countOfPosibleMove = 1; //это для ЛТ, а при добавлении новых карт придется менять эту строку
			int freeToMoveCell = freePlaceToMove(14 - idCell, neww.GetComponent<onTankAtWar>().countOfPosibleMove); //число клеток куда можно ходить
			int enemyNear = countAroundYou(14 - idCell, true); //число союзников возле врага
			if(freeToMoveCell == 0 && enemyNear == 0) { //если некуда ходить и стрелять
				neww.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили
			}
		} else if(tempUP.type == "MT") {
			neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesAtWarMiddleImg[1]; //выставит значок красного легкого танка
			neww.GetComponent<onTankAtWar>().countOfPosibleMove = 0; //это для ЛТ, а при добавлении новых карт придется менять эту строку
			int enemyNear = countAroundYou(14 - idCell, true); //число союзников возле врага
			if(enemyNear == 0) { //если некуда ходить и стрелять
				neww.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили
			}
		}

		reckeckPossibilitiesOfCards();

		yield break;
	}

	public IEnumerator aLotOfRequest() { //запросы на сервер, чтобы узнать, нашел ли противника
		WWWForm form = new WWWForm();
		form.AddField("Login", mainInfoProfile.login);

		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/aLotOfRequest.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит

		if(www.error != null || www.isNetworkError || www.isHttpError) {
			Debug.Log("What???");

			canOpenNewWindow = 1;
			//isErrorProccessFind = 2; //произошла ошибка 
			darkPanel.enabled = true;
			StartCoroutine(aphaChangeDark(4)); //вылезет затемнуха с надписью ошибки
			//errorTextUpdate = www.error;

			//повторная попытка отправки запроса
			form = new WWWForm();
			form.AddField("Login", mainInfoProfile.login);
			www = UnityWebRequest.Post("http://thearmynations.ru/main/aLotOfRequest.php", form);
			yield return www.SendWebRequest(); //ждет, пока не отправит

			if(www.error != null || www.isNetworkError || www.isHttpError) {
				errorTextUpdate = www.error;
				isErrorProccessFind = 2; //произошла ошибка 
				yield break;
			}
			isErrorProccessFind = 1; //успешно отправили, но всё еще нужно убрать затемнуху
		}// else {
		//	isErrorProccessFind = -1; //успешно отправили
		//}
		

		if (www.downloadHandler.text != "NON") { //если соперник найден
			//Debug.Log(www.downloadHandler.text);
			tempEnemy = JsonUtility.FromJson<enemyClass>(www.downloadHandler.text); //получил всю инфу о противнике и бое

			if(!tempEnemy.enemy.amaSecond) { //если я нашёл, то нужны еще проверки на enemyReady
				shouldCheckEnemyReady = 0;
				float shetchik = 0;
				float toTwo = 0;
				while(shetchik <= 20f) { //20 секунд ждем согласия противника
					if(toTwo >= 2f && shouldCheckEnemyReady == 0) {
						toTwo = 0;
						shouldCheckEnemyReady = 1; //идет проверка
						StartCoroutine(checkEnemyReady(0)); //чекает и не должен удалять противника
					}
					if(shouldCheckEnemyReady == 2) { //если можно начинать бой
						shetchik = 0;
						break;
					}

					toTwo += Time.deltaTime;
					shetchik += Time.deltaTime;
					yield return null;
				}
				shouldCheckEnemyReady = 0;
				if(shetchik >= 20) {
					yield return checkEnemyReady(1); //чекает и удаляет противника
					isErrorProccessFind = -1; //успешно отправили
					yield break;
				}
			}

			//isEnemyFound = true;
			//isErrorProccessFind = -1; //успешно отправили

			if(tempEnemy.enemy.isAmaFirst == 1) { //говорим мы сейчас ходим или нет
				isAmaTurn = true;
			} else {
				isAmaTurn = false;
			}

			numberOfMove = 0; //выставляем счетчик ходов в ноль
			if(!isAmaTurn) { //если не наш ход
				StartCoroutine(checkNewMove()); //проверяет на новые ходы соперника
			}

			objectFindProccess.transform.Find("flagImgEnemy/nameOfProfile").GetComponent<Text>().text = tempEnemy.enemy.login; //ставим ник игрока в меню поиска соперника

			int tempIdEnemyShtabInfo = 0; //запоминает id информации о шатбе соперника
			for(int w = 0; w < iAAS.iAOS.Length; w++) { //ставим название штаба в меню поиска соперника
				if(iAAS.iAOS[w].name == tempEnemy.enemy.infoEnemy.nameOfShtab) {
					objectFindProccess.transform.Find("flagImgEnemy/nameOfShtab").GetComponent<Text>().text = iAAS.iAOS[w].info0;

					for(int b = 0; b < flagsFinders.Length; b++) { //настраиваем флаг позади аватара
						if(flagsFinders[b].name == iAAS.iAOS[w].nation) {
							objectFindProccess.transform.Find("flagImgEnemy").GetComponent<Image>().sprite = flagsFinders[b].img;
							tempIdEnemyShtabInfo = w;

							break;
						}
						if(b == spritesAvatars.Length - 1) {
							Debug.Log("Не нашёл флаг");
						}
					}

					break;
				}
				if(w == iAAS.iAOS.Length - 1) {
					Debug.Log("Не нашел название штаба");
				}
			}

			for(int w = 0; w < spritesAvatars.Length; w++) { //ищем и ставим аватар
				if(spritesAvatars[w].name == tempEnemy.enemy.infoEnemy.nameOfShtab) {
					objectFindProccess.transform.Find("flagImgEnemy/faceId").GetComponent<Image>().sprite = spritesAvatars[w].img;

					break;
				}
				if(w == spritesAvatars.Length - 1) {
					Debug.Log("Не нашёл аватар");
				}
			}

			//objectWarPlace.transform.Find("map/Button(0,0)/nameProfile").GetComponent<Text>().text = mainInfoProfile.login; //имя профиля игрока
			//objectWarPlace.transform.Find("map/Button(0,0)/cardsImg/atAll").GetComponent<Text>().text = numOfCard.ToString();

			cellsGO[0].transform.Find("nameProfile").GetComponent<Text>().text = mainInfoProfile.login; //имя профиля игрока
			cellsGO[0].transform.Find("cardsImg/atAll").GetComponent<Text>().text = numOfCard.ToString(); //наше число карт
			for(int w = 0; w<iAAS.iAOS.Length; w++) {
				if(iAAS.iAOS[w].name == infoAboutShtab.shtabs[numberShtabNow].name) {
					cellsGO[0].GetComponent<onTankAtWar>().hp = iAAS.iAOS[w].hp; //число хп в скрипт ставим
					cellsGO[0].transform.Find("shtabImg/hpImg/Text").GetComponent<Text>().text = iAAS.iAOS[w].hp.ToString(); //отображаем хп
					cellsGO[0].GetComponent<onTankAtWar>().damage = iAAS.iAOS[w].damage; //число урона в скрипт ставим
					cellsGO[0].transform.Find("shtabImg/attackImg/Text").GetComponent<Text>().text = iAAS.iAOS[w].damage.ToString(); //отображаем урон
					plusFuel = iAAS.iAOS[w].plusFuel; //поменяли прирост топляка
					cellsGO[0].transform.Find("plusFuel/Text").GetComponent<Text>().text = plusFuel.ToString(); //отобразили прирост топлива
					fuelNow = plusFuel; //поменяли число топлива
					cellsGO[0].transform.Find("fuelImg/Text").GetComponent<Text>().text = fuelNow.ToString(); //отобразили число топлива
					for(int k = 0; k<shtabsImg.Length; k++) {
						if(shtabsImg[k].name == infoAboutShtab.shtabs[numberShtabNow].name) {
							cellsGO[0].transform.Find("shtabImg").GetComponent<Image>().sprite = shtabsImg[k].img; //поменяли картинку штаба

							break;
						}
						if(k == shtabsImg.Length-1) {
							Debug.Log("Не нашел картинку штаба");
						}
					}
					for(int k = 0; k<flagsImgSmall.Length; k++) {
						if(flagsImgSmall[k].name == iAAS.iAOS[w].nation) {
							cellsGO[0].transform.Find("shtabImg/flagImg").GetComponent<Image>().sprite = flagsImgSmall[k].img; //поменяли спрайт флага

							break;
						}
						if(k == flagsImgSmall.Length-1) {
							Debug.Log("Не нашёл спрайт флага");
						}
					}
					cellsGO[0].transform.Find("shtabImg/nameTxt").GetComponent<Text>().text = iAAS.iAOS[w].info0; //поставили название штаба
					cellsGO[0].transform.Find("shtabImg/plusFuel/Text").GetComponent<Text>().text = iAAS.iAOS[w].plusFuel.ToString(); //отобразили прирост ресов на самой карте штаба

					break;
				}
				if(w == iAAS.iAOS.Length-1) {
					Debug.Log("Не нашёл инфо о штабе");
				}
			}

			//objectWarPlace.transform.Find("map/Button(4,2)/nameProfile").GetComponent<Text>().text = tempEnemy.enemy.login; //имя профиля противника
			//objectWarPlace.transform.Find("map/Button(4,2)/cardsImg/atAll").GetComponent<Text>().text = tempEnemy.enemy.infoEnemy.countOfCard.ToString();
			cellsGO[14].transform.Find("nameProfile").GetComponent<Text>().text = tempEnemy.enemy.login; //имя профиля противника
			cellsGO[14].transform.Find("cardsImg/atAll").GetComponent<Text>().text = tempEnemy.enemy.infoEnemy.countOfCard.ToString(); //число карт соперника
			cellsGO[14].GetComponent<onTankAtWar>().hp = iAAS.iAOS[tempIdEnemyShtabInfo].hp; //число хп в скрипт ставим
			cellsGO[14].transform.Find("shtabImg/hpImg/Text").GetComponent<Text>().text = iAAS.iAOS[tempIdEnemyShtabInfo].hp.ToString(); //отображаем хп
			cellsGO[14].GetComponent<onTankAtWar>().damage = iAAS.iAOS[tempIdEnemyShtabInfo].damage; //число урона в скрипт ставим
			cellsGO[14].transform.Find("shtabImg/attackImg/Text").GetComponent<Text>().text = iAAS.iAOS[tempIdEnemyShtabInfo].damage.ToString(); //отображаем урон
			plusFuelEnemy = iAAS.iAOS[tempIdEnemyShtabInfo].plusFuel; //поменяли прирост топляка
			cellsGO[14].transform.Find("plusFuel/Text").GetComponent<Text>().text = plusFuelEnemy.ToString(); //отобразили прирост топлива
			fuelNowEnemy = plusFuelEnemy; //поменяли число топлива
			cellsGO[14].transform.Find("fuelImg/Text").GetComponent<Text>().text = fuelNowEnemy.ToString(); //отобразили число топлива
			for(int k = 0; k < shtabsImg.Length; k++) {
				if(shtabsImg[k].name == tempEnemy.enemy.infoEnemy.nameOfShtab) {
					cellsGO[14].transform.Find("shtabImg").GetComponent<Image>().sprite = shtabsImg[k].img; //поменяли картинку штаба

					break;
				}
				if(k == shtabsImg.Length - 1) {
					Debug.Log("Не нашел картинку штаба");
				}
			}
			for(int k = 0; k < flagsImgSmall.Length; k++) {
				if(flagsImgSmall[k].name == iAAS.iAOS[tempIdEnemyShtabInfo].nation) {
					cellsGO[14].transform.Find("shtabImg/flagImg").GetComponent<Image>().sprite = flagsImgSmall[k].img; //поменяли спрайт флага

					break;
				}
				if(k == flagsImgSmall.Length - 1) {
					Debug.Log("tempIdEnemyShtabInfo = " + tempIdEnemyShtabInfo.ToString());
					Debug.Log("iAAS.iAOS[tempIdEnemyShtabInfo].nation = " + iAAS.iAOS[tempIdEnemyShtabInfo].nation);
					Debug.Log("Не нашёл спрайт флага");
				}
			}
			cellsGO[14].transform.Find("shtabImg/nameTxt").GetComponent<Text>().text = iAAS.iAOS[tempIdEnemyShtabInfo].info0; //поставили название штаба
			cellsGO[14].transform.Find("shtabImg/plusFuel/Text").GetComponent<Text>().text = iAAS.iAOS[tempIdEnemyShtabInfo].plusFuel.ToString(); //отобразили прирост ресов на самой карте штаба

			for (int i = 0; i < infoAboutShtab.shtabs[numberShtabNow].cards.Count; i++) { //добавление свободных карт для добавления рандомных на поле боя
																						  //freeCardsAtWar.Add(
				freeCard newC = new freeCard();
				newC.name = infoAboutShtab.shtabs[numberShtabNow].cards[i].name;
				newC.count = infoAboutShtab.shtabs[numberShtabNow].cards[i].count;
				freeCardsAtWar.Add(newC);
			}

			isEnemyFound = true;
		}
		isErrorProccessFind = -1; //успешно отправили

								  //Debug.Log("Я должен быть раньше");
		yield break;
	}

	public IEnumerator checkEnemyReady(int shouldDelete) { //запросы на сервер, чтобы узнать, готов ли противник
		WWWForm form = new WWWForm();
		form.AddField("Login", mainInfoProfile.login);
		form.AddField("Delete", shouldDelete);
		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/checkEnemy.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит

		if(www.downloadHandler.text == "go") {
			shouldCheckEnemyReady = 2; //можно начинать бой
		} else if(www.downloadHandler.text == "wait") {
			shouldCheckEnemyReady = 0; //дальше чекаем
		}

		yield break;
	}

	public void pointerOn(GameObject me) { //работает при наведении на карту в руке в режиме боя

		objectWarPlace.transform.Find("myCards").GetComponent<HorizontalLayoutGroup>().enabled = false; //отключает сортировку карт в руке
		//если на карту не нажимали и на неё можно жать и я сейчас хожу
		if (me.GetComponent<rememberPos>().upOrDown != 2 && canITouchCardAtWar == true && isAmaTurn) {
			me.GetComponent<rememberPos>().upOrDown = 1;
			Cursor.SetCursor(cursors[1], Vector2.zero, CursorMode.Auto); //ставлю вместо курсора пальчик
			StartCoroutine(moveCardAtWarPointer(me));
		}
	}

	public void pointerExit(GameObject me) { //работает при уберании мыши с карты в руке									
		if (me.GetComponent<rememberPos>().upOrDown != 2 && canITouchCardAtWar == true) {
			me.GetComponent<rememberPos>().upOrDown = 0;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //ставлю стандартный курсор
			StartCoroutine(moveCardAtWarPointer(me));
		}
	}

	public void pointerClick(GameObject me) { //нажатие на карту (в руке)
		if (canITouchCardAtWar == true && fuelNow >= me.GetComponent<rememberPos>().uP.needFuel && isAmaTurn) { //первое нажатие (выбор карты)
/*			for(int i = 0; i<cellsGO.Length; i++) {
				if(me.name == cellsGO[i].name) {
					me.GetComponent<rememberPos>().myPosAtHieararchy = i; //запоминает id в иерархии
					break;
				}
				if(i == cellsGO.Length-1) {
					Debug.Log("Не бывает такого");
				}
			}*/
			me.GetComponent<rememberPos>().myPosAtHieararchy = me.transform.GetSiblingIndex(); //запоминает id в иерархии
			me.transform.SetAsLastSibling(); //ставит карточку вниз иерархии, чтобы она выводилась поверх всех

			me.GetComponent<rememberPos>().upOrDown = 2;
			StartCoroutine(moveCardAtWarPointer(me)); //плавное движение
			canITouchCardAtWar = false;
			//me.transform.Find("backLight").GetComponent<Image>().sprite = backLightsForCardAtWar[1];

			me.transform.Find("backLight").gameObject.SetActive(false); //отключил зеленый свет
			me.transform.Find("backLightYellow").gameObject.SetActive(true); //включил желтый свет

			isOutputCard = true; //флаг утвержадющий факт того, что мы выводим карту
			selectedCardAtWar = me; //запомнили объект, на который нажали
			for (int i = 0; i<3; i++) { //подсветит ячейки, на которые можно вывести карту
				//gButton[i].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
				if(gButton[i].transform.childCount == 0) { //если в клетке не стоит карта
					GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
					neww.transform.SetParent(gButton[i].transform, false);
					neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
					Image im = neww.GetComponent<Image>();
					im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
				}
			}
		} else if (canITouchCardAtWar == false && me.GetComponent<rememberPos>().upOrDown == 2) { //второе нажатие (сброс выбора карты)
			me.transform.SetSiblingIndex(me.GetComponent<rememberPos>().myPosAtHieararchy); //ставит карточку на своё место в иерархии

			me.GetComponent<rememberPos>().upOrDown = 0;
			canITouchCardAtWar = true;

			//me.transform.Find("backLight").GetComponent<Image>().sprite = backLightsForCardAtWar[0];
			me.transform.Find("backLight").gameObject.SetActive(true); //отключил зеленый свет
			me.transform.Find("backLightYellow").gameObject.SetActive(false); //включил желтый свет

			isOutputCard = false; //флаг утвержадющий факт того, что мы не выводим карту
			selectedCardAtWar = null;
			for (int i = 0; i<3; i++) { //потушит подсветку ячеек на которые можно вывести карту
				//gButton[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				if(gButton[i].transform.GetChild(0).name == "BACKLIGHT") { //мы уверены, что дочерний объект есть и ищем нашу подсетку
					Destroy(gButton[i].transform.GetChild(0).gameObject); //тушим подсветку
				}
			}
			pointerExit(me); 
		}

	}

	public IEnumerator moveCard(GameObject goCell, GameObject cardThatMove) { //плавное движение карты в бою при перемещении в клетку goCell
		cardThatMove.GetComponent<onTankAtWar>().isAmaMove = true; //карта двигается
		float curTime = 0, timeOfTravel = 0.5f, normalizedValue;
		Vector3 startPosition = cardThatMove.GetComponent<RectTransform>().anchoredPosition;
		Vector3 finishPosition = new Vector3(); //чтобы компилятор не ругался (в switch оно якобы может не изменится)

		GameObject neww = Instantiate(prefOfArrow); //создали стрелку
		neww.transform.SetParent(cardThatMove.transform, false); //засунули под нашу карту
		neww.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //ставим в самый центр относительно центра карты
		if(cardThatMove.GetComponent<onTankAtWar>().isIFriendly == false) { //если это карта противника
			//делаем стрелку красной
			neww.transform.Find("2/1").GetComponent<Image>().sprite = twoSpritesForArrow[0];
			neww.transform.Find("2").GetComponent<Image>().sprite = twoSpritesForArrow[1];
		}

		Vector3 startPositionArr = new Vector3();
		Vector3 finishPositionArr = new Vector3();

		switch(goCell.GetComponent<memForCells>().idDirectionCell) {
			case 'w':
				finishPosition = new Vector3(startPosition.x, startPosition.y + 90, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -41.5f);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,90);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x, startPositionArr.y + 70, startPositionArr.z);
				break;
			case 'a':
				finishPosition = new Vector3(startPosition.x - 90, startPosition.y, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(41.5f, 0);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x - 70, startPositionArr.y, startPositionArr.z);
				break;
			case 'd':
				finishPosition = new Vector3(startPosition.x + 90, startPosition.y, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(-41.5f, 0);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x + 70, startPositionArr.y, startPositionArr.z);
				break;
			case 's':
				finishPosition = new Vector3(startPosition.x, startPosition.y - 90, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 41.5f);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 270);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x, startPositionArr.y - 70f, startPositionArr.z);
				break;
			case 'e':
				finishPosition = new Vector3(startPosition.x + 90, startPosition.y + 90, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(-41.5f, -41.5f);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 45);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x + 70, startPositionArr.y + 70, startPositionArr.z);
				break;
			case 'q':
				finishPosition = new Vector3(startPosition.x - 90, startPosition.y + 90, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(41.5f, -41.5f);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 135);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x - 70, startPositionArr.y + 70, startPositionArr.z);
				break;
			case 'c':
				finishPosition = new Vector3(startPosition.x + 90, startPosition.y - 90, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(-41.5f, 41.5f);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 315);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x + 70, startPositionArr.y - 70, startPositionArr.z);
				break;
			case 'z':
				finishPosition = new Vector3(startPosition.x - 90, startPosition.y - 90, startPosition.z);
				neww.GetComponent<RectTransform>().anchoredPosition = new Vector2(41.5f, 41.5f);
				neww.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 225);

				neww.transform.SetParent(objectWarPlace.transform.Find("map"), true);
				startPositionArr = neww.GetComponent<RectTransform>().anchoredPosition;
				finishPositionArr = new Vector3(startPositionArr.x - 70, startPositionArr.y - 70, startPositionArr.z);
				break;
		}

		char temp = goCell.GetComponent<memForCells>().idDirectionCell;
		if(temp == 'w' || temp == 'a' || temp == 's' || temp == 'd') { //одна единица ходов минус
			cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove -= 1;
			if(cardThatMove.GetComponent<onTankAtWar>().type == "MT") { //если ходил средний танк
				cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove -= 1;
			}
		} else { //две единицы ходов минус
			cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove -= 2;
		}

		neww.transform.SetAsFirstSibling(); //кидаем в начало иерархии, чтобы стрелка отображалась под картой

		neww.GetComponent<CanvasGroup>().alpha = 1; //включил отображение стрелки
		float curTimeArr = 0, timeOfTravelArr = 0.5f, normalizedValueArr;
		while(curTimeArr < timeOfTravelArr) {
			curTimeArr += Time.deltaTime;
			normalizedValueArr = curTimeArr / timeOfTravelArr; // we normalize our time

			//двигаем стрелку
			neww.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPositionArr, finishPositionArr, normalizedValueArr);

			yield return null;
		}

		while(curTime < timeOfTravel) {
			curTime += Time.deltaTime;
			normalizedValue = curTime / timeOfTravel; // we normalize our time

			//двигаем карту
			cardThatMove.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, finishPosition, normalizedValue);
			neww.GetComponent<CanvasGroup>().alpha = 1 - normalizedValue;

			yield return null;
		}

		Destroy(neww);


		cardThatMove.transform.SetParent(goCell.transform, false); //положили карту под клетку
		cardThatMove.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		for(int i = 0; i < cellsGO.Length; i++) {
			if(cellsGO[i].name == goCell.name) { //находим нашу клетку
				cardThatMove.GetComponent<onTankAtWar>().idField = i;
				break;
			}
			if(i == cellsGO.Length-1) {
				Debug.Log("Хуета");
			}
		}
		cardThatMove.GetComponent<onTankAtWar>().isAmaMove = false; //карта уже не двигается

/*		
		//Debug.Break();
		Debug.Log("NumberOfCell = " + cardThatMove.transform.parent.GetSiblingIndex());
		Debug.Log("around you = " + countAroundYou(cardThatMove.transform.parent.GetSiblingIndex(), !cardThatMove.GetComponent<onTankAtWar>().isIFriendly).ToString());*/

		int idWhereCard = 0;
		for(int i = 0; i<cellsGO.Length; i++) {
			if(cellsGO[i].name == cardThatMove.transform.parent.name) {
				idWhereCard = i;
				break;
			}
			if(i == cellsGO.Length-1) {
				Debug.Log("Не бывает такого");
			}
		}

		int countAround = countAroundYou(idWhereCard, !cardThatMove.GetComponent<onTankAtWar>().isIFriendly); //считает число врагов рядом с этой картой
		int freeCellAround = freePlaceToMove(idWhereCard, cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove); //число свободных клеток, куда можно походить
		if((countAround == 0 || !cardThatMove.GetComponent<onTankAtWar>().canAttack) && (cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove == 0 || freeCellAround == 0)) { //карта не может не стрелять, не ходить
			//Debug.Log("Карта якобы в этом id = " + idWhereCard.ToString());
			cardThatMove.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
		}

/*		if(!cardThatMove.GetComponent<onTankAtWar>().canAttack && cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove == 0) { //если не может стрелять и двигаться
			cardThatMove.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
		} else if(cardThatMove.GetComponent<onTankAtWar>().countOfPosibleMove == 0 && countAroundYou(idWhereCard, !cardThatMove.GetComponent<onTankAtWar>().isIFriendly) == 0) { //если не можешь двигаться и стрелять не в кого
			cardThatMove.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
		}*/

		reckeckPossibilitiesOfCards();

		yield break;
	}

	public void clickOnCell(GameObject me) { //клик на клетку на карте(чтобы вывести на поле) или (чтобы переместить карту)
		if(!isAmaTurn) { //если не мой ход
			return;
		}

		if(isMoveOrAttackCard && selectedCardMoveOrAttack.GetComponent<onTankAtWar>().countOfPosibleMove > 0) { //если мы уже выбрали карту на поле и сейчас кликнули на клетку, чтобы походить
			if (me.transform.childCount > 0 && me.transform.GetChild(0).name == "BACKLIGHT") {
			//if(me.GetComponent<Image>().color.r == 0.05490184f) { //если клетка выделена зеленым
				Destroy(me.transform.GetChild(0).gameObject); //удаляет подсветку

				selectedCardMoveOrAttack.transform.SetParent(objectWarPlace.transform.Find("map"), true); //открепили карту от клетки

				StartCoroutine(moveCard(me, selectedCardMoveOrAttack)); //плавно перемещаем карту на клетку me

/*				if(me.GetComponent<memForCells>().idDirectionCell == 'w' || me.GetComponent<memForCells>().idDirectionCell == 'a' || me.GetComponent<memForCells>().idDirectionCell == 's' || me.GetComponent<memForCells>().idDirectionCell == 'd') {
					selectedCardMoveOrAttack.GetComponent<onTankAtWar>().countOfPosibleMove--; //минус одно очко передвижения
				} else {
					selectedCardMoveOrAttack.GetComponent<onTankAtWar>().countOfPosibleMove -= 2; //минус два очка передвижения
				}*/
				GameObject tempCard = selectedCardMoveOrAttack; //запоминаем ссылку, так как след строка удалит ссылку из selectedCardMoveOrAttack

				int tempIdField = 0;
				//сохраняем ход на сервер
				for(int i = 0; i < cellsGO.Length; i++) {
					if(cellsGO[i].name == me.name) { //находим нашу клетку
						StartCoroutine(sendInfoAboutMove(tempCard.GetComponent<onTankAtWar>().idField, i, "NULL", true, false));
						tempIdField = i;
						break;
					}
				}

				//tempCard.GetComponent<onTankAtWar>().idField = me.transform.GetSiblingIndex(); //меняем id поля на котором карта стоит
				//selectedCardMoveOrAttack.GetComponent<onTankAtWar>().clickOnTankToGo(); //тушим свет
				//Debug.Log(me.transform.GetSiblingIndex().ToString());

				selectedCardMoveOrAttack.GetComponent<onTankAtWar>().cancelSelection(selectedCardMoveOrAttack, tempCard.GetComponent<onTankAtWar>().idField, tempIdField);



				//reckeckPossibilitiesOfCards(); - не надо, так как в moveCard эта штука и так вызывается
			}
		}

		if (isOutputCard && me.transform.childCount > 0 && me.transform.GetChild(0).name == "BACKLIGHT") { //если выбрали карту и сейчас будем ставить (вывод карты на поле)
			for(int i = 0; i<gButton.Length; i++) { //удаляет все подсветки
				if(gButton[i].transform.childCount > 0 && gButton[i].transform.GetChild(0).name == "BACKLIGHT") {
					Destroy(gButton[i].transform.GetChild(0).gameObject);
				}
			}
			//Destroy(me.transform.GetChild(0).gameObject); //удаляет подсветку

			GameObject neww = Instantiate(prefOfCardAtWar);
			neww.transform.SetParent(me.transform, false);
			neww.transform.SetAsFirstSibling();
			unitParam tempUP = selectedCardAtWar.GetComponent<rememberPos>().uP;
			neww.name = tempUP.name;
			for (int i = 0; i<cardsImgLow.Length; i++) {
				if (neww.name == cardsImgLow[i].name) {
					neww.GetComponent<Image>().sprite = cardsImgLow[i].img;
					break;
				}
			}
			neww.transform.Find("nameTxt").GetComponent<Text>().text = tempUP.russianName;
			neww.transform.Find("attackImg/Text").GetComponent<Text>().text = tempUP.damage.ToString();
			neww.transform.Find("hpImg/Text").GetComponent<Text>().text = tempUP.hp.ToString();
			if (selectedCardAtWar.GetComponent<rememberPos>().uP.plusFuel > 0) {
				neww.transform.Find("plusFuel").gameObject.SetActive(true);
				neww.transform.Find("plusFuel/Text").GetComponent<Text>().text = tempUP.plusFuel.ToString();
			}
			Destroy(selectedCardAtWar); //убиваем карту в руке
			isOutputCard = false; //карту не выводим
			canITouchCardAtWar = true; //карты в руке снова можно выбирать
			objectWarPlace.transform.Find("myCards").GetComponent<HorizontalLayoutGroup>().enabled = true; //включает сортировку карт в руке
			//numOfCardAtGarbage++;
			//objectWarPlace.transform.Find("map/Button(0,0)/cardsImg/trashCard").GetComponent<Text>().text = numOfCardAtGarbage.ToString();
			fuelNow -= tempUP.needFuel;
			objectWarPlace.transform.Find("map/Button(0,0)/fuelImg/Text").GetComponent<Text>().text = fuelNow.ToString();
			//for (int i = 0; i<3; i++) { //потушит подсветку ячеек на которые можно вывести карту
			//	gButton[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
			//}
			plusFuel += tempUP.plusFuel;
			objectWarPlace.transform.Find("map/Button(0,0)/plusFuel/Text").GetComponent<Text>().text = plusFuel.ToString();
			neww.GetComponent<onTankAtWar>().hp = tempUP.hp;
			neww.GetComponent<onTankAtWar>().damage = tempUP.damage;
			//neww.GetComponent<onTankAtWar>().countOfPosibleMove = 1; //это для ЛТ, а при добавлении новых карт придется менять эту строку
			neww.GetComponent<onTankAtWar>().canAttack = true;
			neww.GetComponent<onTankAtWar>().type = tempUP.type; //тип карты
			neww.GetComponent<onTankAtWar>().plusFuel = tempUP.plusFuel; //даёт топлива

			//neww.GetComponent<onTankAtWar>().idField = me.transform.GetSiblingIndex();
			for(int i = 0; i<cellsGO.Length; i++) {
				if(cellsGO[i].name == me.name) { //нашли нашу клетку (её индекс)
					neww.GetComponent<onTankAtWar>().idField = i;
					break;
				}
			}
			neww.GetComponent<onTankAtWar>().isIFriendly = true; //наша карта

			for(int i = 0; i<typesImg.Length; i++) {
				if(typesImg[i].name == tempUP.type) {
					neww.transform.Find("typeImg").GetComponent<Image>().sprite = typesImg[i].img; //поменяли тип танка
					break;
				}
				if(i == typesImg.Length-1) {
					Debug.Log("Не нашёл тип карты");
				}
			}

			for(int i = 0; i<flagsImg.Length; i++) {
				if(flagsImg[i].name == tempUP.nation) {
					neww.transform.Find("flagImg").GetComponent<Image>().sprite = flagsImg[i].img; //поменяли флаг карты
					break;
				}
				if(i == flagsImg.Length - 1) {
					Debug.Log("Не нашёл флаг для карты");
				}
			}

			if(tempUP.type == "LT") {
				neww.GetComponent<onTankAtWar>().countOfPosibleMove = 1; //это для ЛТ, а при добавлении новых карт придется менять эту строку
				int freeToMoveCell = freePlaceToMove(neww.GetComponent<onTankAtWar>().idField, neww.GetComponent<onTankAtWar>().countOfPosibleMove); //число клеток куда можно ходить
				int enemyNear = countAroundYou(neww.GetComponent<onTankAtWar>().idField, false); //число врагов рядом
				if(freeToMoveCell == 0 && enemyNear == 0) { //если некуда ходить и стрелять
					neww.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили
				}
			} else if(tempUP.type == "MT") {
				neww.GetComponent<onTankAtWar>().countOfPosibleMove = 0; //средний танк не может сразу ходить
				int enemyNear = countAroundYou(neww.GetComponent<onTankAtWar>().idField, false); //число врагов рядом
				if(enemyNear == 0) { //если стрелять некуда (а ходить он и так не может)
					neww.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили
				}
			}

			StartCoroutine(sendInfoAboutMove(-1, neww.GetComponent<onTankAtWar>().idField, neww.name, false, false)); //отправляет инфу о ходе на сервер

			reCheckAllCardAtWar(); //см описание функции(подсветка карт, которые можно вывести на поле)

			//reckeckPossibilitiesOfCards();

			StartCoroutine(waitAFrame());
		}
	}

	private IEnumerator waitAFrame() {
		yield return null;

		reckeckPossibilitiesOfCards();

		yield break;
	}

	public void sendInfoAboutMoveNoCoroutine(int whoAttack, int toAttack, string cardThatUse, bool isCardMove, bool isShoulEnd) { //отправляет инфу о ходе на сервер
		StartCoroutine(sendInfoAboutMove(whoAttack, toAttack, cardThatUse, isCardMove, isShoulEnd));
	}

	public IEnumerator sendInfoAboutMove(int whoAttack, int toAttack, string cardThatUse, bool isCardMove, bool isShoulEnd) { //отправляет инфу о ходе на сервер
		WWWForm form = new WWWForm();
		form.AddField("Name", tempEnemy.enemy.namePlace); //название комнаты битвы
		form.AddField("Step", numberOfMove); //номер хода
		form.AddField("Login", mainInfoProfile.login); //имя профиля игрока
													   //form.AddField("History", "");
		if(isShoulEnd) {
			form.AddField("EndFlag", 1);
		} else {
			form.AddField("EndFlag", 0);
		}

		templateOfMove tOM = new templateOfMove();
		tOM.whoAttack = whoAttack;
		tOM.toAttack = toAttack;
		tOM.isCardMove = isCardMove;
		tOM.cardThatUse = cardThatUse;

		form.AddField("History", JsonUtility.ToJson(tOM));

		UnityWebRequest www = UnityWebRequest.Post("http://thearmynations.ru/main/sendInfoAboutMove.php", form);
		yield return www.SendWebRequest(); //ждет, пока не отправит

		numberOfMove++; //индекс хода двигаем


		yield break;
	}

	public void sellectEnemyCardForHQ() { //когда карты выделяет штаб
		for(int i = 1; i<cellsGO.Length; i++) {
			if(i == 1 || i == 5 || i == 6) {
				if(cellsGO[i].transform.childCount != 0 && cellsGO[i].transform.GetChild(0).name != "BACKLIGHT") {
					if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[i].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = true;
					}
				}
			} else if(i > 1 && i < 14) { //все клетки, кроме 0, 1, 5, 6, 14 
				if(cellsGO[i].transform.childCount != 0 && cellsGO[i].transform.GetChild(0).name != "BACKLIGHT") {
					if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						if(checkAroundYou(i)) { //если рядом с противником есть союзная карта
							cellsGO[i].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = true;
						}
					}
				}
			} else if(i == 14) {
				cellsGO[i].transform.Find("shtabImg").transform.Find("backLight").GetComponent<Image>().enabled = true;
			}
		}
	}

	public void unSellectCardForHQ() { //штаб убирает выделения
		for(int i = 1; i < cellsGO.Length; i++) {
			if(i != 14) { //если это не вражеский штаб
				if(cellsGO[i].transform.childCount != 0 && cellsGO[i].transform.GetChild(0).name != "BACKLIGHT") {
					if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[i].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = false;
					}
				}
			} else { //если враж штаб
				cellsGO[i].transform.Find("shtabImg").transform.Find("backLight").GetComponent<Image>().enabled = false;
			}
		}
	}

	public int freePlaceToMove(int id, int possibleMoves) { //считает число клеток рядом, на которые можно походить
		int countAround = 0;

		//Debug.Log(possibleMoves);

		if(possibleMoves == 0) {
			return countAround;
		}

		//Debug.Log(cellsGO[id + 5].transform.childCount);
		if(id < 9) { //сверху
			if(cellsGO[id + 5].transform.childCount == 0) {
				countAround++;
				//Debug.Log("Yep");
			}
		}
		if(id != 1 && id != 5 && id != 10) { //лево
			if(cellsGO[id - 1].transform.childCount == 0) {
				countAround++;
			}
		}
		if(id != 4 && id != 9 && id < 13) { //право
			if(cellsGO[id + 1].transform.childCount == 0) {
				countAround++;
			}
		}
		if(id > 5) { //снизу
			if(id - 5 != 0 && cellsGO[id - 5].transform.childCount == 0) {
				countAround++;
			}
		}

		if(possibleMoves < 2) {
			return countAround;
		}

		if(id != 4 && id < 8) { //вверх вправо
			if(cellsGO[id + 6].transform.childCount == 0) {
				countAround++;
			}
		}
		if(id != 5 && id < 10 && id != 0) { //вверх влево
			if(cellsGO[id + 4].transform.childCount == 0) {
					countAround++;
			}
		}
		if(id != 10 && id > 6) { //вниз влево
			if(id - 6 != 0 && cellsGO[id - 6].transform.childCount == 0) {
				countAround++;
			}
		}
		if(id != 9 && id > 4 && id != 14) { //вниз вправо
			if(cellsGO[id - 4].transform.childCount == 0) {
				countAround++;
			}
		}

		return countAround;
	}

	public int countAroundYou(int id, bool isItFriendly) { //чекает наличие враждебных карт возле союзной (id - номер клетки, где союзная карта)
		int countAround = 0;
		if(id <= 9) { //сверху
			if(id + 5 == 14 && !isItFriendly) { //если вражеский штаб
				countAround++;
			} else if(id != 9 && cellsGO[id + 5].transform.childCount != 0 && cellsGO[id + 5].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 5].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
					//Debug.Log("Хули");
				}
			}
		}
		if(id != 5 && id != 10) { //лево
			if(id - 1 == 0 && isItFriendly) { //союзный штаб
				countAround++;
			} else if(id-1 != 0 && cellsGO[id - 1].transform.childCount != 0 && cellsGO[id - 1].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 1].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
				}
			}
		}
		if(id != 4 && id != 9 && id <= 13) { //право
			if(id + 1 == 14 && !isItFriendly) { //если вражеский штаб
				countAround++;
			} else if(id != 13 && cellsGO[id + 1].transform.childCount != 0 && cellsGO[id + 1].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 1].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
				}
			}
		}
		if(id >= 5) { //снизу
			if(id-5 == 0 && isItFriendly) {
				countAround++;
			} else if(id-5 != 0 && cellsGO[id - 5].transform.childCount != 0 && cellsGO[id - 5].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 5].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
				}
			}
		}
		if(id != 4 && id <= 8) { //вверх вправо
			if(id + 6 == 14 && !isItFriendly) { //если вражеский штаб
				countAround++;
			} else if(id != 8 && cellsGO[id + 6].transform.childCount != 0 && cellsGO[id + 6].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				//Debug.Log("I checked " + (id+6).ToString());
				//Debug.Log("isItFriendly = " + isItFriendly.ToString());
				if(cellsGO[id + 6].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
				}
			}
		}
		if(id != 5 && id < 10 && id != 0) { //вверх влево
			if(cellsGO[id + 4].transform.childCount != 0 && cellsGO[id + 4].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 4].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
					//Debug.Log("Хули");
				}
			}
		}
		if(id != 10 && id >= 6) { //вниз влево
			if(id-6 == 0 && isItFriendly) {
				countAround++;
			} else if(id-6 != 0 && cellsGO[id - 6].transform.childCount != 0 && cellsGO[id - 6].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 6].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
				}
			}
		}
		if(id != 9 && id > 4 && id != 14) { //вниз вправо
			if(cellsGO[id - 4].transform.childCount != 0 && cellsGO[id - 4].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 4].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == isItFriendly) { //если там вражеская карта
					countAround++;
				}
			}
		}

		return countAround;
	}

	public bool checkAroundYou(int id) { //чекает наличие союзных карт возле вражеской (id - номер клетки, где вражеская карта)
		if(id < 9) { //сверху
			if(cellsGO[id + 5].transform.childCount != 0 && cellsGO[id + 5].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 5].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id != 1 && id != 5 && id != 10) { //лево
			if(cellsGO[id - 1].transform.childCount != 0 && cellsGO[id - 1].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 1].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id != 4 && id != 9 && id < 13) { //право
			if(cellsGO[id + 1].transform.childCount != 0 && cellsGO[id + 1].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 1].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id > 5) { //снизу
			if(cellsGO[id - 5].transform.childCount != 0 && cellsGO[id - 5].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 5].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id != 4 && id < 8) { //вверх вправо
			if(cellsGO[id + 6].transform.childCount != 0 && cellsGO[id + 6].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 6].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id != 5 && id < 10 && id != 0) { //вверх влево
			if(cellsGO[id + 4].transform.childCount != 0 && cellsGO[id + 4].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id + 4].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id != 10 && id > 6) { //вниз влево
			if(cellsGO[id - 6].transform.childCount != 0 && cellsGO[id - 6].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 6].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}
		if(id != 9 && id > 4 && id != 14) { //вниз вправо
			if(cellsGO[id - 4].transform.childCount != 0 && cellsGO[id - 4].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
				if(cellsGO[id - 4].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == true) { //если там наша карта
					return true;
				}
			}
		}

		return false;
	}

	//возвращает число противников рядом с картой
	public int selectEnemyCard(int indexOfCellFrom, bool isLight) { //выделяет карты, которые можно атаковать вокруг клетки с индексом indexOfCellFrom
		//if(shouldReset == false) {
		int countOfEnemyNear = 0;

			if(indexOfCellFrom < 9) { //сверху
				if(cellsGO[indexOfCellFrom + 5].transform.childCount != 0 && cellsGO[indexOfCellFrom + 5].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom + 5].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom + 5].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom + 5].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'w';
					countOfEnemyNear++;
					}
				}
			}
			if(indexOfCellFrom != 1 && indexOfCellFrom != 5 && indexOfCellFrom != 10) { //лево
				if(cellsGO[indexOfCellFrom - 1].transform.childCount != 0 && cellsGO[indexOfCellFrom - 1].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom - 1].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom - 1].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom - 1].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'a';
					countOfEnemyNear++;
				}
				}
			}
			if(indexOfCellFrom != 4 && indexOfCellFrom != 9 && indexOfCellFrom < 13) { //право
				if(cellsGO[indexOfCellFrom + 1].transform.childCount != 0 && cellsGO[indexOfCellFrom + 1].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom + 1].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom + 1].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom + 1].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'd';
					countOfEnemyNear++;
				}
				}
			}
			if(indexOfCellFrom > 5) { //снизу
				if(cellsGO[indexOfCellFrom - 5].transform.childCount != 0 && cellsGO[indexOfCellFrom - 5].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom - 5].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom - 5].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom - 5].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 's';
					countOfEnemyNear++;
				}
				}
			}
			if(indexOfCellFrom != 4 && indexOfCellFrom < 8) { //вверх вправо
				if(cellsGO[indexOfCellFrom + 6].transform.childCount != 0 && cellsGO[indexOfCellFrom + 6].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom + 6].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
					cellsGO[indexOfCellFrom + 6].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom + 6].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'e';
					countOfEnemyNear++;
				}
				}
			}
			if(indexOfCellFrom != 5 && indexOfCellFrom < 10 && indexOfCellFrom != 0) { //вверх влево
				if(cellsGO[indexOfCellFrom + 4].transform.childCount != 0 && cellsGO[indexOfCellFrom + 4].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom + 4].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom + 4].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom + 4].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'q';
					countOfEnemyNear++;
				}
				}
			}
			if(indexOfCellFrom != 10 && indexOfCellFrom > 6) { //вниз влево
				if(cellsGO[indexOfCellFrom - 6].transform.childCount != 0 && cellsGO[indexOfCellFrom - 6].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom - 6].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom - 6].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom - 6].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'z';
					countOfEnemyNear++;
				}
				}
			}
			if(indexOfCellFrom != 9 && indexOfCellFrom > 4 && indexOfCellFrom != 14) { //вниз вправо
				if(cellsGO[indexOfCellFrom - 4].transform.childCount != 0 && cellsGO[indexOfCellFrom - 4].transform.GetChild(0).name != "BACKLIGHT") { //если в клетке стоит карта
					if(cellsGO[indexOfCellFrom - 4].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly == false) { //если там карта противника
						cellsGO[indexOfCellFrom - 4].transform.GetChild(0).transform.Find("backLight").GetComponent<Image>().enabled = isLight;
						//cellsGO[indexOfCellFrom - 4].transform.GetChild(0).GetComponent<onTankAtWar>().idDirectionCell = 'c';
					countOfEnemyNear++;
				}
				}
			}
		if(indexOfCellFrom == 13 || indexOfCellFrom == 8 || indexOfCellFrom == 9) { //отсюда можно попасть во вражеский штаб
			objectWarPlace.transform.Find("map/Button(4,2)/shtabImg/backLight").GetComponent<Image>().enabled = isLight; //выделили штаб красным(или потушили подсветку)
			countOfEnemyNear++;
		}
		return countOfEnemyNear;
		//}
	}

	//indexOfCellFrom - индекс поля откуда танк ходит
	//possibleMove - количество передвижений
	public void selectNeededCellsToMove(int indexOfCellFrom, int possibleMove, bool shouldReset) { //выделяет клетки, куда можно походить танку
		//int ret = 0;
		if (shouldReset == false) {
			if(indexOfCellFrom < 10) { //сверху
									   //cellsGO[indexOfCellFrom + 5].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
				if(cellsGO[indexOfCellFrom + 5].transform.childCount == 0) { //если в клетке не стоит карта
					GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
					neww.transform.SetParent(cellsGO[indexOfCellFrom + 5].transform, false);
					neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
					Image im = neww.GetComponent<Image>();
					im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
					cellsGO[indexOfCellFrom + 5].GetComponent<memForCells>().idDirectionCell = 'w';
				}
			}
			if(indexOfCellFrom != 1 && indexOfCellFrom != 5 && indexOfCellFrom != 10) { //лево
				//cellsGO[indexOfCellFrom - 1].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
				//cellsGO[indexOfCellFrom - 1].GetComponent<memForCells>().idDirectionCell = 'a';
				if(cellsGO[indexOfCellFrom - 1].transform.childCount == 0) { //если в клетке не стоит карта
					GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
					neww.transform.SetParent(cellsGO[indexOfCellFrom - 1].transform, false);
					neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
					Image im = neww.GetComponent<Image>();
					im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
					cellsGO[indexOfCellFrom - 1].GetComponent<memForCells>().idDirectionCell = 'a';
				}
			}
			if(indexOfCellFrom != 4 && indexOfCellFrom != 9 && indexOfCellFrom != 14) { //право
				//cellsGO[indexOfCellFrom + 1].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
				//cellsGO[indexOfCellFrom + 1].GetComponent<memForCells>().idDirectionCell = 'd';
				if(cellsGO[indexOfCellFrom + 1].transform.childCount == 0) { //если в клетке не стоит карта
					GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
					neww.transform.SetParent(cellsGO[indexOfCellFrom + 1].transform, false);
					neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
					Image im = neww.GetComponent<Image>();
					im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
					cellsGO[indexOfCellFrom + 1].GetComponent<memForCells>().idDirectionCell = 'd';
				}
			}
			if(indexOfCellFrom > 5) { //снизу
				//cellsGO[indexOfCellFrom - 5].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
				//cellsGO[indexOfCellFrom - 5].GetComponent<memForCells>().idDirectionCell = 's';
				if(cellsGO[indexOfCellFrom - 5].transform.childCount == 0) { //если в клетке не стоит карта
					GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
					neww.transform.SetParent(cellsGO[indexOfCellFrom - 5].transform, false);
					neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
					Image im = neww.GetComponent<Image>();
					im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
					cellsGO[indexOfCellFrom - 5].GetComponent<memForCells>().idDirectionCell = 's';
				}
			}
			if(possibleMove > 1) { //если можно ходить горизонтально
				if(indexOfCellFrom != 4 && indexOfCellFrom < 9) { //вверх вправо
					//cellsGO[indexOfCellFrom + 6].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
					//cellsGO[indexOfCellFrom + 6].GetComponent<memForCells>().idDirectionCell = 'e';
					if(cellsGO[indexOfCellFrom + 6].transform.childCount == 0) { //если в клетке не стоит карта
						GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
						neww.transform.SetParent(cellsGO[indexOfCellFrom + 6].transform, false);
						neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
						Image im = neww.GetComponent<Image>();
						im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
						cellsGO[indexOfCellFrom + 6].GetComponent<memForCells>().idDirectionCell = 'e';
					}
				}
				if(indexOfCellFrom != 5 && indexOfCellFrom < 10 && indexOfCellFrom != 0) { //вверх влево
					//cellsGO[indexOfCellFrom + 4].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
					//cellsGO[indexOfCellFrom + 4].GetComponent<memForCells>().idDirectionCell = 'q';
					if(cellsGO[indexOfCellFrom + 4].transform.childCount == 0) { //если в клетке не стоит карта
						GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
						neww.transform.SetParent(cellsGO[indexOfCellFrom + 4].transform, false);
						neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
						Image im = neww.GetComponent<Image>();
						im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
						cellsGO[indexOfCellFrom + 4].GetComponent<memForCells>().idDirectionCell = 'q';
					}
				}
				if(indexOfCellFrom != 10 && indexOfCellFrom > 6) { //вниз влево
					//cellsGO[indexOfCellFrom - 6].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
					//cellsGO[indexOfCellFrom - 6].GetComponent<memForCells>().idDirectionCell = 'z';
					if(cellsGO[indexOfCellFrom - 6].transform.childCount == 0) { //если в клетке не стоит карта
						GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
						neww.transform.SetParent(cellsGO[indexOfCellFrom - 6].transform, false);
						neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
						Image im = neww.GetComponent<Image>();
						im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
						cellsGO[indexOfCellFrom - 6].GetComponent<memForCells>().idDirectionCell = 'z';
					}
				}
				if(indexOfCellFrom != 9 && indexOfCellFrom > 4 && indexOfCellFrom != 14) { //вниз вправо
					//cellsGO[indexOfCellFrom - 4].GetComponent<Image>().color = new Color(0.05490184f, 1, 0, 0.09803922f);
					//cellsGO[indexOfCellFrom - 4].GetComponent<memForCells>().idDirectionCell = 'c';
					if(cellsGO[indexOfCellFrom - 4].transform.childCount == 0) { //если в клетке не стоит карта
						GameObject neww = Instantiate(prefOfLightUnderCell); //создаем под ней подсветку
						neww.transform.SetParent(cellsGO[indexOfCellFrom - 4].transform, false);
						neww.name = "BACKLIGHT"; //меняем название, чтобы потом удалять эту подсветку
						Image im = neww.GetComponent<Image>();
						im.color = new Color(im.color.r, im.color.g, im.color.b, 0.5f); //немного затемняем эту подсветку
						cellsGO[indexOfCellFrom - 4].GetComponent<memForCells>().idDirectionCell = 'c';
					}
				}
			}
		} else {
			if(indexOfCellFrom < 10) { //сверху
									   //cellsGO[indexOfCellFrom + 5].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom + 5];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom != 0 && indexOfCellFrom != 5 && indexOfCellFrom != 10) { //лево
				//cellsGO[indexOfCellFrom - 1].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom - 1];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom != 4 && indexOfCellFrom != 9 && indexOfCellFrom != 14) { //право
				//cellsGO[indexOfCellFrom + 1].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom + 1];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom > 4) { //снизу
				//cellsGO[indexOfCellFrom - 5].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom - 5];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom != 4 && indexOfCellFrom < 9) { //вверх вправо
				//cellsGO[indexOfCellFrom + 6].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom + 6];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom != 5 && indexOfCellFrom < 10 && indexOfCellFrom != 0) { //вверх влево
				//cellsGO[indexOfCellFrom + 4].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom + 4];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom != 10 && indexOfCellFrom > 5) { //вниз влево
				//cellsGO[indexOfCellFrom - 6].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom - 6];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
			if(indexOfCellFrom != 9 && indexOfCellFrom > 4 && indexOfCellFrom != 14) { //вниз вправо
				//cellsGO[indexOfCellFrom - 4].GetComponent<Image>().color = new Color(1, 1, 1, 0.09803922f);
				GameObject go = cellsGO[indexOfCellFrom - 4];
				if(go.transform.childCount > 0 && go.transform.GetChild(0).name == "BACKLIGHT") { //если в клетке подсветка
					Destroy(go.transform.GetChild(0).gameObject);
				}
			}
		}
		
	}

	public void reCheckAllCardAtWar() { //проверяет все карты в руке, чтобы расставить им правильные подсветки сигнализирующие о возможности вывода на поле
		GameObject go = objectWarPlace.transform.Find("myCards").gameObject;
		int len = go.transform.childCount;
		for (int i = 0; i<len; i++) {
			if (go.transform.GetChild(i).gameObject.GetComponent<rememberPos>().uP.needFuel > fuelNow || !isAmaTurn) { //не хватает топлива или не мой ход
				go.transform.GetChild(i).Find("backLight").gameObject.SetActive(false);
				go.transform.GetChild(i).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
			} else {
				go.transform.GetChild(i).Find("backLight").gameObject.SetActive(true);
				go.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
			}
		}

	}

	public IEnumerator moveCardAtWarPointer (GameObject me) { //плавное движение карт при наведении и тп.
		float curTime = 0, timeOfTravel = 0.25f, normalizedValue;
		//Vector3 startPosition = me.GetComponent<rememberPos>().myPos;
		Vector3 startPosition = me.GetComponent<RectTransform>().anchoredPosition;
		int startState = me.GetComponent<rememberPos>().upOrDown;
		while (curTime < timeOfTravel) {
			if (startState != me.GetComponent<rememberPos>().upOrDown) {
				//if (me.GetComponent<rememberPos>().upOrDown != 2) {
					yield break;
				//}
				//yield break;
			}
			curTime += Time.deltaTime;
			normalizedValue = curTime / timeOfTravel; // we normalize our time
			if (me.GetComponent<rememberPos>().upOrDown == 1 || me.GetComponent<rememberPos>().upOrDown == 2) {
				me.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, new Vector3(startPosition.x, me.GetComponent<rememberPos>().myPos.y + 15, startPosition.z), normalizedValue);
			} else {
				me.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, new Vector3(startPosition.x, me.GetComponent<rememberPos>().myPos.y, startPosition.z), normalizedValue);
			}
			
			yield return null;
		}


		yield break;
	}

	public void reckeckPossibilitiesOfCards() { //проверяет возможности карт на возможность ходить и стрелять, дабы менять подсветки и подобную лабуду
		for(int i = 1; i < 14; i++) {
			if(cellsGO[i].transform.childCount != 0) { //если в клетке есть карта
				if(isAmaTurn) { //если наш ход
					//Debug.Log(cellsGO[i].transform.GetChild(0).name);
					if(cellsGO[i].transform.GetChild(0).name == "BACKLIGHT") {
						Destroy(cellsGO[i].transform.GetChild(0).gameObject);
					}
					//Debug.Log(cellsGO[i].transform.GetChild(0).name);
					if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly) {
						int countAround = countAroundYou(i, false); //считает число противников рядом с нашей картой
						onTankAtWar myCard = cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>();
						int freeCellAround = freePlaceToMove(i, myCard.countOfPosibleMove); //число свободных клеток, куда можно походить
						if((countAround > 0 && myCard.canAttack) || (freeCellAround > 0 && myCard.countOfPosibleMove > 0)) {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
							myCard.frameImgGreen.enabled = true;
						} else {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
							myCard.frameImgGreen.enabled = false;
						}
	/*					if((countAround == 0 || !oTAW.canAttack) && (oTAW.countOfPosibleMove == 0 || freeCellAround == 0)) { //карта не может не стрелять, не ходить
							me.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
							oTAW.frameImgGreen.enabled = false;
						}*/
					} else { //вражеская карта
						if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().canBackDamage) {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
						} else {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
						}
					}
				} else { //не наш ход
					if(!cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly) { //вражеская карта
						int countAround = countAroundYou(i, true); //считает число противников рядом с нашей картой
						onTankAtWar myCard = cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>();
						int freeCellAround = freePlaceToMove(i, myCard.countOfPosibleMove); //число свободных клеток, куда можно походить
						if((countAround > 0 && myCard.canAttack) || (freeCellAround > 0 && myCard.countOfPosibleMove > 0)) {
							//Debug.Log("залупа");
							//Debug.Log("countAround = " + countAround);
							//Debug.Log("freeCellAround = " + freeCellAround);
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
							//myCard.frameImgGreen.enabled = true;
						} else {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили карту
							myCard.frameImgGreen.enabled = false;
						}
					} else { //дружественная карта
						if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().canBackDamage) {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
						} else {
							cellsGO[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
						}
					}
				}
			}
		}
	}



	private void checkAllCellsWithMyTank() { //чекает все клетки. И тем, в которых стоит наш танк, восстанавливает возможность двигаться и стрелять, а вражеским восстанавливает возможность ответного урона
		if(isAmaTurn) {
			cellsGO[0].GetComponent<onTankAtWar>().canAttack = true;
			cellsGO[0].GetComponent<onTankAtWar>().frameImgGreen.enabled = true; //вкл подсветку

			for(int i = 1; i < 14; i++) {
				if(cellsGO[i].transform.childCount != 0) { //если в клетке есть карта
					if(cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly) { //если это наша карта
						onTankAtWar myCard = cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>();
						if(myCard.type == "LT") {
							myCard.countOfPosibleMove = 2;
						} else if(myCard.type == "MT") {
							myCard.countOfPosibleMove = 2;
						}
						
						myCard.canAttack = true; //может стрелять
						myCard.transform.Find("attackImg/disable").gameObject.SetActive(false);
						//myCard.canBackDamage = true; //восстанавливает возможность обратного урона своим картам
						//myCard.GetComponent<Image>().color = new Color(1, 1, 1, 1); //делает картинку карты яркой
						//cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().frameImgGreen.enabled = true; //вкл подсветку
					} else { //если не наша карта
						onTankAtWar myCard = cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>();
						myCard.canBackDamage = true; //восстанавливает возможность обратного урона картам противника
						myCard.transform.Find("attackImg/disable").gameObject.SetActive(false);
						//myCard.GetComponent<Image>().color = new Color(1, 1, 1, 1); //делает картинку карты яркой
					}
				}
			}
		} else { //не наш ход
			cellsGO[14].GetComponent<onTankAtWar>().canAttack = true;

			for(int i = 1; i < 14; i++) {
				if(cellsGO[i].transform.childCount != 0) { //если в клетке есть карта
					if(!cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>().isIFriendly) { //если вражеская карта
						onTankAtWar myCard = cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>();
						myCard.countOfPosibleMove = 2; //пока так, но в дальнейшем будет много типов карт и это исправится
						myCard.canAttack = true; //может стрелять
						myCard.transform.Find("attackImg/disable").gameObject.SetActive(false);
						//myCard.GetComponent<Image>().color = new Color(1, 1, 1, 1); //делает картинку карты яркой
					} else {
						onTankAtWar myCard = cellsGO[i].transform.GetChild(0).GetComponent<onTankAtWar>();
						myCard.canBackDamage = true; //восстанавливает возможность обратного урона картам противника
						myCard.transform.Find("attackImg/disable").gameObject.SetActive(false);
						//myCard.GetComponent<Image>().color = new Color(1, 1, 1, 1); //делает картинку карты яркой
					}
				}
			}
		}
		//cellsGO[0].GetComponent<onTankAtWar>().canAttack = true;

		//cellsGO[0].transform.Find("shtabImg").GetComponent<Image>().color = new Color(1, 1, 1, 1); //делает картинку карты яркой

		//cellsGO[0].GetComponent<onTankAtWar>().frameImgGreen.enabled = true; //вкл подсветку


	}

	public void endTurn () { //заканчивает ход
		if(isAmaTurn) {
			StartCoroutine(sendInfoAboutMove(-1, -1, "NULL", false, true)); //отправляем инфу о том, что мы закончили ход
			if(selectedCardMoveOrAttack != null) { //если была выбрана карта
				selectedCardMoveOrAttack.GetComponent<onTankAtWar>().clickOnTankToGo(); //тушим её и клетки, которые она выделила
																						//это обязательно делать до того, как isAmaTurn стал false
			}
			isAmaTurn = false;
			myTime.color = new Color(0.4056604f, 0.4008767f, 0.376958f); //делаю своё время серым
			enemyTime.color = new Color(0.7529412f, 0.7568628f, 0.7411765f); //делаю время противника белым
			StartCoroutine(checkNewMove()); //чекаем ходы соперника
			secondsStepAtWar = 0; //восстановили время на ход
			minutesStepAtWar = 2; //восстановили вермя на ход
			stepTime.color = new Color(0.6698113f, 0.1737718f, 0.1789934f); //сделал время на ход красным
			
			buttonOfEndTurn.gameObject.SetActive(false);

			fuelNowEnemy = plusFuelEnemy; //заполнили топляк противнику
			objectWarPlace.transform.Find("map/Button(4,2)/fuelImg/Text").GetComponent<Text>().text = plusFuelEnemy.ToString(); //вывели число топляка противника

			reCheckAllCardAtWar(); //меняем цвета карт в руке

			cellsGO[14].transform.Find("shtabImg/downPolosa").GetComponent<Image>().enabled = true; //включили красную херню над штабом противника
			cellsGO[0].transform.Find("shtabImg/backlight").GetComponent<Image>().enabled = false; //выключили зеленый орнамент(анимированный) на нашем штабе

			cellsGO[14].transform.Find("shtabImg").GetComponent<Image>().color = new Color(1, 1, 1, 1); //подсветили штаб противника
			cellsGO[14].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(false);
			cellsGO[0].transform.Find("shtabImg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили свой штаб
			cellsGO[0].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(true);

			checkAllCellsWithMyTank(); //чекает все клетки. И тем, в которых стоит наш танк, восстанавливает возможность двигаться и стрелять, а вражеским восстанавливает возможность ответного урона
			reckeckPossibilitiesOfCards(); //проверяет возможности карт на возможность ходить и стрелять, дабы менять подсветки и подобную лабуду

			cardsEnemy--; //число карт противника сократилось
			countOfCardEnemyText.text = cardsEnemy.ToString(); //выставили число карт соперника
			GameObject neww = Instantiate(prefOfEnemyBackCard); //создали карту противнику в руку
			neww.transform.SetParent(objectWarPlace.transform.Find("enemyCards"), false);
		} else {
			isAmaTurn = true;
			myTime.color = new Color(0.7529412f, 0.7568628f, 0.7411765f); //делаю своё время белым
			enemyTime.color = new Color(0.4056604f, 0.4008767f, 0.376958f); //делаю время противника серым
			secondsStepAtWar = 0; //восстановили время на ход
			minutesStepAtWar = 2; //восстановили вермя на ход
			stepTime.color = new Color(0.259167f, 0.6037736f, 0.3227867f); //сделал время на ход зеленым
			
			buttonOfEndTurn.gameObject.SetActive(true);

			fuelNow = plusFuel;
			objectWarPlace.transform.Find("map/Button(0,0)/fuelImg/Text").GetComponent<Text>().text = plusFuel.ToString(); //вывели число топляка своё

			reCheckAllCardAtWar(); //меняем цвета карт в руке
			
			checkAllCellsWithMyTank(); //чекает все клетки. И тем, в которых стоит наш танк, восстанавливает возможность двигаться и стрелять, а вражеским восстанавливает возможность ответного урона
			reckeckPossibilitiesOfCards(); //проверяет возможности карт на возможность ходить и стрелять, дабы менять подсветки и подобную лабуду

			cellsGO[0].transform.Find("downPolosa").GetComponent<Image>().enabled = true; //включили зеленую херню под своим штабом

			cellsGO[14].transform.Find("shtabImg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); //затемнили штаб противника
			cellsGO[14].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(true);
			cellsGO[0].transform.Find("shtabImg").GetComponent<Image>().color = new Color(1, 1, 1, 1); //подсветили наш штаб
			cellsGO[0].transform.Find("shtabImg/attackImg/disable").gameObject.SetActive(false);

			createNewCardForMeAtWar();
			numOfCard--; //число наших карт в колоде уменьшилось
			countOfCardText.text = numOfCard.ToString(); //обновили число наших карт в колоде
		}

		return;
	}

	public void pointerOnCell(GameObject me) { //навел мышку на клетку
		if(me.transform.childCount > 0 && me.transform.GetChild(0).name == "BACKLIGHT") {
			Image img = me.transform.GetChild(0).GetComponent<Image>();
			img.color = new Color(img.color.r, img.color.g, img.color.b, 1); //делает подсветку ярче
			Cursor.SetCursor(cursors[1], Vector2.zero, CursorMode.Auto); //ставлю вместо курсора пальчик
		}
	}

	public void pointerExitCell(GameObject me) { //убрал мышь с клетки
		if(me.transform.childCount > 0 && me.transform.GetChild(0).name == "BACKLIGHT") {
			Image img = me.transform.GetChild(0).GetComponent<Image>();
			img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f); //делает подсветку тусклее
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //ставлю стандартный курсор
		}
	}
}

[Serializable]
public class resTech{ //исследованная техника (в профиле)
	public List<unit> units;
}


[Serializable]
public class unit{ //имя и количство данной техники (в профиле)
	public string name;
	public int count;
	//public int idOfEverything; //id для быстрого поиска цены на сервере, для нахождения нужной картинки техники и тд
}

[Serializable]
public class unitParam{ //цена техники (на сервере)
	public string name;
	public int expPrice;
	public int monPrice;
	public int power;
	public string type;
	public string info;
	public int damage;
	public int hp;
	public int plusFuel;
	public int needFuel;
	public string russianName;
	public String nation;
	public int numSpecial; //1-нанести 2 урона
}

[Serializable]
public class paramUnits{ //цены на технику (на сервере)
	public unitParam[] unit;
}

[Serializable]
public class Profile { //информация юзера
	public string login;
	public int money;
	public int expi;
	public int gold;
	public string info_tanks;
	public string shtabs;
}

[Serializable]
public class shtabsOfUser{ //информация о штабах игрока
	public List<shtab> shtabs;
}

[Serializable]
public class shtab { //информация о штабах игрока
	public string name;
	public int power;
	public int exp;
	public List<cardForChangeColoda> cards;
}

[Serializable]
public class cardForChangeColoda { //карты в колоде (выбранные, тоесть которые возьмут в бой)
	public string name; 
	public int count; //количество
	//public int countCollection; //число данной карты в коллекции
	//public int countColoda; //число данной карты в колоде
}

[Serializable]
public class infoAboutAllShtab { //информация о штабах с сервера
	public infoAboutOneShtab[] iAOS;
}

[Serializable]
public class infoAboutOneShtab { //информация о штабе с сервера
	public string name;
	public int expPrice;
	public int monPrice;
	public string info0;
	public string info1;
	public string info2;
	public int power;
	public int hp;
	public int damage;
	public int plusFuel;
	public String nation;
}

[Serializable] 
public class imageWithName { // имя и картинка
	public string name;
	public Sprite img;
}

[Serializable]
public class enemyClass { //логин противника и название места, где у вас будет махач
	public enemyX enemy;
}

[Serializable]
public class enemyX {
	public string login; //ник противника
	public string namePlace; //имя комнаты в базе данных
	public infoToEnemyMain infoEnemy; //infoEnemyMain
	public int isAmaFirst; //я хожу первый?
	public bool amaSecond; //true - меня нашли, false - я нашёл
}

[Serializable]

public class infoToEnemyMain { //свои данные нужно положить на сервер
	public int countOfCard; //число карт
	public String nameOfShtab; //имя штаба
}

[Serializable]
public class freeCard { //класс свободной карты для генерации рандомной карты в бою
	public string name;
	public int count;
}

[Serializable]
public class templateOfMove { //шаблон хода
	public int whoAttack; //откуда бахаем (или же с какой клетки двигалась карта при флаге isCardToMove)
	public int toAttack; //куда бахаем (если who != -1), куда вывели карту (если cardThatUse != NULL), куда карта идёт (если isCardToMove)
	public bool isCardMove; //true - карта двигалась, false - (или кто-то стрелял, или куда-то вывели карту)
	public string cardThatUse; //имя карты, которую вывел на поле
}
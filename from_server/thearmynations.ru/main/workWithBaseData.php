<?php
date_default_timezone_set('Asia/Novosibirsk');
require '/home/n68203/public_html/thearmynations.ru/libs/rb-mysql.php'; //найдет библиотеку, иначе - ошибка и прикращение скрипта

R::setup( 'mysql:host=localhost; dbname=n68203_mainBase', 'n68203_Nikita', 'x7sqbmj7yi5g8ojzkf' ); //подключились к базе mySQL

//$host = 'localhost'; // адрес сервера 
//$database = 'n68203_NewBase'; // имя базы данных
//$user = 'n68203_dbuser'; // имя пользователя
//$password = 'hbt9hlhs6ssqsmw1of'; // пароль

//$link = mysqli_connect($host, $user, $password, $database) 
//    or die("Ошибка " . mysqli_error($link));

if (!R::testConnection()) { //проверка, есть ли соединение?
	echo 'Сервер временно недоступен...';
	exit ('Ошибка подключения к базе данных!');
}

function register_user ($login, $password) {
	$repeatCheck = R::findOne('users', 'login = ?', array($login));
	if (isset($repeatCheck)) return "001"; //аккаунт занят
	
	$user = R::dispense('users'); //по факту - создание новой таблицы
	$user->login = $login; //добавили новое поле с логином
	$user->password = $password; //добавили новое поле с паролем
	$user->money = 10000;
	$user->expi = 0;
	$user->gold = 50;
	/*$user->infoTanks = json_encode( array(
		'MS1' => 3,
		'BT2' => -1,
		'T26' => -1
	));*/
	$user->infoTanks = '{"units":[{"name":"MS1","count":3,"idOfEverything":0}]}';
	$user->shtabs = '{"shtabs":[{"name":"sh_uchebnayachast","power":25,"exp":0,"cards":[{"name":"MS1","countCollection":3,"countColoda":0}]}]}';
	//json_encode(["shtabs" => 
	//	["name" => "sh_uchebnayachast", "power" => 25, "exp" => 0, "cards" => [["name"=> "MS1", "count" => -3]]]
	//]);
	R::store($user); //сохранили таблицу в базу данных
	return "0"; //пользователь зарегистрирован
}

function get_user ($login, $password) {
	$user = R::findOne('users', 'login = ? AND password = ?', [$login, $password]);
	//if (!isset($user)) return '002'; //неверный логин или пароль
	if (!isset($user)) {
			$arr = array(
				'result' => "002" //неверный логин или пароль
				//'money' => 0,
				//'expi' => 0,
				//'gold' => 0
			);
		//return (json_encode($arr));
		return ('2');
	}
	$arr = array(
		'result' => '0', //вошел
		'money' => $user->money,
		'expi' => $user->expi,
		'gold' => $user->gold
	);
	//return (json_encode ($arr));
	return ('0');
}

function saveMsg ($nameOfTable, $login, $textMsg) {
	$user = R::dispense($nameOfTable); //по факту - создание новой таблицы
	$user->timeWhen = date("H:i"); // часы и минуты
	$user->login = $login;
	$user->textMsg = $textMsg;
	$user->yearMonthDay = date("y:n:N");
	R::store($user);
	return 0;
}

function getAllParamResearch () {
	$user = R::findOne('researchcosts', 'id = 1');
	return $user->base;
}

function getInfoUser ($login) {
	$user = R::findOne('users', 'login = ?', [$login]);
	return $user;
}

function updateShtabInfoServer () {
	$user = R::findOne('researchcosts', 'id = 1');
	return $user->baseshtab;
}

function getInfoAboutShtabClient ($login) {
	$user = R::findOne('users', 'login = ?', [$login]);
	return $user->shtabs;
}

function getAllUserNewInfo ($login, $money, $expi, $gold, $infoTanks, $shtabs) {
	$user = R::findOne('users', 'login = ?', array($login));
	$user->money = $money;
	$user->expi = $expi;
	$user->gold = $gold;
	$user->infoTanks = $infoTanks;
	$user->shtabs = $shtabs;
	R::store($user);
}

function firstSendFindEnemy ($login, $mainInfo) {
    $user = R::dispense('tablefindenemy'); //по факту - создание новой таблицы
    $user->login = $login; //добавили новое поле с логином
    $user->status = 0; //0 - свободен, 1- уже в бою
    $user->whoFindMe = "NULL";
    $user->mainInfo = $mainInfo;
    $user->uniqIdentity = "None";
    R::store($user);
    
}

function deleteCellFind ($login) {
	$user = R::findOne('tablefindenemy', 'login = ?', [$login]);
	R::trash($user);
}

    function aLotRequest($login) { //множество запросов на поиск противника
        $ama = R::findOne('tablefindenemy', 'login = ?', [$login]);
        if ($ama->status == 0) { //если мой статус 0, тоесть я еще не приглашен никем в бой
            $user = R::findOne('tablefindenemy', 'login != ? AND status != 1', [$login]);
            if (!isset($user)) return "NON";
            
            $ama->status = 1;
            $ama->firstStep = rand(0, 1); //каким ходит игрок
            $ama->whoFindMe = $user->login; //кого я нашёл
            
            $unId = $ama->login . $user->login . rand(1, 1000111000);
            
            
            $user->status = 1;
            $user->whoFindMe = $login;
            $user->firstStep = 1 - $ama->firstStep; //каким ходит противник
            
            $uMIC = $user->mainInfo; //user main info copy
            
            $user->mainInfo = $ama->mainInfo; //записали ему число наших карт, чтобы он мог это число потом получить
            $user->uniqIdentity = $unId;
            R::store($user);
            R::store($ama);
            //$res =  $ama->login . $user->login; //имя комнаты
            
            
            $newTable = R::dispense('warhistory'); //по факту - создание новой таблицы
            $newTable->name = $unId; //добавили новое поле с именем боя
            $newTable->step = -1;
            $newTable->login = $ama->login;
            $newTable->history = "";
            $newTable->endFlag = false;
            R::store($newTable);
            
            //R::trash($ama);
            return '{"enemy":{"login":"' . $user->login . '","namePlace":"' . $unId .'", "infoEnemy": '. $uMIC .', "isAmaFirst": '. $ama->firstStep .', "amaSecond":false}}';
        } else {
            $user = R::findOne('tablefindenemy', 'login = ?', [$ama->whoFindMe]); //нашли своего соперника
            $user->enemyReady = true;
            R::store($user);
            
            $amacopy = $ama;
            R::trash($ama);
            return '{"enemy":{"login":"' . $amacopy->whoFindMe . '","namePlace":"' . $amacopy->uniqIdentity .'", "infoEnemy": '. $amacopy->mainInfo .', "isAmaFirst": '. $amacopy->firstStep .', "amaSecond":true}}';
        }

    
    }
    
    function checkConfirmation($login, $shouldDelete) { //проверка, что противник принял приглашение в бой
        $ama = R::findOne('tablefindenemy', 'login = ?', [$login]);
        
        if($shouldDelete == 1) {
            $enemy = R::findOne('tablefindenemy', 'login = ?', [$ama->whoFindMe]);
            R::trash($enemy);
            $ama->status = 0;
            R::store($ama);
            return "ok";
        }
        
        if($ama->enemyReady == true) {
             R::trash($ama);
             return "go";
        } else {
            return "wait";
        }
    }
    
    function sendInfoAboutMove($nameRoom, $step, $login, $history, $endFlag) {
        $warhistory = R::dispense('warhistory');
        
        $warhistory->name = $nameRoom;
        $warhistory->step = $step;
        $warhistory->login = $login;
        $warhistory->history = $history;
        $warhistory->endFlag = $endFlag;
        
        R::store($warhistory);
    }
    
    function checkNewMove($nameRoom, $step) {
        $move = R::findOne('warhistory', 'name = ? AND step = ?', [$nameRoom, $step]); //получил строку хода
        if (!isset($move)) { //еще не походил (тобишь ход не нашелся)
            return "NONE";
        } else {
            $endFlag = $move->endFlag;
            if ($endFlag != 0) {
                R::trash($move);
                return "1";
            }
            $his= $move->history;
            R::trash($move);
            return $his;
        }
    }
?>
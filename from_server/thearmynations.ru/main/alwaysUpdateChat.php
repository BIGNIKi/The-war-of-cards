<?php 

//скрипт проверяет на наличие новых сообщений

date_default_timezone_set('Asia/Novosibirsk'); //выставляем время
require '/home/n68203/public_html/thearmynations.ru/libs/rb-mysql.php'; //найдет библиотеку, иначе - ошибка и прикращение скрипта

R::setup( 'mysql:host=localhost; dbname=n68203_mainBase', 'n68203_Nikita', 'x7sqbmj7yi5g8ojzkf' ); //подключились к базе mySQL

$hour = $_POST['hour']; //время, после которого будем загружать сообщения
$idAtChat = $_POST['idAtChat']; //id собщения в таблице
$nameOfTable = $_POST['nameOfTable']; //имя таблицы
$ymd = date("y:n:N"); //год, месяц, день

if ($hour == "-1") { //первый запрос серверу
	$last = R::findLast($nameOfTable); //находит последнюю строку таблицы
	$hour = $last->time_when; //время последнего сообщения
	$idAtChat = -1; //даем id -1
	$arr = array( //массив данных, которые отправим клиенту
		'hour' => $hour,
		'idAtChat' => $last->id,
		'login' => "0",
		'textMsg' => "0"
	);
	echo (json_encode ($arr)); 
	return;
} else {
	$res  = R::findOne($nameOfTable, 'time_when >= ? AND year_month_day = ? AND id > ?', array($hour,$ymd, $idAtChat)); //находим нужную строку
	if (isset($res)) { //если нашли
		$arr = array(
			'hour' => $res->time_when,
			'idAtChat' => $res->id,
			'login' => $res->login,
			'textMsg' => $res->text_msg
		);
		echo (json_encode ($arr));
		return;
	} else { //если не нашли
		$arr = array(
		'hour' => $hour,
		'idAtChat' => $idAtChat,
		'login' => "0",
		'textMsg' => "0"
	);
	echo (json_encode ($arr));
	return;
	}
}

?>
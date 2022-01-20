<?php
	require '/home/n68203/public_html/thearmynations.ru/libs/rb-mysql.php'; //найдет библиотеку, иначе - ошибка и прикращение скрипта
	R::setup( 'mysql:host=localhost; dbname=n68203_mainBase', 'n68203_Nikita', 'x7sqbmj7yi5g8ojzkf' ); //подключились к базе mySQL
	
	//$user = R::findOne('researchcosts', 'id = 1');
	/*$user->base = json_encode([
		["name" => "MS1", "expPrice" => 0, "monPrice" => 0, "power" => 1, "type" => "LT", "info" => "Советский легкий танк 1 уровня", "damage" => 1, "hp" => 3, "plusFuel" => 0, "needFuel" => 2, "russianName" => "МС-1"], 		
		["name" => "BT2", "expPrice" => 200, "monPrice" => 1600, "power" => 1, "type" => "LT", "info" => "Советский легкий танк 1 уровня", "damage" => 2, "hp" => 4, "plusFuel" => 2, "needFuel" => 3, "russianName" => "БТ-2"],
		["name" => "T26", "expPrice" => 250, "monPrice" => 2500, "power" => 1, "type" => "LT", "info" => "Советский легкий танк 1 уровня", "damage" => 2, "hp" => 6, "plusFuel" => 0, "needFuel" => 4, "russianName" => "Т-26"]
	]);*/
	
	//$user->baseshtab = json_encode([
	//["name" => "sh_uchebnayachast", "expPrice" => 0, "monPrice" => 0,  "info0" => "Учебный штаб", "info1" => "Советский учебный штаб 1 уровня", "info2" =>"Учебная колода СССР", "power" => 10, "hp" => 18, "damage" => 2, "plusFuel" => 5]
	//]);
	//R::store($user); //сохранили таблицу в базу данных
	
	//$user = R::findOne('users', 'login = "BIGNIK"');
	
//	$user->expi = 675;
//	$user->infoTanks = '{"units":[{"name":"MS1","count":3,"idOfEverything":0}]}';
//	$user->shtabs = '{"shtabs":[{"name":"sh_uchebnayachast","power":25,"exp":160,"cards":[{"name":"MS1","count":-3}]}]}';
	
//	R::store($user); //сохранили таблицу в базу данных
//	echo "Всё";
 ?>
<?php

//отправка сообщеня на сервер

require 'workWithBaseData.php';

$nameOfTable = $_POST ['nameOfTable'];
$login = $_POST['login'];
$textMsg = $_POST['textOfMsg'];

saveMsg($nameOfTable, $login, $textMsg); 

?>
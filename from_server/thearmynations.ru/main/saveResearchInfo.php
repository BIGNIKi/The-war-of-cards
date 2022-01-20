<?php

require 'workWithBaseData.php';

$login = $_POST['Login'];
$money = $_POST['Money'];
$expi = $_POST['Expi'];
$gold = $_POST['Gold'];
$infoTanks = $_POST['InfoTanks'];
$shabs = $_POST['Shtabs'];

getAllUserNewInfo($login, $money, $expi, $gold, $infoTanks, $shabs);

?>
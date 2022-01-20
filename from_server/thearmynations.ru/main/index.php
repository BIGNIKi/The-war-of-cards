<?php

require 'workWithBaseData.php';

$login = $_POST['Login'];
$password = $_POST['Password'];

if (!isset($login) && !isset($password)) {
	echo 'Неверные данные!';
	exit ('Неверные данные!');
}

echo register_user ($login, $password);

?>
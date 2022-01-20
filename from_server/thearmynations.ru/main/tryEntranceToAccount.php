<?php

require 'workWithBaseData.php';

$login = $_POST['Login'];
$password = $_POST['Password'];

echo get_user($login, $password);


?>
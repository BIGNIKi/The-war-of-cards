<?php 

require 'workWithBaseData.php';

$login = $_POST['Login'];

echo getInfoUser($login);

?>
<?php

	require 'workWithBaseData.php';
    $login = $_POST['Login'];
    $mainInfo = $_POST['infoToEnemyMain'];
    firstSendFindEnemy($login, $mainInfo);
    echo "Complete";
?>
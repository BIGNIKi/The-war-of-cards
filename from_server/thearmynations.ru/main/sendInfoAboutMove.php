<?php
    require 'workWithBaseData.php';
    $nameRoom = $_POST['Name'];
    $step = $_POST['Step'];
    $login = $_POST['Login'];
    $history = $_POST['History'];
    $endFlag = $_POST['EndFlag'];
    sendInfoAboutMove($nameRoom, $step, $login, $history, $endFlag);
?>
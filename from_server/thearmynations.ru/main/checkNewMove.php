<?php
    require 'workWithBaseData.php';
    $nameRoom = $_POST['Name'];
    $step = $_POST['Step'];
    echo checkNewMove($nameRoom, $step);
?>
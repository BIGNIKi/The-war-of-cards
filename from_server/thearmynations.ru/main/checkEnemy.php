<?php

    require 'workWithBaseData.php';
    $login = $_POST['Login'];
    $shouldDelete = $_POST["Delete"];
    echo checkConfirmation($login, $shouldDelete);

?>
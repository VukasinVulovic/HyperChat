<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");

    if (!Validate::userName($_POST["username"]) || !Validate::userPassword($_POST["password"])) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $user = $db->createUser($_POST["username"], $_POST["password"]);

    echo json_encode($user);
?>
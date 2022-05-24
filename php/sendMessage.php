<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");

    if (!Validate::chatId($_POST["chat"]) || !Validate::userId($_POST["user"]) || !Validate::userToken($_POST["token"])) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $sent = $db->sendMessage($_POST["text"], $_POST["chat"], $_POST["user"], $_POST["token"]);

    echo json_encode($sent);
?>
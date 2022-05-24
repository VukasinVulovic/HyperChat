<?php
    // header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");

    if (!Validate::chatId($_GET["chat"]) || !Validate::userId($_GET["user"]) || !Validate::userToken($_GET["token"])) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $messages = $db->getChatMessages($_GET["chat"], $_GET["user"], $_GET["token"]);

    echo json_encode($messages);
?>
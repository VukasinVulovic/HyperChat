<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");

    if (
        strlen($_GET["user"]) <= 0 || strlen($_GET["chat"]) <= 0 || strlen($_GET["token"]) <= 0 ||
        !preg_match("/^[0-9]{0,5}$/", $_GET["user"]) || !preg_match("/^[0-9]{0,5}$/", $_GET["chat"])
    ) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $messages = $db->getChatMessages($_GET["chat"], $_GET["user"], $_GET["token"]);

    echo json_encode($messages);
?>
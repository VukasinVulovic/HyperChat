<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");

    if (
        strlen($_POST["text"]) <= 0 || strlen($_POST["chat"]) <= 0 || strlen($_POST["user"]) <= 0 || strlen($_POST["token"]) <= 0 ||
        !preg_match("/^[0-9]{0,5}$/", $_GET["chat"]) || !preg_match("/^[0-9]{0,5}$/", $_GET["user"])    
    ) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $messages = $db->sendMessage($_POST["text"], $_POST["chat"], $_POST["user"], $_POST["token"]);

    echo json_encode($messages);
?>
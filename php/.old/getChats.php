<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");
    
    if(
        strlen($_GET["user"]) <= 0 || strlen($_GET["token"]) <= 0 || 
        !preg_match("/^[0-9]{0,5}$/", $_GET["user"])
    ) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $chats = $db->getChats($_GET["user"], $_GET["token"]);

    echo json_encode($chats);
?>
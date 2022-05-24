<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");
    
    if (!Validate::chatName($_POST["name"]) || !Validate::userId($_POST["user"]) || !Validate::userToken($_POST["token"])) {
        http_response_code(400);
        return;
    }

    $db = new DB("localhost", "chatapp", "root", "");
    $created = $db->createChat($_POST["name"], $_POST["user"], $_POST["token"]);

    echo json_encode($created);
?>
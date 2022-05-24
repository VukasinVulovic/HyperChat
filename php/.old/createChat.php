<?php
    header("Content-Type: application/json; charset=utf-8");
    require_once("class.db.php");

    // if (
    //     strlen($_POST["username"]) <= 0 || strlen($_POST["password"]) <= 0 ||
    //     !preg_match("/^[a-zA-Z0-9_]{1,20}$/", $_POST["username"]) || !preg_match("/^(?=.*?[a-z])(?=.*?[0-9]).{8,20}$/", $_POST["password"])
    // ) {
    //     http_response_code(400);
    //     return;
    // }

    $db = new DB("localhost", "chatapp", "root", "");
    $messages = $db->createChat($_POST["name"], $_POST["user_id"], $_POST["token"]);

    echo json_encode($messages);
?>
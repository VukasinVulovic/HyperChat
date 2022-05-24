<?php
    function randString(int $len) {
        $str = "";

        for(; $len > 0; $len--)
            $str .= array_rand(str_split("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890"));

        return $str;
    }

    function randNums(int $len) {
        $str = "";

        for(; $len > 0; $len--)
            $str .= array_rand(str_split("1234567890"));

        return $str;
    }

    class DB {
        private $conn;

        public function __construct(string $domain, string $databaseName, string $username, string $password) {
            $this->conn = new mysqli($domain, $username, $password, $databaseName);

            if ($this->conn->connect_error) {
                http_response_code(500);
                return;
            }
        }

        private function getUsersInChat(string $userId, string $chatId, string $token) {
            $sql = "SELECT id, name FROM users WHERE (SELECT user_id FROM tokens WHERE user_id='" . $userId . "' AND token='" . $token . "' LIMIT 1)='" . $userId . "' AND (SELECT user_id FROM chat_relations WHERE user_id='" . $userId . "' AND chat_id='" . $chatId . "' LIMIT 1)='" . $userId . "'";
            $res = mysqli_query($this->conn, $sql);
        
            if($res->num_rows <= 0)
                return array();
        
            $rows = mysqli_fetch_all($res);
            return array_map(fn(array $row) => array(
                "id" => $row[0],
                "username" => $row[1]
            ), $rows);
        }

        public function getChats(string $userId, string $token) {
            $sql = "SELECT id, name FROM chats WHERE (SELECT user_id FROM tokens WHERE user_id='" . $userId . "' AND token='" . $token . "' LIMIT 1)='" . $userId . "' AND (SELECT user_id FROM chat_relations WHERE user_id='" . $userId . "' LIMIT 1)='" . $userId . "'";
            $res = mysqli_query($this->conn, $sql);
        
            if($res->num_rows <= 0)
                return array();
        
            $rows = mysqli_fetch_all($res);
            return array_map(fn(array $row) => array(
                "chat_id" => $row[0],
                "name" => $row[1],
                "users" => $this->getUsersInChat($userId, $row[0], $token)
            ), $rows);
        }

        public function getChatMessages(string $chatId, string $userId, string $token) {
            $sql = "SELECT id, user_id, (SELECT name FROM users WHERE users.id=user_id), text FROM messages WHERE (SELECT user_id FROM chat_relations WHERE (SELECT user_id FROM tokens WHERE user_id='" . $userId . "' AND token='" . $token . "' LIMIT 1)='" . $userId . "' AND user_id='" . $userId . "' AND chat_id='" . $chatId . "' LIMIT 1)='" . $userId . "'";
            $res = mysqli_query($this->conn, $sql);
        
            if($res->num_rows <= 0)
                return array();
        
            $rows = mysqli_fetch_all($res);
            return array_map(fn(array $row) => array(
                "id" => $row[0],
                "user_id" => $row[1],
                "username" => $row[2],
                "text" => $row[3]
            ), $rows);
        }

        public function sendMessage(string $text, string $chatId, string $userId, string $token) {
            $text = base64_encode($text);
            $sql = "INSERT INTO messages(user_id, chat_id, text) SELECT '" . $userId . "', '" . $chatId . "', '" . $text . "' WHERE (SELECT user_id FROM chat_relations WHERE (SELECT user_id FROM tokens WHERE user_id='" . $userId . "' AND token='" . $token . "' LIMIT 1)='" . $userId . "' AND user_id='" . $userId . "' AND chat_id='" . $chatId . "' LIMIT 1)='" . $userId . "'";
            $res = mysqli_query($this->conn, $sql);

            if($res && mysqli_affected_rows($this->conn) > 0)
                return true;

            return false;
        }

        public function createUser(string $username, string $password) {
            $salt = randString(16);
            $hash = hash("sha256", $password . $salt);

            $sql = "INSERT INTO users(name) SELECT ('" . $username . "') WHERE '" . $username . "' NOT IN (SELECT name FROM users)";
            $res = mysqli_query($this->conn, $sql);

            if(!$res || mysqli_affected_rows($this->conn) <= 0)
                return false;


            $sql = "SELECT id FROM users WHERE name='" . $username . "' LIMIT 1";
            $res = mysqli_query($this->conn, $sql);
        
            if($res->num_rows <= 0)
                return array();
        
            $row = mysqli_fetch_row($res);
                
            $sql = "INSERT INTO creds(user_id, password, salt) VALUES('" . $row[0] . "', '" . $hash . "', '" . $salt . "')";
            $res = mysqli_query($this->conn, $sql);

            if(!$res || mysqli_affected_rows($this->conn) <= 0)
                return false;

            return $this->createToken($row[0], $password);
        }

        public function createToken(string $userId, string $password) {
            $res = mysqli_query($this->conn, "SELECT password, salt FROM creds WHERE user_id='" . $userId . "' LIMIT 1");

            if($res->num_rows <= 0)
                return false;
        
            $row = mysqli_fetch_row($res);
            $hash = hash("sha256", $password . $row[1]);

            if($hash != $row[0])
                return false;

            $token = randString(20);
            $res = mysqli_query($this->conn, "DELETE FROM tokens WHERE user_id='" . $userId . "'");
            $res = mysqli_query($this->conn, "INSERT INTO tokens(user_id, token) VALUES('" . $userId . "', '" . $token . "')");

            if($res && mysqli_affected_rows($this->conn) > 0)
                return $token;
                
            return false;
        }

        public function createChat(string $name, string $userId, string $token) {
            $id = randNums(5);
            $sql = "INSERT INTO chats(id, name) SELECT '" . $id . "', '" . $name . "' WHERE (SELECT user_id FROM tokens WHERE token='" . $token . "' AND user_id='" . $userId . "')='" . $userId . "'";
            $res = mysqli_query($this->conn, $sql);

            if(!$res || mysqli_affected_rows($this->conn) <= 0)
                return false;

            $sql = "INSERT INTO chat_relations(user_id, chat_id) SELECT '" . $userId . "', '" . $id . "' WHERE (SELECT user_id FROM tokens WHERE token='" . $token . "' AND user_id='" . $userId . "')='" . $userId . "'";
            $res = mysqli_query($this->conn, $sql);

            if($res && mysqli_affected_rows($this->conn) > 0)
                return $name . "@" . $id;

            return false;
        }

        public function __destruct() {
            mysqli_close($this->conn);
        }
    }
?>
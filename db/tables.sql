CREATE TABLE users(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	name VARCHAR(25)
);

CREATE TABLE chats(
	id VARCHAR(10) NOT NULL PRIMARY KEY,
    name VARCHAR(25)
);

CREATE TABLE chat_relations(
    user_id INT NOT NULL,
    chat_id VARCHAR(10) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (chat_id) REFERENCES chats(id)
);

CREATE TABLE messages(
	id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    chat_id VARCHAR(10) NOT NULL,
    text VARCHAR(250),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (chat_id) REFERENCES chats(id)
);

CREATE TABLE creds(
    user_id INT NOT NULL,
    password VARCHAR(64),
    salt VARCHAR(40),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE tokens(
    user_id INT NOT NULL,
    token VARCHAR(40),
    FOREIGN KEY (user_id) REFERENCES users(id)
);
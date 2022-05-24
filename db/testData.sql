INSERT INTO users(name) VALUES ('User 1');
INSERT INTO users(name) VALUES ('User 2');

INSERT INTO tokens(user_id, token) VALUES (1, 'd97c908ed44c25fdca302612c70584c8d5acd47a');
INSERT INTO tokens(user_id, token) VALUES (2, 'a50f71f97620945ab2250778b0379c459e9c63a5');

INSERT INTO tokens(user_id, token) VALUES (1, 'd97c908ed44c25fdca302612c70584c8d5acd47a');
INSERT INTO tokens(user_id, token) VALUES (2, 'a50f71f97620945ab2250778b0379c459e9c63a5');

INSERT INTO chats(name) VALUES ('Chat 1');
INSERT INTO chats(name) VALUES ('Chat 2');
INSERT INTO chats(name) VALUES ('Chat 3');

INSERT INTO messages(user_id, chat_id, text) VALUES (1, 1, 'Hello, World!');
INSERT INTO messages(user_id, chat_id, text) VALUES (2, 1, 'Hello, World!');
INSERT INTO messages(user_id, chat_id, text) VALUES (1, 2, 'Hello, World!');
INSERT INTO messages(user_id, chat_id, text) VALUES (2, 2, 'Hello, World!');
INSERT INTO messages(user_id, chat_id, text) VALUES (1, 3, 'Hello, World!');

INSERT INTO chat_relations(user_id, chat_id) VALUES (1, 1);
INSERT INTO chat_relations(user_id, chat_id) VALUES (2, 1);
INSERT INTO chat_relations(user_id, chat_id) VALUES (1, 2);
INSERT INTO chat_relations(user_id, chat_id) VALUES (2, 2);
INSERT INTO chat_relations(user_id, chat_id) VALUES (1, 3);
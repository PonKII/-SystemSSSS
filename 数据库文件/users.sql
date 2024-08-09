-- 创建表users
CREATE TABLE users (
    user_id VARCHAR(255) PRIMARY KEY,
    user_name VARCHAR(255) NOT NULL,
    real_name VARCHAR(255),
    password VARCHAR(255) NOT NULL,
    is_validation INT DEFAULT 0,
    is_can_login INT DEFAULT 1,
    is_permanent INT DEFAULT 0,
    avatar VARCHAR(255),
    gender INT DEFAULT 0
);

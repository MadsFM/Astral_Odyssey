-- Drop the existing schema if needed
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

create table universes
(
    universeid  serial
        primary key,
    name        varchar(100) not null,
    description text
);

alter table universes
    owner to "Odyssey";

create table users
(
    userid       serial
        primary key,
    username     varchar(50)  not null
        unique,
    email        varchar(100) not null
        unique,
    passwordhash varchar(255) not null,
    createdat    timestamp default CURRENT_TIMESTAMP
);

alter table users
    owner to "Odyssey";

create table roles
(
    roleid   serial
        primary key,
    rolename varchar(50) not null
        unique
);

alter table roles
    owner to "Odyssey";

-- Insert predefined roles into the roles table
INSERT INTO roles (rolename)
VALUES ('Admin'),
       ('Player'),
       ('Game Manager');


create table userroles
(
    userid    integer not null
        references users on delete cascade,
    roleid    integer not null
        references roles on delete cascade,
    createdat timestamp default CURRENT_TIMESTAMP,
    primary key (userid, roleid)
);

alter table userroles
    owner to "Odyssey";

create table planets
(
    planetid     serial
        primary key,
    name         varchar(100) not null,
    description  text,
    universeid   integer      not null
        references universes on delete cascade,
    isdiscovered boolean default false
);

alter table planets
    owner to "Odyssey";

create table quests
(
    questid     serial
        primary key,
    title       varchar(100) not null,
    description text,
    planetid    integer
        references planets on delete cascade,
    universeid  integer
        references universes on delete cascade,
    iscompleted boolean default false
);

alter table quests
    owner to "Odyssey";

create table scoreboard
(
    scoreid   serial
        primary key,
    userid    integer not null
        references users on delete cascade,
    points    integer not null,
    updatedat timestamp default CURRENT_TIMESTAMP
);

alter table scoreboard
    owner to "Odyssey";

create table quizzes
(
    quizid   serial
        primary key,
    question text         not null,
    answer   varchar(100) not null,
    hint     text,
    questid  integer
        references quests on delete cascade
);

alter table quizzes
    owner to "Odyssey";

create table userquestprogress
(
    progressid  serial
        primary key,
    userid      integer not null
        references users on delete cascade,
    questid     integer not null
        references quests on delete cascade,
    iscompleted boolean   default false,
    lastupdated timestamp default CURRENT_TIMESTAMP
);

alter table userquestprogress
    owner to "Odyssey";

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'dbPlayer')
CREATE DATABASE dbPlayer;
GO
USE dbPlayer
--dropping tables
IF OBJECT_ID('vwAll') IS NOT NULL
DROP VIEW vwAll;

IF OBJECT_ID('tblUsersSongsRelation') IS NOT NULL 
DROP TABLE tblUsersSongsRelation;

IF OBJECT_ID('tblUsers') IS NOT NULL 
DROP TABLE tblUsers;

IF OBJECT_ID('tblSongs') IS NOT NULL 
DROP TABLE tblSongs;


CREATE TABLE tblUsers(
	userId INT PRIMARY KEY IDENTITY(1,1),
	username VARCHAR(20) NOT NULL,
	password VARCHAR(20) NOT NULL
);

CREATE TABLE tblSongs(
	songId INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30) NOT NULL,
	author VARCHAR(30) NOT NULL,
	duration INT not null
);  

--many to many relation
CREATE TABLE tblUsersSongsRelation(
	userId INT NOT NULL,
	songId INT NOT NULL,
    FOREIGN KEY (userId ) REFERENCES tblUsers(userId), 
    FOREIGN KEY (songId) REFERENCES tblSongs(songId),
    UNIQUE (userId, songId)
);

GO
CREATE VIEW vwAll
as
select u.userId, u.username, u.password, s.songId, s.name, s.author, s.duration
from tblUsersSongsRelation r
inner join tblSongs s
on s.songId = r.userId
inner join tblUsers u
on u.userId = s.songId

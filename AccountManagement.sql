create database AccountManagement
go
use AccountManagement
go

create table Account(
	ID int IDENTITY(1,1) primary key,
	username nvarchar(100) not null unique,
	password nvarchar(100) not null
)


create table RoomChat(
	ID int IDENTITY(1,1) primary key,
	name nvarchar(100) not null
)

create table Room_Account(
	roomID int references RoomChat(ID),
	accountID int references Account(ID),
	primary key(accountID, roomID)
)

create table PrivateMessage(
	ID int IDENTITY(1,1) primary key,
	accountID1 int references Account(ID),
	accountID2 int references Account(ID)
)

create table Message(
	ID int IDENTITY(1,1) primary key,
	roomID int references RoomChat(ID) null,
	privateMessageID int references PrivateMessage(ID) null,
	senderID int references Account(ID),
	content nvarchar(4000) not null,
	timestamp datetime not null
)
SELECT * FROM Message
insert Account(username, password) values('quyen', '123')
insert Account(username, password) values('diep', '123')
insert Account(username, password) values('thuy', '123')
insert Account(username, password) values('anh', '123')

select * from Account

insert RoomChat(name) values('Nhom 1')
insert RoomChat(name) values('Nhom 2')
insert RoomChat(name) values('Nhom 3')

insert Room_Account(roomID, accountID) values(2,1)
insert Room_Account(roomID, accountID) values(3,1)
insert Room_Account(roomID, accountID) values(2,2)
insert Room_Account(roomID, accountID) values(3,2)
insert Room_Account(roomID, accountID) values(2,3)
insert Room_Account(roomID, accountID) values(2,4)
insert Room_Account(roomID, accountID) values(3,4)

insert PrivateMessage(accountID1, accountID2) values(1,2)
insert PrivateMessage(accountID1, accountID2) values(1,3)
insert PrivateMessage(accountID1, accountID2) values(1,4)
insert PrivateMessage(accountID1, accountID2) values(2,3)
insert PrivateMessage(accountID1, accountID2) values(2,4)

insert Message(roomID, senderID, content, timestamp) values(1, 1, 'Hello', CURRENT_TIMESTAMP)
insert Message(roomID, senderID, content, timestamp) values(1, 2, 'Hi', CURRENT_TIMESTAMP)
insert Message(roomID, senderID, content, timestamp) values(1, 1, 'Xin chao', CURRENT_TIMESTAMP)
insert Message(roomID, senderID, content, timestamp) values(1, 1, 'Hello', CURRENT_TIMESTAMP)
insert Message(roomID, senderID, content, timestamp) values(1, 1, 'Xin chao cac ban', CURRENT_TIMESTAMP)

insert Message(privateMessageID, senderID, content, timestamp) values(1, 2, N'Chào bạn', CURRENT_TIMESTAMP)
insert Message(privateMessageID, senderID, content, timestamp) values(1, 2, N'hihhihi', CURRENT_TIMESTAMP)
insert Message(privateMessageID, senderID, content, timestamp) values(1, 2, N'Hello', CURRENT_TIMESTAMP)
insert Message(privateMessageID, senderID, content, timestamp) values(2, 3, N'Chào bạn', CURRENT_TIMESTAMP)
insert Message(privateMessageID, senderID, content, timestamp) values(1, 1, N'Chào bạn', CURRENT_TIMESTAMP)


create procedure GetPrivateMessagesByUsername
@username nvarchar(100)
as
begin
    -- Get private messages with the chat partner's username
    select
        P.ID as privateMessageID,
        case
            when P.accountID1 = (select ID from Account where username = @username)
                then (select username from Account where ID = P.accountID2)
            else (select username from Account where ID = P.accountID1)
        end as chatPartnerUsername
    from PrivateMessage as P
    where (P.accountID1 = (select ID from Account where username = @username)
           or P.accountID2 = (select ID from Account where username = @username))
    --and exists (select 1 from Message where privateMessageID = P.ID);
end 

alter procedure GetGroupChatsByUsername
@username nvarchar(100)
as
begin
	select
        RC.ID as roomChatID,
        RC.name as roomChatName
    from RoomChat as RC
    inner join Room_Account as RA on RC.ID = RA.roomID
    inner join Account as A on RA.accountID = A.ID
    where A.username = @username
    and exists (select 1 from Message where roomID = RC.ID);
end

exec GetMessageInPrivateMessage 1
alter procedure GetMessageInPrivateMessage
@privateMessageID int
as
begin
	select Message.ID, privateMessageID, senderID, (select username from Account where Account.ID = senderID) as senderName, content, timestamp
	from Message
	inner join PrivateMessage on Message.privateMessageID = PrivateMessage.ID
	where Message.privateMessageID = @privateMessageID
end

create procedure GetRoomMembersByID
@roomID int
as
begin
    select A.username as MemberUsername
    from RoomChat as RC
    join Room_Account as RA on RC.ID = RA.roomID
    join Account as A on RA.accountID = A.ID
    where RC.ID = @roomID
end

--Lấy Id của chat client
CREATE PROCEDURE GetPrivateMessageID1
    @username1 nvarchar(100),
    @username2 nvarchar(100)
AS
BEGIN
    SELECT PM.ID
    FROM PrivateMessage PM
    JOIN Account A1 ON PM.accountID1 = A1.ID
    JOIN Account A2 ON PM.accountID2 = A2.ID
    WHERE (A1.username = @username1 AND A2.username = @username2)
       OR (A1.username = @username2 AND A2.username = @username1);
END;
EXEC GetPrivateMessageID1 'anh', 'quyen';
select * from Message

 CREATE PROCEDURE DeleteMessageInPrivateMessage
@privateMessageID int
AS
BEGIN
    DELETE FROM Message WHERE privateMessageID = @privateMessageID;
END;

EXEC DeleteMessageInPrivateMessage 1;


CREATE PROCEDURE GetUserIDByUsername
    @Username nvarchar(100)
AS
BEGIN
    SELECT ID
    FROM Account
    WHERE username = @Username;
END;



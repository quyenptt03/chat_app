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

--Lấy người nhắn từ tên Username
alter procedure GetPrivateMessagesByUsername
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
exec GetPrivateMessagesByUsername 'anh'

--Lấy nhóm từ tên (Quyen)
create  procedure GetGroupChatsByUsername
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

--Lấy nhóm từ tên (Diep)
create procedure GetGroupChatsByUsername1
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
    
end
exec GetGroupChatsByUsername1 "thuy"

--Lay tin nhăn chat cua 1 nguoi
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

--Lay thành viên của 1 nhóm từ ID
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

--Lấy Id của chat client để lưu tin nhắn off
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

--Xóa tin nhắn offline
 CREATE PROCEDURE DeleteMessageInPrivateMessage
@privateMessageID int
AS
BEGIN
    DELETE FROM Message WHERE privateMessageID = @privateMessageID;
END;

EXEC DeleteMessageInPrivateMessage 1;

--Lấy ID từ tên 
CREATE PROCEDURE GetUserIDByUsername
    @Username nvarchar(100)
AS
BEGIN
    SELECT ID
    FROM Account
    WHERE username = @Username;
END;
exec GetUserIDByUsername 'thuy'

--Lấy danh sách tin nhắn nhóm từ ID
create procedure GetMessageRoomMessage
@roomID int
as
begin
    select 
        Message.ID, 
        Message.roomID, 
        Message.senderID, 
        Account.username as senderName, 
        Message.content, 
        Message.timestamp
    from 
        Message
    inner join 
        Account on Message.senderID = Account.ID
    where 
        Message.roomID = @roomID
end
exec GetMessageRoomMessage 3

DELETE FROM Message
WHERE roomID = 3;

-- Hàm sinh  tên nhóm
ALTER FUNCTION SinhTenNhom() RETURNS NVARCHAR(100)
AS
BEGIN
    DECLARE @MaxStt INT;
    DECLARE @NewStt INT;
    DECLARE @NewTenNhom NVARCHAR(100);

    SELECT @MaxStt = ISNULL(MAX(CAST(SUBSTRING(name, LEN('Nhóm ')+1, LEN(name)-LEN('Nhóm ')) AS INT)), 0)
    FROM RoomChat;

    SET @NewStt = @MaxStt + 1;

    SET @NewTenNhom = 'Nhóm ' + CAST(@NewStt AS NVARCHAR);

    RETURN @NewTenNhom;
END;


--Tạo thành viên ra nhóm
alter PROCEDURE CreateGroupWithMember
    @MemberIDs NVARCHAR(MAX)
AS
BEGIN
    DECLARE @NewRoomID INT;
    DECLARE @GroupName NVARCHAR(100);

    SET @GroupName = dbo.SinhTenNhom();

    INSERT INTO RoomChat (name)
    VALUES (@GroupName);

    SET @NewRoomID = SCOPE_IDENTITY();

    CREATE TABLE #TempMemberIDs (ID INT);

    INSERT INTO #TempMemberIDs (ID)
    SELECT CAST(value AS INT)
    FROM dbo.SplitString(@MemberIDs, ',');

    INSERT INTO Room_Account (roomID, accountID)
    SELECT @NewRoomID, ID
    FROM #TempMemberIDs;

    DROP TABLE #TempMemberIDs;

    PRINT N'Đã tạo nhóm thành công';
END;

CREATE FUNCTION SplitString
(
    @Input NVARCHAR(MAX),
    @Delimiter NVARCHAR(255)
)
RETURNS TABLE
AS
RETURN
(
    WITH Split (ID, Value, StartPos, EndPos) AS
    (
        SELECT
            1,
            SUBSTRING(@Input, 1, CHARINDEX(@Delimiter, @Input + @Delimiter) - 1),
            CHARINDEX(@Delimiter, @Input),
            CHARINDEX(@Delimiter, @Input + @Delimiter)
        WHERE
            CHARINDEX(@Delimiter, @Input) > 0
        UNION ALL
        SELECT
            ID + 1,
            SUBSTRING(@Input, EndPos + 1, CHARINDEX(@Delimiter, @Input + @Delimiter, EndPos + 1) - EndPos - 1),
            CHARINDEX(@Delimiter, @Input, EndPos + 1),
            CHARINDEX(@Delimiter, @Input + @Delimiter, EndPos + 1)
        FROM
            Split
        WHERE
            CHARINDEX(@Delimiter, @Input, EndPos + 1) > 0
    )
    SELECT
        ID,
        Value
    FROM
        Split
);


EXEC CreateGroupWithMember '1,3,4,';
select * from Room_Account where roomID=27

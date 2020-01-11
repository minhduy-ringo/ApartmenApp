USE ApartmentManagement
GO
/*----------------------------------------------------------------------
	STOERD PROCEDURE
*/----------------------------------------------------------------------
CREATE OR ALTER PROCEDURE USP_TaskInsertNewTask
(
	@taskType smallint,
	@priority smallint = 0,
	@complexId smallint,
	@buildingID smallint,
	@startDate datetime,
	@endDate datetime,
	@description nvarchar(200),
	@cost money = 0
)
AS
	INSERT INTO [Task] VALUES (@taskType,0,@priority,@complexId,@buildingID,@startDate,@endDate,@description,@cost)
GO

CREATE OR ALTER PROCEDURE USP_LoginAdd
(
	@staffId int,
	@username nvarchar(50),
	@password nvarchar(50),
	@response nvarchar(200) OUTPUT
)
AS
	SET NOCOUNT ON
	DECLARE @salt UNIQUEIDENTIFIER = NEWID()
	
	BEGIN TRY
		INSERT INTO [Login] (staffId,username,password,salt)
		VALUES (@staffId,@username, HASHBYTES('SHA2_512', @password+CAST(@salt as nvarchar(36))),@salt)
	END TRY
	BEGIN CATCH
		SET @response = ERROR_MESSAGE()
	END CATCH
GO

CREATE OR ALTER PROCEDURE USP_Login
(
	@username nvarchar(50),
	@password nvarchar(50)
)
AS
	SET NOCOUNT ON

	DECLARE @staffId INT

    IF EXISTS (SELECT TOP 1 staffId FROM [dbo].[Login] WHERE username=@username)
    BEGIN
		SET @staffId = (SELECT staffId FROM [dbo].[Login] WHERE username=@username AND password=HASHBYTES('SHA2_512', @password+CAST(Salt AS NVARCHAR(36))))
		SELECT * FROM [Staff] WHERE staffId = @staffId
    END
GO
/*----------------------------------------------------------------------
	Tài khoản test
*/----------------------------------------------------------------------
DECLARE @response nvarchar(200)
BEGIN
	--Tài khoản quản lý
	EXEC dbo.USP_LoginAdd 
				@staffId = 6,
				@username = N'manager',
				@password = N'manager',
				@response = @response OUTPUT
	--Tài khoản nhân viên
	EXEC dbo.USP_LoginAdd 
				@staffId = 1,
				@username = N'employee',
				@password = N'employee',
				@response = @response OUTPUT
END
select @response
GO

DECLARE @response smallint, @isManager smallint
EXEC dbo.USP_Login @username = N'1', @password = N'1' 
SELECT @@ROWCOUNT
GO

select * from NoticeReceiver  where receiverId = 15

select * from Notice where noticeId IN ( select noticeId from NoticeReceiver where receiverId = 15)
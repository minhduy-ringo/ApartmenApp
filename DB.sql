--use master
--go
--drop database [ApartmentManagement]
--go

USE [master]
GO
CREATE DATABASE [ApartmentManagement]
go
CREATE LOGIN [ApartmentApiConnect] WITH PASSWORD=N'123456', DEFAULT_DATABASE=[ApartmentManagement], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [ApartmentApiConnect] ENABLE
GO

GRANT SELECT ON SCHEMA :: [dbo] TO apiconnect
go
GRANT INSERT ON SCHEMA :: [dbo] TO apiconnect
go
GRANT UPDATE ON SCHEMA :: [dbo] TO apiconnect
go
GRANT DELETE ON SCHEMA :: [dbo] TO apiconnect
go


use [ApartmentManagement]
go
-----------------------------------------------
create table Complex(
	complexId smallint IDENTITY NOT NULL,
	name nvarchar(100),
	address nvarchar(200),
	city nvarchar(50),
	numOfBuilding smallint,
	mainPhone nvarchar(15),

	CONSTRAINT [PK_Complex_CompId] PRIMARY KEY CLUSTERED
	(
		[complexId]
	)
)
go
create table Building(
	complexId smallint NOT NULL,
	buildingNum smallint NOT NULL,
	numOfStaff smallint,
	numOfTenant int,
	numOfDepartment int,
	availApartment int,

	CONSTRAINT [PK_Building_BuildingNum] PRIMARY KEY CLUSTERED
	(
		[complexId],
		[buildingNum]
	)
)
go
create table Department(
	departmentId smallint NOT NULL,
	name nvarchar(50),
	managerId int,

	CONSTRAINT [PK_Department_DepId] PRIMARY KEY CLUSTERED
	(
		[departmentId]
	)
)
go
create table Staff(
	staffId int NOT NULL,
	name nvarchar(100),
	birthday DATE,
	address nvarchar(100),
	city nvarchar(50),
	mobile nvarchar(15),
	departmentId smallint,
	complexId smallint,
	joinDate DATE,
	leaveDate DATE,

	CONSTRAINT [PK_Staff_staffId] PRIMARY KEY CLUSTERED
	(
		[staffId]
	)
)
go
create table StaffVacation
(
	staffVacationId int IDENTITY NOT NULL,
	staffId int NOT NULL,
	maxVacationDay tinyint,
	usedDay tinyint,

	CONSTRAINT [PK_StaffVacation_Id_staffId] PRIMARY KEY CLUSTERED
	(
		[staffVacationId],
		[staffId]
	)
)
go
create table Schedule(
	scheduleId int IDENTITY NOT NULL,
	staffId int NOT NULL,
	workDate DATE,
	startWorkHour TIME(0),
	endWorkHour TIME(0),
	isHoliday bit,
	isWeekend bit,

	CONSTRAINT [PK_Schedule_ScheduleId_staffId] PRIMARY KEY CLUSTERED
	(
		[scheduleId],
		[staffId]
	)
)
go
create table TaskType(
	taskTypeId nvarchar(10) NOT NULL,
	taskTypeName nvarchar(50),

	CONSTRAINT [PK_TaskType_TaskTypeId] PRIMARY KEY CLUSTERED
	(
		[taskTypeId]
	)
)
go
create table Task(
	taskId int IDENTITY NOT NULL,
	taskTypeId nvarchar(10) NOT NULL,
	taskStatus nvarchar(50),
	priority smallint,
	complexId smallint,
	buildingNum smallint,
	startDate DATETIME,
	endDate DATETIME,
	description nvarchar(200),
	cost money,

	CONSTRAINT [PK_Task_TaskId_staffId] PRIMARY KEY CLUSTERED
	(
		[taskId]
	)
)
go
create table TaskStaff(
	taskId int NOT NULL,
	staffId int NOT NULL,

	CONSTRAINT [PK_TaskStaff_TaskId_StaffId] PRIMARY KEY CLUSTERED
	(
		[taskId],
		[staffId]
	)
)
go
create table LeaveRequest(
	leaveRequestId int,
	staffId int,
	leaveDate DATE,
	type nvarchar(20),
	confirmStatus nvarchar(20),
	leaveReason nvarchar(200),

	CONSTRAINT [PK_LeaveNotice_LeaveNoticeId] PRIMARY KEY CLUSTERED
	(
		[leaveRequestId],
		[staffId]
	)
)
go

--Foregin key
alter table LeaveRequest
add constraint FK_LeaveRequest_Staff
foreign key (staffId) references Staff(staffId)
go
alter table Task
add constraint FK_Task_TaskType
foreign key (taskTypeId) references TaskType(taskTypeId)
go
alter table Task
add constraint FK_Task_Building
foreign key (complexId,buildingNum) references Building(complexId,buildingNum)
go
alter table TaskStaff
add constraint FK_TaskStaff_Task
foreign key (taskId) references Task(taskId) ON DELETE CASCADE
go
alter table TaskStaff
add constraint FK_TaskStaff_Staff
foreign key (staffId) references Staff(staffId) ON DELETE CASCADE
go
alter table StaffVacation
add constraint FK_StaffVacation_Staff
foreign key (staffId) references Staff(staffId)
go
alter table Schedule
add constraint FK_Schedule_Staff
foreign key (staffId) references Staff(staffId)
go
alter table Staff
add constraint FK_Staff_Complex
foreign key (complexId) references Complex(complexId)
go
alter table Staff
add constraint FK_Staff_Department
foreign key (departmentId) references Department(departmentId)
go
alter table Building
add constraint FK_Building_Complex
foreign key (complexId) references Complex(complexId)
go
alter table Department
add constraint FK_Department_Staff
foreign key (managerId) references Staff(staffId)
go
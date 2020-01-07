USE ApartmentManagement
GO

CREATE OR ALTER VIEW ManagerTaskView
AS
SELECT
	taskId,
	TaskType.taskTypeName,
	taskStatus,
	buildingNum
FROM
	Task INNER JOIN TaskType ON Task.taskTypeId = TaskType.taskTypeId
GO

CREATE OR ALTER VIEW ManagerScheduleView
AS
SELECT
	Schedule.scheduleId,
	Staff.name,
	Schedule.workDate,
	Schedule.startWorkHour,
	Schedule.endWorkHour
FROM
	Schedule INNER JOIN Staff ON Schedule.staffId = Staff.staffId
GO

/*----------------------------------------------------------------------
	STOERD PROCEDURE
*/----------------------------------------------------------------------

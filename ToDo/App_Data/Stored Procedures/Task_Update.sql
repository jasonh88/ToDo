alter proc dbo.Task_Update
		@Id int
		,@Task nvarchar(100)
		,@Notes nvarchar(200)
		,@Due datetime
		,@Completed bit
as
/*
Declare @ID int = 17
		,@Task nvarchar(100) = 'Dont do laundry'
		,@Notes nvarchar(200) = 'Dryer'
		,@Due datetime = '2017-06-28 14:30:00'
		,@Completed bit = 0
Exec dbo.Task_Update
		@ID
		,@Task
		,@Notes
		,@Due
		,@Completed

Select * from dbo.Task
*/
BEGIN
UPDATE [dbo].[Task]
   SET [Task] = @Task
      ,[Notes] = @Notes
      ,[Due] = @Due
      ,[Completed] = @Completed
      ,[DateModified] = getUTCDate()
 WHERE ID = @ID
END



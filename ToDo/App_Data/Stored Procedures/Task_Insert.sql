alter proc dbo.Task_Insert
			@Task nvarchar(100)
			,@Notes nvarchar(200)
			,@Due datetime
			,@Completed bit 
			,@ID int output

as
/*
Declare @Task nvarchar(100) = 'Do Laundry'
				,@Notes nvarchar(200) = 'Air dry!'
				,@Due datetime = 1900-01-01 00:00:00
				,@Completed bit = 1
				,@ID int = 1

Exec dbo.Task_Insert
		@Task
		,@Notes
		,@Due
		,@Completed
		,@ID output

Select * from Task
*/
BEGIN
INSERT INTO [dbo].[Task]
           ([Task]
           ,[Notes]
           ,[Due]
           ,[Completed]
           ,[DateAdded]
           ,[DateModified])
     VALUES
           (@Task	
           ,@Notes
           ,@Due
           ,@Completed
           ,getutcdate()
           ,getutcdate())
		   
		   SET @ID = SCOPE_IDENTITY()
END



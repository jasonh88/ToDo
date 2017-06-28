alter proc dbo.Task_Delete
	@Id int
as
/*
declare @ID int = 17

exec dbo.Task_Delete
			@ID

			SELECT * FROM TASK
*/
BEGIN
DELETE FROM [dbo].[Task]
      WHERE ID = @ID
END



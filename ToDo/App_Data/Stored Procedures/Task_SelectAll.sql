create proc dbo.Task_SelectAll

as
/*
exec dbo.Task_SelectAll

*/
BEGIN
SELECT [ID]
      ,[Task]
      ,[Notes]
      ,[Due]
      ,[Completed]
      ,[DateAdded]
      ,[DateModified]
  FROM [dbo].[Task]
END


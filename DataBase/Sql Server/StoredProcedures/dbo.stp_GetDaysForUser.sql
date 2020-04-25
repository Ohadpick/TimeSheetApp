
CREATE OR ALTER PROCEDURE dbo.stp_GetDaysForUser 
			@userId			int = NULL, 
			@startDate		date = NULL, 
			@endDate		date = NULL
AS
BEGIN
	-- gets week first day
	SET @startDate = dbo.fn_WeekFirstDay (@startDate)

	IF @endDate IS NULL
		SET @endDate = dbo.fn_WeekLastDay (@startDate)

	;WITH CTE AS (
		  SELECT @startDate as ReportedDate
		  UNION ALL
		  SELECT DATEADD (day, 1, ReportedDate)
		  FROM CTE
		  WHERE DATEADD (day, 1, ReportedDate) <= @endDate
		 )
	SELECT	CTE.ReportedDate
			,D.UserId
			,D.DaySequenceId
			,D.ProjectId
			,D.StartTime
			,D.EndTime
			,D.Remark
	FROM	CTE LEFT JOIN Days D
	ON		CTE.ReportedDate	= D.ReportedDate
	AND		D.UserId			= @userId


END


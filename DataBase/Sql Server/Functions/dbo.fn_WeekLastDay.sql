
CREATE OR ALTER FUNCTION dbo.fn_WeekLastDay (
			@date date = NULL)
RETURNS date
AS
BEGIN
	IF @date IS NULL
		SET @date = getdate()

	SET @date = DATEADD (day, 7 - DATEPART(WEEKDAY, @date), @date)

	RETURN @date
END



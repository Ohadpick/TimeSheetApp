
CREATE OR ALTER FUNCTION dbo.fn_WeekFirstDay (
			@date date = NULL)
RETURNS date
AS
BEGIN
	IF @date IS NULL
		SET @date = getdate()

	SET @date = DATEADD (day, -DATEPART(WEEKDAY, @date) + 1, @date)

	RETURN @date
END



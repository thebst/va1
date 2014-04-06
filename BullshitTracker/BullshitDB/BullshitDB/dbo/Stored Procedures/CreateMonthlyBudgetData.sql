
CREATE PROCEDURE [dbo].[CreateMonthlyBudgetData]



AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
insert into MonthlyBudget (Period, Budget)
select Periods.Id, Budgets.Id from
Periods
cross join
Budgets


END

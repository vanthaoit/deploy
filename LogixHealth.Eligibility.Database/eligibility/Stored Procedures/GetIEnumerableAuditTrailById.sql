
CREATE PROCEDURE [eligibility].[GetIEnumerableAuditTrailById] @Field nvarchar(15)
AS
select * from eligibility.AuditTrails WHERE Field = @Field 

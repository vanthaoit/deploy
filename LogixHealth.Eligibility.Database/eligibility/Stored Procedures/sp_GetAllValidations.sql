 CREATE PROCEDURE [eligibility].[sp_GetAllValidations]  
    
    AS
    BEGIN
    SET NOCOUNT ON;

    select *
	from eligibility.MdRequestValidation
    END
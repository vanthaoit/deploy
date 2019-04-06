 CREATE PROCEDURE [eligibility].[sp_GetValidationCodeKeys]  
    
    AS
    BEGIN
    SET NOCOUNT ON;

    select ValidationCode
	from eligibility.MdRequestValidation
    END
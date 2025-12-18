SET IDENTITY_INSERT dbo.Patients ON;

INSERT INTO dbo.Patients (Id, Name, Condition, Age)
VALUES (10, 'imrank', 'good', 25);

SET IDENTITY_INSERT dbo.Patients OFF;

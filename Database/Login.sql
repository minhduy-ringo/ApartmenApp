USE [master]
GO
CREATE LOGIN [ApartmentApiConnect] WITH PASSWORD=N'123456', DEFAULT_DATABASE=[ApartmentManagement], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [ApartmentApiConnect] ENABLE
GO
/****** Object:  User [apiconnect]    Script Date: 12/17/2019 5:22:43 PM ******/
USE [ApartmentManagement]
GO
CREATE USER [apiconnect] FOR LOGIN [ApartmentApiConnect] WITH DEFAULT_SCHEMA=[dbo]
GO
GRANT SELECT ON SCHEMA :: [dbo] TO apiconnect
go
GRANT INSERT ON SCHEMA :: [dbo] TO apiconnect
go
GRANT UPDATE ON SCHEMA :: [dbo] TO apiconnect
go
GRANT DELETE ON SCHEMA :: [dbo] TO apiconnect
go
GRANT EXECUTE ON SCHEMA :: [dbo] to apiconnect
go
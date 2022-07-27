use [Flights]

truncate table [dbo].[ArrivingFlights]
truncate table [dbo].[DepartingFlights]
truncate table [dbo].[Flights]
truncate table [dbo].[Histories]

update [dbo].[Stations]
set [FlightInStation] = NULL

update [dbo].[Stations]
set [PrepTime] = 10
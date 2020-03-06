
CREATE PROCEDURE [dbo].[Tournament_insert]  
  @p_TournamentName nvarchar(max) 
AS  
BEGIN
  INSERT INTO [dbo].[Tournaments] ([TournamentName])
  VALUES (@p_TournamentName)

  SELECT SCOPE_IDENTITY() AS TournamentId
END

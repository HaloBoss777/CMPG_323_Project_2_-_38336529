IF NOT EXISTS (SELECT * FROM sysobjects WHERE [Name] = 'User' and xtype='U')
BEGIN

CREATE TABLE [User] (
	[UserId] int IDENTITY(1,1) NOT NULL,
	[Email] varchar(max) NOT NULL,
	[Password] varchar(max) NOT NULL

	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
	[UserId] ASC

)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY] 
) ON [PRIMARY]

END
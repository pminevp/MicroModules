USE [test]
GO
/****** Object:  StoredProcedure [dbo].[proc_AddGroup]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_AddGroup]
	-- Add the parameters for the stored procedure here
@Name nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   insert into tblUserGroup ([Name]) values (@Name)

END

GO
/****** Object:  StoredProcedure [dbo].[proc_AddUserToGroup]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_AddUserToGroup]
	-- Add the parameters for the stored procedure here
	 @UserId int,
	 @GroupId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

if(select count(*) from tblUserGroupAssociation where UserId=@UserId and GroupId=@GroupId)<1
begin
insert into tblUserGroupAssociation(UserId,GroupId)
		values(@UserId,@GroupId)
end

END

GO
/****** Object:  StoredProcedure [dbo].[proc_CreateUser]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_CreateUser]
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(255),
	@Password nvarchar(max),
	@FirstName nvarchar(255),
	@LastName nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 
 insert into tblUsers(UserName,Password,FirstName,LastName)
			values(@UserName,@Password,@FirstName,@LastName)

select @@IDENTITY
END

GO
/****** Object:  StoredProcedure [dbo].[proc_DeleteGroup]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteGroup]
	-- Add the parameters for the stored procedure here
	@groupId int
 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 
 if(select count(*) from tblUserGroupAssociation where GroupId=@groupId)=0
 begin
		delete from tblUserGroup where GroupId=@groupId
 end

END

GO
/****** Object:  StoredProcedure [dbo].[proc_EditUser]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[proc_EditUser]
	-- Add the parameters for the stored procedure here
	@UserId int,
 
	@FirstName nvarchar(255),
	@LastName nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 
Update tblUsers 
		set FirstName=@FirstName, LastName=@LastName
		where UserId= @UserId 
 
END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetHashedPassword]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetHashedPassword]
	-- Add the parameters for the stored procedure here
@UserName nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select top 1 * from tblUsers where UserName =@UserName

END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetUserGroups]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserGroups]
	-- Add the parameters for the stored procedure here
 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT        GroupId, Name FROM            tblUserGroup

END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetUserGroupsByUserId]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetUserGroupsByUserId]
	-- Add the parameters for the stored procedure here
 @UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT        u.GroupId, u.Name FROM  tblUserGroup u
						inner join tblUserGroupAssociation uga on uga.GroupId = u.GroupId

						where uga.UserId=@UserId

END

GO
/****** Object:  StoredProcedure [dbo].[proc_RemoveUserToGroup]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[proc_RemoveUserToGroup]
	-- Add the parameters for the stored procedure here
	 @UserId int,
	 @GroupId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

if(select count(*) from tblUserGroupAssociation where UserId=@UserId and GroupId=@GroupId)>0
begin
delete from tblUserGroupAssociation where UserId=@UserId and GroupId= @GroupId
	 
end

END

GO
/****** Object:  StoredProcedure [dbo].[proc_UserLogin]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_UserLogin]
	-- Add the parameters for the stored procedure here
 @UserName nvarchar(255),
 @Password nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT        UserId, UserName, Password, FirstName, LastName
FROM            tblUsers where UserName = @UserName and Password=@Password

END

GO
/****** Object:  Table [dbo].[tblUserGroup]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserGroup](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_tblUserGroup] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUserGroupAssociation]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserGroupAssociation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_tblUserGroupAssociation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 4/6/2016 10:52:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

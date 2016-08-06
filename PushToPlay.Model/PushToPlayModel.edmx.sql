
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/06/2016 17:38:29
-- Generated from EDMX file: D:\Projetos\PushToPlay\PushToPlay.Model\PushToPlayModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GameDetailPlatform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameDetails] DROP CONSTRAINT [FK_GameDetailPlatform];
GO
IF OBJECT_ID(N'[dbo].[FK_GameDetailUserGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGames] DROP CONSTRAINT [FK_GameDetailUserGame];
GO
IF OBJECT_ID(N'[dbo].[FK_GameGameDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameDetails] DROP CONSTRAINT [FK_GameGameDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_GameGenre_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameGenre] DROP CONSTRAINT [FK_GameGenre_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_GameGenre_Genre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameGenre] DROP CONSTRAINT [FK_GameGenre_Genre];
GO
IF OBJECT_ID(N'[dbo].[FK_GameUserGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGames] DROP CONSTRAINT [FK_GameUserGame];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_GroupEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupUserGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroups] DROP CONSTRAINT [FK_GroupUserGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationFriendUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationFriend] DROP CONSTRAINT [FK_RelationFriendUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RelationFriendUser1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RelationFriend] DROP CONSTRAINT [FK_RelationFriendUser1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_UserEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGames] DROP CONSTRAINT [FK_UserUserGame];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroups] DROP CONSTRAINT [FK_UserUserGroup];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActivityMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityMessages];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[GameDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameDetails];
GO
IF OBJECT_ID(N'[dbo].[GameGenre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameGenre];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO
IF OBJECT_ID(N'[dbo].[Genres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genres];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Platforms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Platforms];
GO
IF OBJECT_ID(N'[dbo].[RelationFriend]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RelationFriend];
GO
IF OBJECT_ID(N'[dbo].[SteamUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SteamUsers];
GO
IF OBJECT_ID(N'[dbo].[UserGames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGames];
GO
IF OBJECT_ID(N'[dbo].[UserGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroups];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Group_Id] int  NULL,
    [User_Id] int  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [GiantBombID] bigint  NOT NULL,
    [Aliases] nvarchar(max)  NULL,
    [MetacriticScore] int  NULL,
    [MetacriticUrl] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL,
    [Developer] nvarchar(max)  NULL,
    [Publisher] nvarchar(max)  NULL,
    [SteamAppId] int  NULL,
    [SteamAppUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupType] int  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Platforms'
CREATE TABLE [dbo].[Platforms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Abbreviation] nvarchar(max)  NULL,
    [GiantBombID] bigint  NOT NULL,
    [PlatformImage] nvarchar(max)  NULL,
    [IconImage] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'SteamUsers'
CREATE TABLE [dbo].[SteamUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [LastLogon] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL,
    [Name] nvarchar(max)  NULL,
    [PsnId] nvarchar(max)  NULL,
    [OriginId] nvarchar(max)  NULL,
    [GamerTag] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [SkynerdId] nvarchar(max)  NULL
);
GO

-- Creating table 'GameDetails'
CREATE TABLE [dbo].[GameDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GameId] int  NOT NULL,
    [PlatformId] int  NOT NULL,
    [Price] nvarchar(max)  NULL,
    [IconImage] nvarchar(max)  NULL,
    [GameImage] nvarchar(max)  NULL,
    [ReleaseDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Genres'
CREATE TABLE [dbo].[Genres] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ModifiedDate] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [GiantBombId] bigint  NOT NULL
);
GO

-- Creating table 'UserGames'
CREATE TABLE [dbo].[UserGames] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [GameId] int  NOT NULL,
    [GameDetailId] int  NOT NULL
);
GO

-- Creating table 'RelationFriends'
CREATE TABLE [dbo].[RelationFriends] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [UserTargetId] int  NOT NULL,
    [UserBaseId] int  NOT NULL
);
GO

-- Creating table 'ActivityMessages'
CREATE TABLE [dbo].[ActivityMessages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BaseId] int  NULL,
    [BaseType] int  NULL,
    [TargetID] int  NULL,
    [TargetType] int  NULL,
    [Message] nvarchar(max)  NULL,
    [MessageType] int  NULL,
    [MessageAction] int  NULL,
    [ReplyToMessage] int  NULL,
    [ModifiedDate] datetime  NULL,
    [CreationDate] datetime  NOT NULL
);
GO

-- Creating table 'UserGroups'
CREATE TABLE [dbo].[UserGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [GroupId] int  NOT NULL,
    [Status] int  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'GameGenre'
CREATE TABLE [dbo].[GameGenre] (
    [Games_Id] int  NOT NULL,
    [Genres_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Platforms'
ALTER TABLE [dbo].[Platforms]
ADD CONSTRAINT [PK_Platforms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SteamUsers'
ALTER TABLE [dbo].[SteamUsers]
ADD CONSTRAINT [PK_SteamUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GameDetails'
ALTER TABLE [dbo].[GameDetails]
ADD CONSTRAINT [PK_GameDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genres'
ALTER TABLE [dbo].[Genres]
ADD CONSTRAINT [PK_Genres]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [PK_UserGames]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RelationFriends'
ALTER TABLE [dbo].[RelationFriends]
ADD CONSTRAINT [PK_RelationFriends]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActivityMessages'
ALTER TABLE [dbo].[ActivityMessages]
ADD CONSTRAINT [PK_ActivityMessages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserGroups'
ALTER TABLE [dbo].[UserGroups]
ADD CONSTRAINT [PK_UserGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Games_Id], [Genres_Id] in table 'GameGenre'
ALTER TABLE [dbo].[GameGenre]
ADD CONSTRAINT [PK_GameGenre]
    PRIMARY KEY CLUSTERED ([Games_Id], [Genres_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_UserEvent]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEvent'
CREATE INDEX [IX_FK_UserEvent]
ON [dbo].[Events]
    ([User_Id]);
GO

-- Creating foreign key on [GameId] in table 'GameDetails'
ALTER TABLE [dbo].[GameDetails]
ADD CONSTRAINT [FK_GameGameDetail]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameGameDetail'
CREATE INDEX [IX_FK_GameGameDetail]
ON [dbo].[GameDetails]
    ([GameId]);
GO

-- Creating foreign key on [PlatformId] in table 'GameDetails'
ALTER TABLE [dbo].[GameDetails]
ADD CONSTRAINT [FK_GameDetailPlatform]
    FOREIGN KEY ([PlatformId])
    REFERENCES [dbo].[Platforms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameDetailPlatform'
CREATE INDEX [IX_FK_GameDetailPlatform]
ON [dbo].[GameDetails]
    ([PlatformId]);
GO

-- Creating foreign key on [Games_Id] in table 'GameGenre'
ALTER TABLE [dbo].[GameGenre]
ADD CONSTRAINT [FK_GameGenre_Game]
    FOREIGN KEY ([Games_Id])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Genres_Id] in table 'GameGenre'
ALTER TABLE [dbo].[GameGenre]
ADD CONSTRAINT [FK_GameGenre_Genre]
    FOREIGN KEY ([Genres_Id])
    REFERENCES [dbo].[Genres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameGenre_Genre'
CREATE INDEX [IX_FK_GameGenre_Genre]
ON [dbo].[GameGenre]
    ([Genres_Id]);
GO

-- Creating foreign key on [UserId] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [FK_UserUserGame]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserGame'
CREATE INDEX [IX_FK_UserUserGame]
ON [dbo].[UserGames]
    ([UserId]);
GO

-- Creating foreign key on [GameId] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [FK_GameUserGame]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameUserGame'
CREATE INDEX [IX_FK_GameUserGame]
ON [dbo].[UserGames]
    ([GameId]);
GO

-- Creating foreign key on [GameDetailId] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [FK_GameDetailUserGame]
    FOREIGN KEY ([GameDetailId])
    REFERENCES [dbo].[GameDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameDetailUserGame'
CREATE INDEX [IX_FK_GameDetailUserGame]
ON [dbo].[UserGames]
    ([GameDetailId]);
GO

-- Creating foreign key on [UserTargetId] in table 'RelationFriends'
ALTER TABLE [dbo].[RelationFriends]
ADD CONSTRAINT [FK_RelationFriendUser]
    FOREIGN KEY ([UserTargetId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationFriendUser'
CREATE INDEX [IX_FK_RelationFriendUser]
ON [dbo].[RelationFriends]
    ([UserTargetId]);
GO

-- Creating foreign key on [UserBaseId] in table 'RelationFriends'
ALTER TABLE [dbo].[RelationFriends]
ADD CONSTRAINT [FK_RelationFriendUser1]
    FOREIGN KEY ([UserBaseId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RelationFriendUser1'
CREATE INDEX [IX_FK_RelationFriendUser1]
ON [dbo].[RelationFriends]
    ([UserBaseId]);
GO

-- Creating foreign key on [Group_Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_GroupEvent]
    FOREIGN KEY ([Group_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupEvent'
CREATE INDEX [IX_FK_GroupEvent]
ON [dbo].[Events]
    ([Group_Id]);
GO

-- Creating foreign key on [GroupId] in table 'UserGroups'
ALTER TABLE [dbo].[UserGroups]
ADD CONSTRAINT [FK_GroupUserGroups]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupUserGroups'
CREATE INDEX [IX_FK_GroupUserGroups]
ON [dbo].[UserGroups]
    ([GroupId]);
GO

-- Creating foreign key on [UserId] in table 'UserGroups'
ALTER TABLE [dbo].[UserGroups]
ADD CONSTRAINT [FK_UserUserGroup]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserGroup'
CREATE INDEX [IX_FK_UserUserGroup]
ON [dbo].[UserGroups]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
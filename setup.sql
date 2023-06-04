USE master;
GO

PRINT 'Creating ContentTracker database...';
CREATE DATABASE ContentTracker;
GO

USE ContentTracker;
GO

PRINT 'Creating ContentTracker tables...';
CREATE TABLE Movies (
    Id uniqueidentifier NOT NULL,
    PRIMARY KEY (Id)
);
GO

CREATE TABLE SourceMovies (
    SourceName varchar(50) NOT NULL,
    SourceId int NOT NULL,
    LastRenewed datetime NOT NULL,
    BackdropUri varchar(max),
    ImdbId nvarchar(20),
    PosterUri varchar(max),
    ReleaseDate datetime,
    Runtime int,
    Status varchar(50),
    Title varchar(100),
    MovieId uniqueidentifier NOT NULL,
    PRIMARY KEY (SourceName, SourceId)
);
GO
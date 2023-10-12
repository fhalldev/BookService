CREATE TABLE [dbo].[book] (
    [book_id]                     INT           IDENTITY (1, 1) NOT NULL,
    [book_name]                   VARCHAR (MAX) DEFAULT ('') NOT NULL,
    [book_author]                 VARCHAR (MAX) DEFAULT ('') NOT NULL,
    [book_category]               INT           DEFAULT ((0)) NOT NULL,
    [book_registration_timestamp] DATETIME      NOT NULL,
    [book_description]            VARCHAR (MAX)  DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([book_id] ASC)
);


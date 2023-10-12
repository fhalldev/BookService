﻿/*
Deployment script for book_task

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "book_task"
:setvar DefaultFilePrefix "book_task"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering Procedure [dbo].[pr_book_update]...';


GO
ALTER PROCEDURE dbo.[pr_book_update]
    @BookID int = 0,
    @BookName varchar(MAX) = '',
    @BookAuthor varchar(MAX) = '',
    @BookCategory int = 0,
    @BookDescription varchar(MAX) = '',
    @RegistrationTimestamp datetime = GETDATE
AS
    IF NOT EXISTS (SELECT 1 FROM book WHERE book_id = @BookID)
    BEGIN
        -- Insert a new book
        INSERT INTO book (book_name, book_author, book_category, book_description, book_registration_timestamp)
        VALUES (@BookName, @BookAuthor, @BookCategory, @BookDescription, GETDATE());
    END

    ELSE
    BEGIN
        -- Update an existing book
        UPDATE book
        SET book_name = book_name,
            book_author = book_author,
            book_category = @BookCategory,
            book_description = @BookDescription
        WHERE book_id = @BookID;
    END
GO
PRINT N'Update complete.';


GO
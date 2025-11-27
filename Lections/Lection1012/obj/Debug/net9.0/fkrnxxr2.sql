BEGIN TRANSACTION;
ALTER TABLE [Category] ADD [Description] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251016082858_AddDescriptionToCategory', N'9.0.9');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251016083202_ChangeDescriptionInCategory', N'9.0.9');

COMMIT;
GO


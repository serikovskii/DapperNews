create table [dbo].[Articles]
(
	[Id] uniqueidentifier not null primary key,
	[Name] nvarchar(50) not null,
	[Text] nvarchar(max) not null,
	[CreationDate] datetime not null,
	[DeletedDate] datetime null
)

create table [dbo].[Comments]
(
	[Id] uniqueidentifier not null primary key,
	[Text] nvarchar(max) not null,
	[ArticleId] uniqueidentifier not null,
	[CreationDate] datetime not null,
	[DeletedDate] datetime null
	FOREIGN KEY (ArticleId) REFERENCES Articles(Id)
)

insert into Articles 
values
	(NEWID(), 'Сайт KFC от студии Лебедева проработал меньше, чем разрабатывался', GETDATE(), NULL),
	(NEWID(), 'Власти Сан-Франциско поддержали запрет на использование систем распознавания лиц госслужбами', GETDATE(), NULL),
	(NEWID(), 'Uber в США позволит заказывать поездки с молчаливыми водителями', GETDATE(), NULL)

insert into Comments
values
	(NEWID(), 'Конечно же дизайн Лебедева лучше.', (select Id from Articles where Name like 'Сайт KFC от студии') , GETDATE(), NULL),
	(NEWID(), 'Д - демократия.', (select Id from Articles where Name like 'Власти Сан-Франциско') , GETDATE(), NULL),
	(NEWID(), 'В Питерском Таксовичкофф этим опциям уже пару лет', (select Id from Articles where Name like 'Uber в США') , GETDATE(), NULL),
	(NEWID(), 'Куда важнее заказывать принявших душ с утра водителей', (select Id from Articles where Name like 'Uber в США') , GETDATE(), NULL)
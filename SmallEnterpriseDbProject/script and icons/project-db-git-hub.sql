--Script db project
-- if it doesn't work in block, first create the db, than use it, than create the table
create database AddressBook;
use AddressBook;

create table biz_contact(
	id bigint identity(1, 1) primary key not null,
	date_added datetime not null,
	company nvarchar(200),
	website varchar(max),
	title nvarchar(200),
	first_name nvarchar(100),
	last_name nvarchar(100),
	address nvarchar(200),
	city nvarchar(100),
	state nvarchar(100),
	postal_code nvarchar(100),
	email varchar(320),
	mobile varchar(50),
	notes nvarchar(1000)
);

create table login_table(
    id bigint identity(1, 1) primary key not null,   
    username nvarchar(50) not null,   
    psw nvarchar(50) not null  
);  




-- populate the db
insert into biz_contact(date_added, company, website, title, first_name, last_name, address, city, state, postal_code, email, mobile, notes) 
                values ('08-11-2016', 'CompanyGovC', 'www.gov.gov', 'CyberSecurityGov', 'Mike', 'Smith', '123 Main St.StreetCasual', 'Cincinnati', 'OH', '44663', 'CyberTech@outlook.com', '555-123-1234', 'Notes');
insert into biz_contact 
                values ('08-11-2016', 'BestCompanyC', 'www.best.gov', 'BestITSolutions', 'Jerry', 'Scotti', '345 Main St.LeVeline', 'Roma', 'IT', '45875', 'gerryCapoPlaza@gmail.com', '555-555-3231', 'Conference');
insert into biz_contact 
                values ('09-11-2016', 'GrulliCompagnia', 'www.grullietoscanacci.it', 'Vernacoliere', 'Leonardo', 'Da Vinci', '125 Main St.Aurora', 'Pisa', 'IT', '35885', 'LeoOldButGold@gmail.com', '555-234-2342', 'Play Assasin''s Creed');
insert into biz_contact 
                values ('10-11-2016', 'BadBoys', 'www.whatAreYouGonnaDo.it', 'MiamiChief', 'William', 'Smith', 'st. main street 564', 'Miami Vice', 'US', '12345','KingSunGlasses80BB@gmail.com', '555-849-2923', 'TadTalks for boomers');

insert into biz_contact 
                values ('01-12-2017', 'BadBoys', 'www.whatAreYouGonnaDo.it', 'MiamiChief', 'William', 'Shakespeare', 'st. Royal Palace 13', 'Stratford-upon-Avon', 'UK', '67345','ToBeOrNotToBe@gmail.com', '555-849-2923', 'Think about the question ''To be or not to be''');
insert into biz_contact 
                values ('01-09-2019', 'BestCompanyC', 'www.whatAreYouGonnaDo.it', 'MiamiChief', 'William', 'Smith', 'st. main street 564', 'Miami Vice', 'US', '12345','KingSunGlasses80BB@gmail.com', '555-849-2923', 'TadTalks for boomers');
insert into biz_contact 
                values ('08-02-2021', 'HardWareArchitecture', 'www.hardwareArch.it', 'ArchKing', 'Elliot', 'Anderson', 'st. main street NY 12', 'New York', 'US', '78345','fsociety@gmail.com', '667-849-2923', 'Hack the e-corp aka evil corp. Than play video games');
insert into biz_contact 
                values ('09-01-2016', 'CantaAutori', 'www.canta_tu.it', 'DeGregoriFan', 'Bobby', 'Dylan', 'st. main COncerto Road 69', 'Rimini', 'IT', '29345','deGregorySpyingDylan@gmail.com', '575-809-2323', 'Ascoltare Dylan, poi fare una cover dei suoi brani. Ascoltare Dylan');




insert into login_table(username, psw)
				values ('Edson93', 'log123');

select * from biz_contact;

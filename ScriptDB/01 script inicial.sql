create database AgroDB
go

use AgroDB
go

create table T_Example(
id int primary key identity,
Descripcion nvarchar(500)
)


select * from T_Example
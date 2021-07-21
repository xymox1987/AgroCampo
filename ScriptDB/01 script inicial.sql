create database AgroDB
go

use AgroDB
go
--drop table t_example
create table T_Example(
Id bigint primary key identity,
Descripcion nvarchar(500),
State int not null
)

go

insert into T_Example values ('test1',1)


select * from T_Example
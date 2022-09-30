create database CRUD_Veterinaria

use CRUD_Veterinaria
go

Set dateformat dmy

create table clientes
(id_cliente int identity (1,1),
nombre varchar(100),
sexo bit
constraint pk_clientes primary key (id_cliente))

create table tipos_mascotas
(id_tipo int,
descripcion varchar(30),
constraint pk_tipo_mascotas primary key (id_tipo))

create table mascotas
(id_mascota int identity,
nombre varchar(30),
edad int, 
id_tipo int,
id_cliente int
constraint pk_mascotas primary key (id_mascota)
constraint fk_clientes foreign key (id_cliente)
references clientes (id_cliente),
constraint fk_tipos foreign key (id_tipo)
references tipos_mascotas (id_tipo))

create table atenciones 
(id_atencion int identity (1,1),
fecha datetime, 
descripcion varchar(1000),
importe decimal(12,2),
id_mascota int
constraint pk_atenciones primary key (id_atencion)
constraint fk_mascotas foreign key (id_mascota)
references mascotas (id_mascota))

insert into tipos_mascotas values (1,'Perro')
insert into tipos_mascotas values (2,'Gato')
insert into tipos_mascotas values (3,'Araña')
insert into tipos_mascotas values (4,'Iguana')

insert into clientes values ('Juan García', 1)
insert into clientes values ('Ivana Lana', 2)
insert into clientes values ('Lucía Pérez', 2)

insert into mascotas values ('Gigi', 12, 2, 1)
insert into mascotas values ('Luli', 4, 3, 3)
insert into mascotas values ('Roco', 1, 1, 2)
insert into mascotas values ('Alis', 5, 3, 1)
insert into mascotas values ('Beto', 3, 1, 2)

insert into atenciones values ('11/08/2022', 'Vacunación completa', 6000, 1)
insert into atenciones values ('06/05/2022', 'Desparacitación', 1000, 2)
insert into atenciones values ('01/07/2022', 'Dolor estomacal, se recetó protector gástrico', 7000, 3)
insert into atenciones values ('01/01/2022', 'Vacunación completa', 6000, 4)


create procedure sp_proximo
@next int output
as
begin
set @next = (select MAX(id_mascota)+1 from mascotas);
end

create procedure sp_cargar_tipos
as
select * from tipos_mascotas

create procedure sp_cargar_clientes
as
select * from clientes

create procedure sp_insertar_mascotas
@nombre varchar(30),
@edad int,
@id_tipo int,
@id_cliente int,
@id_mascota int output
as
begin
insert into mascotas (nombre, edad, id_tipo, id_cliente) values(@nombre, @edad, @id_tipo, @id_cliente)
set @id_mascota = SCOPE_IDENTITY()
end

create procedure sp_insertar_atencion
@fecha datetime,
@descripcion varchar(1000),
@importe decimal(12,2),
@id_mascota int
as
begin
insert into atenciones (fecha, descripcion, importe, id_mascota) values (@fecha, @descripcion, @importe, @id_mascota)
end


select * from mascotas
select * from atenciones

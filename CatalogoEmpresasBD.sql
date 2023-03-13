create database CatalogoEmpresaBD;
GO

use CatalogoEmpresaBD;

create table Contactos(
ID int not null identity (1,1),
Nombre nvarchar(50) not null,
Email nvarchar(100) not null,
Telefono nvarchar(15),
Movil nvarchar(15) not null,
primary key(ID)
);
GO
create table Empresas(
ID int not null identity (1,1),
Nombre nvarchar(100) not null,
Rubro nvarchar(50) not null,
Categoria nvarchar(25) not null,
Departamento nvarchar(25) not null,
ContactoID int not null,
primary key (ID),
foreign key (ContactoID) references Contactos(ID)
);
GO
--Tablas agregadas para el segundo sprint--

create table Roles(
Id int not null identity(1,1),
Nombre nvarchar(30) not null,
primary key(Id)
);
GO

create table Usuarios(
Id int not null identity (1,1),
RolId int not null,
Nombre nvarchar(30) not null,
Apellido nvarchar(30) not null,
[Login] nvarchar(25) not null,
[Password] nvarchar(50) not null,
Estatus tinyint not null,
FechaRegistro datetime not null,
primary key(Id),
foreign key(RolId) references Roles (Id)
);
GO
insert into Contactos(Nombre, Email, Telefono, Movil) values
('Dayana', 'dayana@gmail.com', '24566096', '73522288'),
('Karla', 'Karla@gmail.com', '24566094', '78528298');

insert into Empresas(Nombre, Rubro, Categoria, Departamento, ContactoID) values
('Superselectos','Negocios','Supermercado','Sonsonate',1)


select*from Contactos
select*from Empresas

--Datos iniciales para las nuevas tablas--
insert into Roles(Nombre) values ('Administrador');
insert into Usuarios(RolId, Nombre, Apellido, [Login], [Password], Estatus, FechaRegistro) values (1, 'Juan', 
'Peña', 'JP', '2e6e5a2b38ba905790605c9b101497bc', 1, SYSDATETIME());

select*from Roles
select*from Usuarios

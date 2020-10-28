use [incube]; 
go
/************************************
            Справочники
************************************/

/***********************************
   Справочник причин неаттестации
***********************************/
if object_id('sp_cause', 'U') is not null
    drop table sp_cause;
go
create table sp_cause(
   cause_id int not null, 
   name text not null, 
   constraint pk_cause_id primary key (cause_id));
go

/***********************************
  Справочник контрагентов
***********************************/
if object_id('sp_contractor', 'U') is not null
    drop table sp_contractor;
go
create table sp_contractor(
   contractor_id int not null,   
   name text not null,
   shipper bit,                  -- Грузоотправитель 
   consigner bit,                -- Грузополучатель
 constraint pk_contractor_id primary key (contractor_id));
go

/***********************************
   Справочник материалов
***********************************/
if object_id('sp_mat', 'U') is not null
    drop table sp_mat;
go
 create table sp_mat(
 mat_id int not null, 
 name text not null, 
 constraint pk_mat_id primary key (mat_id));
 go
/***********************************
    Cправочник пользователей
***********************************/
if object_id('sp_users', 'U') is not null
    drop table sp_users;
go
 create table sp_users(
 user_id int not null,
 login varchar(200) not null,
 password varchar(200) not null,
 fio varchar(300) not null, 
 emple_id varchar(50),
 constraint pk_user_id primary key (user_id));
 go
/************************************
        Рабочие таблицы
************************************/
/***********************************
    Таблица партий
***********************************/
-- Удаление таблицы car так как есть foreign key
if object_id('tb_car', 'U') is not null
    drop table tb_car;
go
if object_id('tb_part', 'U') is not null
    drop table tb_part;
go
 create table tb_part(
 part_id uniqueidentifier not null,
 oper varchar(300) not null,       -- ФИО оператора ОТК 
 start_time DATETIME not null,     -- время начала аттестации
 end_time datetime,                -- время окончания аттестации  
 constraint pk_tb_part primary key (part_id));
 go
 /***********************************
    Таблица вагонов
***********************************/
if object_id('tb_car', 'U') is not null
    drop table tb_car;
go 
create table tb_car(
   part_id uniqueidentifier not null,  
   car_id int not null,
   num varchar(8),                -- Номер вагона
   shipper int not null,          -- Грузоотправитель 
   consigner int not null,        -- Грузополучатель
   mat int not null,              -- код материала
   att_code int not null,         -- Признак аттестации
   tara float,                    -- Реальная тара вагона 
   tara_e float,                  -- Тара вагона по данным Этран
   zone_e int,                    -- Зона вагона по данным Этран
   cause_id int,                  -- Код причины неаттестации
   carrying_e float,              -- Грузоподъемность по данным Этран
   att_time datetime,             -- Время аттестации вагона   
 constraint pk_tb_car primary key (part_id, car_id),
 CONSTRAINT fk_tb_car FOREIGN KEY (part_id)
    REFERENCES tb_part (part_id)
    ON DELETE CASCADE);
 CONSTRAINT fk_shipper FOREIGN KEY (shipper)
    REFERENCES sp_contractor (contractor_id)
    ON DELETE CASCADE);	
CONSTRAINT fk_consigner FOREIGN KEY (consigner)
    REFERENCES sp_contractor (contractor_id)
    ON DELETE CASCADE);	
CONSTRAINT fk_mat FOREIGN KEY (mat)
    REFERENCES sp_mat (mat_id)
    ON DELETE CASCADE);	
 go
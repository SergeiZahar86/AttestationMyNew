use [incube]; 
go

/*Справочник причин неаттестации*/
DROP TABLE sp_cause;
go
create table sp_cause(
 cause_id int not null, 
 name text not null, 
 constraint pk_cause_id primary key (cause_id));
 GO
 /*Справочник контрагентов*/
 drop table sp_contractor; 
create table sp_contractor(
 contractor_id int not null,   
 name text not null,
  shipper bit,            -- Грузоотправитель 
 consignee bit,          -- Грузополучатель
 constraint pk_contractor_id primary key (contractor_id));
 GO
 /*Справочник материалов*/
 drop table  sp_mat; 
 create table sp_mat(
 mat_id int not null, 
 name text not null, 
 constraint pk_mat_id primary key (mat_id));
 GO
 /* справочник пользователей*/
 drop table sp_users; 
 create table sp_users(
 user_id int not null,
 login varchar(200) not null,
 password varchar(200) not null,
 fio varchar(300) not null, 
 emple_id varchar(50),
 constraint pk_user_id primary key (user_id));
 go
 /*данные по партии*/
 drop table tb_part;
 create table tb_part(
 part_id varchar(38) not null,
 oper varchar(300) not null,       -- ФИО оператора ОТК 
 shipper int not null,
 consignee int not null,
 mat int not null,
 start_time DATETIME not null,
 end_time datetime,
 constraint pk_tb_part primary key (part_id));
 go
 /*Данные по вагонам*/
 drop table tb_car;
 create table tb_car(
 part_id varchar(38) not null,
 car_id int not null,
 num varchar(8),
 att_code int not null,
 tara float,
 tara_e float,
 zone_e int,
 cause_id int,
 carrying float,
 att_time datetime,
 constraint pk_tb_car primary key (part_id, car_id),
 CONSTRAINT fk_tb_car FOREIGN KEY (part_id)
    REFERENCES tb_part (part_id)
    ON DELETE CASCADE);
 go
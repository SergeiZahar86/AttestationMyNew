use [incube]; 
go
/************************************
            �����������
************************************/

/***********************************
   ���������� ������ ������������
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
  ���������� ������������
***********************************/
if object_id('sp_contractor', 'U') is not null
    drop table sp_contractor;
go
create table sp_contractor(
   contractor_id int not null,   
   name text not null,
   shipper bit,                  -- ���������������� 
   consigner bit,                -- ���������������
 constraint pk_contractor_id primary key (contractor_id));
go

/***********************************
   ���������� ����������
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
    C��������� �������������
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
        ������� �������
************************************/
/***********************************
    ������� ������
***********************************/
-- �������� ������� car ��� ��� ���� foreign key
if object_id('tb_car', 'U') is not null
    drop table tb_car;
go
if object_id('tb_part', 'U') is not null
    drop table tb_part;
go
 create table tb_part(
 part_id uniqueidentifier not null,
 oper varchar(300) not null,       -- ��� ��������� ��� 
 start_time DATETIME not null,     -- ����� ������ ����������
 end_time datetime,                -- ����� ��������� ����������  
 constraint pk_tb_part primary key (part_id));
 go
 /***********************************
    ������� �������
***********************************/
if object_id('tb_car', 'U') is not null
    drop table tb_car;
go 
create table tb_car(
   part_id uniqueidentifier not null,  
   car_id int not null,
   num varchar(8),                -- ����� ������
   shipper int not null,          -- ���������������� 
   consigner int not null,        -- ���������������
   mat int not null,              -- ��� ���������
   att_code int not null,         -- ������� ����������
   tara float,                    -- �������� ���� ������ 
   tara_e float,                  -- ���� ������ �� ������ �����
   zone_e int,                    -- ���� ������ �� ������ �����
   cause_id int,                  -- ��� ������� ������������
   carrying_e float,              -- ���������������� �� ������ �����
   att_time datetime,             -- ����� ���������� ������   
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
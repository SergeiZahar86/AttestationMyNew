# ver 2: 22.10.2020
# Наименование пакета в Java
namespace java com.indas.att.thrift

typedef i32 int

exception DataProviderException {                     
   1:string message,   # описание ошибки
   2:int error_code    # Код ошибки
}

# Справочник причин неаттестации
struct cause_t {
  1:int id,      
  2:string name   
}

# Справочник контрагентов
struct contractor_t {
  1:int id,      
  2:string name,
  3:bool shipper,
  4:bool consigner  
}

# Справочник материалов
struct mat_t {
  1:int id,      
  2:string name
}

# Данные по вагонам
struct car_t {
  1:string part_id,        # Номер партии
  2:int car_id,            # Порядковый номер вагона в партии 
  3:string num,            # Номер вагона
  4:int shipper,           # Грузоотправитель  
  5:int consigner,         # Грузополучатель
  6:int mat,               # Код материала 
  7:int att_code,          # Признак аттестации (фттестован, не аттестован, условно аттестован)
  8:double tara,           # Вес тары
  9:double tara_e,         # Вес тары по Этрану
  10:int zone_e,           # Зона вагона
  11:int cause_id,         # Код причины неаттестации
  12:double carrying_e,    # Грузоподъемность
  13:string att_time       # Время аттестации
}

#данные по партии
struct part_t {
  1:string part_id,        # Номер партии
  2:string oper,           # ФИО оператора ОТК 
  6:list<car_t> cars,      # Вагоны партии 
  7:string start_time,     # Начало аттестации партии вагонов
  8:string end_time        # Окончание атткстации
}

struct photo_t {           # Массив фотографий
  1:binary left,           # Фотография левого борта вагона   
  2:binary right,          # Фотография правого борта вагона
  3:binary top             # Фотография сверху
}

service DataProviderService
{
      # Справочники
      list<cause_t> getCauses() throws (1:DataProviderException ex),                                          # Запрос справочника причин неаттестации
      list<contractor_t> getContractors() throws (1:DataProviderException ex),                                # Запрос справочника контрагентов
      list<mat_t> getMat() throws (1:DataProviderException ex),                                               # Запрос справочника материалов
      
      # Запрос данных	  
      photo_t getPhoto(1:string part_id, 2:int car_id) throws (1:DataProviderException ex),                   # Получение фотографий вагона
      part_t getPart(1:string part_id) throws (1:DataProviderException ex),                                   # Запрос партии вагонов   
      string getUser(1:string login, 2:string password, 3:string empl_id) throws (1:DataProviderException ex),# Получение имени пользователя
	  string getNum(1:string part_id, 2:int car_id) throws (1:DataProviderException ex),                      # Получение номера вагона
	  string getOldPart() throws (1:DataProviderException ex),                                                # Получение номера последней незакрытой партии
    
	  # запись значенией
  	  bool setNum(1:string part_id, 2:int car_id, 3:string num) throws (1:DataProviderException ex),             # Корректировка номера вагона	
      bool setAtt(1:string part_id, 2:int car_id, 3:int att_code) throws (1:DataProviderException ex),           # Корректировка признака аттестации
	  bool setUser(1:string part_id,2:string user) throws (1:DataProviderException ex),                          # запись имени оператора
	  bool setTara(1:string part_id,2:int car_id,3:double tara) throws (1:DataProviderException ex),             # корректировка тары по Этран
	  bool setZone(1:string part_id,2:int car_id,3:int zone) throws (1:DataProviderException ex),                # корректировка зоны по Этран
	  bool setCarry(1:string part_id,2:int car_id,3:double carry) throws (1:DataProviderException ex),           # корректировка грузоподъемности по Этран
	  bool setShipper(1:string part_id,2:int car_id,3:int shipper) throws (1:DataProviderException ex),          # корректировка грузоотправителя
	  bool setConsigner(1:string part_id,2:int car_id,3:int consigner) throws (1:DataProviderException ex),      # корректировка грузополучателя
	  bool setMat(1:string part_id,2:int car_id,3:int mat) throws (1:DataProviderException ex),                  # корректировка материала
	  bool setCar(1:string part_id,2:int car_id, 3:car_t car) throws (1:DataProviderException ex),               # корректировка данных по вагону
	  bool setCause(1:string part_id,2:int car_id, 3:int cause_id) throws (1:DataProviderException ex),          # корректировка причины неаттестации
	  
	  # Сервисные функции 
	  part_t startAtt(1:string user) throws (1:DataProviderException ex),                                        # Начало аттестации 
	  bool endAtt(1:string part_id) throws (1:DataProviderException ex),                                         # Завершение аттестации
	  bool changePass(1:string login, 2:string oldPass, 3:string newPass, 4:string newEmpl_id) throws (1:DataProviderException ex) # Смена данных учетной записи 
}



# Наименование пакета в Java
namespace java com.indas.att.thrift

typedef i32 int

exception DataProviderException {                     
   1:string message,   # описание ошибки
   2:int error_code,   # Код ошибки
}

# Структура описания причин неаттестации
struct cause_t {
  1:int id,      
  2:string name   
}

struct car_t {
  1:int part_id,           # Номер партии
  2:int car_id,            # Порядковый номер вагона в партии 
  3:string num,            # Номер вагона
  4:int att_code,          # Признак аттестации (фттестован, не аттестован, условно аттестован)
  5:double tara,           # Вес тары
  6:int zone_e,            # Зона вагона
  7:int cause_id,          # Код причины неаттестации
  8:string cause_name,     # Наименование причины неаттестации
  9:double carrying,       # Грузоподъемность
 10:string att_time        # Время аттестации
}

struct part_t {
  1:int part_id,           # Номер партии
  2:string oper,           # ФИО оператора ОТК 
  3:string shipper,        # Грузоотправитель  
  4:string consignee,      # Грузополучатель
  5:list<car_t> cars,      # Вагоны партии 
  6:string start_time,     # Начало аттестации партии вагонов
  7:string end_time        # Окончание атткстации
}

struct photo_t {             # Массив фотографий
  1:binary left,           # Фотография левого борта вагона   
  2:binary right,          # Фотография правого борта вагона
  3:binary top             # Фотография сверху
}

service DataProviderService
{
      photo_t getPhoto(1:int part_id, 2:int car_id) throws (1:DataProviderException ex),                       # Получение фотографий вагона
      list<cause_t> getCauses() throws (1:DataProviderException ex),                                         # Запрос справочника причин неаттестации
      part_t getPart(1:int id) throws (1:DataProviderException ex),                                          # Запрос партии вагонов   
      string chLogin(1:string user, 2:string password, 3:string empl_id) throws (1:DataProviderException ex),# Идентификация пользователя
      bool setNum(1:int part_id, 2:int car_id, 3:string num) throws (1:DataProviderException ex),            # Корректировка номера вагона	
      bool setAtt(1:int att_code) throws (1:DataProviderException ex),                               	     # Корректировка признака аттестации
	  bool setOper(1:string oper_name) throws (1:DataProviderException ex),                                  # запись имени оператора
}
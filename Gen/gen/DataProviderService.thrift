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
# состояние процессов позрузки, взвешивания и аттестации
struct state_bits {
  1:int task,   
  2:int inspection,    
  3:int weight,
  4:int load   
}

# Информация дата провайдера
struct info_dp {
  1:state_bits state,   
  2:int active_wagon,
  3:PusherPosition position
}

enum PusherPosition {
  FROM_FRONT
  FROM_BEHIND  
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
  7:int att_code,          # Признак аттестации (аттестован, не аттестован)
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
  2:list<car_t> cars,      # Вагоны партии 
  3:string start_time_att,     # Начало аттестации партии вагонов
  4:string end_time_att        # Окончание атткстации
}

struct photo_t {           # Массив фотографий
  1:binary left,           # Фотография левого борта вагона   
  2:binary right,          # Фотография правого борта вагона
  3:binary top             # Фотография сверху
}

struct Status_Att{         # Статус аттестации
  1:int id,                # Номер сообщения
  2:string massage         # Сообщение
}

service DataProviderService
{
    # Справочники
    list<cause_t> getCauses() throws (1:DataProviderException ex),                                          # Запрос справочника причин неаттестации
    list<contractor_t> getContractors() throws (1:DataProviderException ex),                                # Запрос справочника контрагентов
    list<mat_t> getMat() throws (1:DataProviderException ex),                                               # Запрос справочника материалов
      
    # Запрос данных	  
   	photo_t getPhoto(1:string part_id, 2:int car_id) throws (1:DataProviderException ex),                   # Получение фотографий вагона
   	part_t getPart() throws (1:DataProviderException ex),                     		                # Запрос партии вагонов   
	state_bits getStatusBits()  throws (1:DataProviderException ex),                                        # состояние процессов  
	info_dp getInfoDP() throws (1:DataProviderException ex),                                        # информация дата провайдера                                              
    
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
	#part_t startAtt(1:string userLogin) throws (1:DataProviderException ex),	# Начало аттестации 
	bool endAtt(1:string userLogin) throws (1:DataProviderException ex),			# Завершение аттестации
	  
	string createTask(1:string userLogin, 2:PusherPosition position) throws (1:DataProviderException ex),		# Создать задание 
	void endTask(1:string userLogin) throws (1:DataProviderException ex),		# Закрыть задание

	void removeTask(1:string userLogin) throws (1:DataProviderException ex),	# Удалить задание

}



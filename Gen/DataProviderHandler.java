////////////////////////////////////////////////////
// Класс обработчика сервисных функций
////////////////////////////////////////////////////

package com.indas.att.dataprovider;

import com.indas.att.thrift.DataProviderException;
import com.indas.att.thrift.DataProviderService;
import com.indas.att.thrift.Photo;
import com.indas.att.thrift.car_t;
import com.indas.att.thrift.cause_t;
import com.indas.att.thrift.part_t;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;
import java.util.ArrayList;
import java.util.List;
import org.apache.thrift.TException;
import org.apache.thrift.transport.TSocket;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


public class DataProviderHandler implements DataProviderService.Iface{
    
    static Logger logger = LoggerFactory.getLogger(DataProviderHandler.class);
    TSocket socket;
    List<String> hosts;
    DataProviderHandler(TSocket socket,List<String> hosts){
       this.socket =socket; 
       this.hosts =hosts;
    }

    /////////////////////////////////////////////////////////////////////
    // Проверка ip адреса
    /////////////////////////////////////////////////////////////////////     
    boolean checkIP(){
        String ip = socket.getSocket().getInetAddress().getHostAddress();
        if(!hosts.contains(ip)) {
            logger.info("access denied ip: "+ ip);
            return false;
        }
        return true;
    }
    /////////////////////////////////////////////////////////////////////
    // Запрос фотографий вагона
    /////////////////////////////////////////////////////////////////////
    @Override
    public Photo getPhoto(int part_id, int car_id) throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        Photo ph = new Photo();
        
        try {
               RandomAccessFile inLeft  = new RandomAccessFile("images/1.jpg","rb");
               RandomAccessFile inRight = new RandomAccessFile("images/2.jpg","rb");
               RandomAccessFile inTop   = new RandomAccessFile("images/3.jpg","rb"); 
            
               FileChannel fchLeft  = inLeft.getChannel();
               FileChannel fchRight = inRight.getChannel();
               FileChannel fchTop   = inTop.getChannel();
               
               long LSize  = fchLeft.size(); 
               long RSize = fchRight.size();
               long TSize   = fchTop.size();

               ph.left  = ByteBuffer.allocate((int)LSize);
               ph.right = ByteBuffer.allocate((int)RSize);
               ph.top   = ByteBuffer.allocate((int)TSize);
               
               fchLeft.read(ph.left);
               fchRight.read(ph.right);
               fchTop.read(ph.top);
               
               ph.left.flip();
               ph.right.flip();
               ph.top.flip();
               
               fchLeft.close();
               fchRight.close();
               fchTop.close();
               
               inLeft.close();
               inRight.close();
               inTop.close();
 
               
            } catch(IOException ex) {
                throw new DataProviderException(ex.getMessage(), 0);
            }
        return ph;
    }

    ///////////////////////////////////////////////////////////////////////
    // Запрос справочника причин не аттестации
    //////////////////////////////////////////////////////////////////////
    @Override
    public List<cause_t> getCauses() throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        List<cause_t> causes = new ArrayList();
        causes.add(new cause_t(1,"Грязное дно"));
        causes.add(new cause_t(2,"Грязный номер"));
        causes.add(new cause_t(3,"Красная зона"));
        return(causes);
    }

    ///////////////////////////////////////////////////////////////////////
    // Запрос данных по всем вагонам партии
    /////////////////////////////////////////////////////////////////////
    @Override
    public part_t getPart(int id) throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        part_t part = new part_t();
        
        part.consignee="ООО Поляны";
        part.shipper="ОАО ЗСМК";
        part.oper="";
        part.part_id=23;
        part.start_time="02.10.2020 02:04:56";
        part.end_time="02.10.2020 06:54:56";
        
        List<car_t> cars = new ArrayList();
        cars.add(new car_t(2,1,"88956754",0,23.0,1,0,"",25.6,"02.10.2020 04:34"));
        cars.add(new car_t(2,2,"88956754",0,21.0,1,0,"",23.6,"02.10.2020 04:44"));
        return part;
    }
    
    //////////////////////////////////////////////////////////////////////////
    // Проверка логина и возвращение имени оператора
    /////////////////////////////////////////////////////////////////////////
    @Override
    public String chLogin(String user, String password, String empl_id) throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        if(empl_id==null && user=="oper" && password=="oper") return "Соловьев Иван Иванович";
        if(empl_id!=null && empl_id =="12345678") return "Иванов Петр Иванович";
        else return "";
    }
    //////////////////////////////////////////////////////////////////////////
    // Запись номера вагона
    //////////////////////////////////////////////////////////////////////////
    @Override
    public boolean setNum(int part_id, int car_id, String num) throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        if(part_id==2 && car_id==4) { return true; }
        else {
                throw new DataProviderException("Вагон не найден", 2);
        }  
    }
    ////////////////////////////////////////////////////////////////////////
    // Запись признака аттестации
    ///////////////////////////////////////////////////////////////////////
    @Override
    public boolean setAtt(int att_code) throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        return true;
    }
    ////////////////////////////////////////////////////////////////////////
    //  Запись ФИО оператора
    ///////////////////////////////////////////////////////////////////////
    @Override
    public boolean setOper(String oper_name) throws TException {
        if(!checkIP()) throw new DataProviderException("Доступ запрещен", 0);
        return true;
    }
    
}

/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;


#if !SILVERLIGHT
[Serializable]
#endif
public partial class part_t : TBase
{
  private string _part_id;
  private string _oper;
  private int _shipper;
  private int _consigner;
  private int _mat;
  private List<car_t> _cars;
  private string _start_time;
  private string _end_time;

  public string Part_id
  {
    get
    {
      return _part_id;
    }
    set
    {
      __isset.part_id = true;
      this._part_id = value;
    }
  }

  public string Oper
  {
    get
    {
      return _oper;
    }
    set
    {
      __isset.oper = true;
      this._oper = value;
    }
  }

  public int Shipper
  {
    get
    {
      return _shipper;
    }
    set
    {
      __isset.shipper = true;
      this._shipper = value;
    }
  }

  public int Consigner
  {
    get
    {
      return _consigner;
    }
    set
    {
      __isset.consigner = true;
      this._consigner = value;
    }
  }

  public int Mat
  {
    get
    {
      return _mat;
    }
    set
    {
      __isset.mat = true;
      this._mat = value;
    }
  }

  public List<car_t> Cars
  {
    get
    {
      return _cars;
    }
    set
    {
      __isset.cars = true;
      this._cars = value;
    }
  }

  public string Start_time
  {
    get
    {
      return _start_time;
    }
    set
    {
      __isset.start_time = true;
      this._start_time = value;
    }
  }

  public string End_time
  {
    get
    {
      return _end_time;
    }
    set
    {
      __isset.end_time = true;
      this._end_time = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool part_id;
    public bool oper;
    public bool shipper;
    public bool consigner;
    public bool mat;
    public bool cars;
    public bool start_time;
    public bool end_time;
  }

  public part_t() {
  }

  public void Read (TProtocol iprot)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.String) {
              Part_id = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              Oper = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              Shipper = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              Consigner = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.I32) {
              Mat = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.List) {
              {
                Cars = new List<car_t>();
                TList _list0 = iprot.ReadListBegin();
                for( int _i1 = 0; _i1 < _list0.Count; ++_i1)
                {
                  car_t _elem2;
                  _elem2 = new car_t();
                  _elem2.Read(iprot);
                  Cars.Add(_elem2);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.String) {
              Start_time = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.String) {
              End_time = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public void Write(TProtocol oprot) {
    oprot.IncrementRecursionDepth();
    try
    {
      TStruct struc = new TStruct("part_t");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Part_id != null && __isset.part_id) {
        field.Name = "part_id";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Part_id);
        oprot.WriteFieldEnd();
      }
      if (Oper != null && __isset.oper) {
        field.Name = "oper";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Oper);
        oprot.WriteFieldEnd();
      }
      if (__isset.shipper) {
        field.Name = "shipper";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Shipper);
        oprot.WriteFieldEnd();
      }
      if (__isset.consigner) {
        field.Name = "consigner";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Consigner);
        oprot.WriteFieldEnd();
      }
      if (__isset.mat) {
        field.Name = "mat";
        field.Type = TType.I32;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Mat);
        oprot.WriteFieldEnd();
      }
      if (Cars != null && __isset.cars) {
        field.Name = "cars";
        field.Type = TType.List;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Cars.Count));
          foreach (car_t _iter3 in Cars)
          {
            _iter3.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (Start_time != null && __isset.start_time) {
        field.Name = "start_time";
        field.Type = TType.String;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Start_time);
        oprot.WriteFieldEnd();
      }
      if (End_time != null && __isset.end_time) {
        field.Name = "end_time";
        field.Type = TType.String;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(End_time);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override string ToString() {
    StringBuilder __sb = new StringBuilder("part_t(");
    bool __first = true;
    if (Part_id != null && __isset.part_id) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Part_id: ");
      __sb.Append(Part_id);
    }
    if (Oper != null && __isset.oper) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Oper: ");
      __sb.Append(Oper);
    }
    if (__isset.shipper) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Shipper: ");
      __sb.Append(Shipper);
    }
    if (__isset.consigner) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Consigner: ");
      __sb.Append(Consigner);
    }
    if (__isset.mat) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Mat: ");
      __sb.Append(Mat);
    }
    if (Cars != null && __isset.cars) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Cars: ");
      __sb.Append(Cars);
    }
    if (Start_time != null && __isset.start_time) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Start_time: ");
      __sb.Append(Start_time);
    }
    if (End_time != null && __isset.end_time) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("End_time: ");
      __sb.Append(End_time);
    }
    __sb.Append(")");
    return __sb.ToString();
  }

}


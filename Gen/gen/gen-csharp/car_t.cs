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
public partial class car_t : TBase
{
  private string _part_id;
  private int _car_id;
  private string _num;
  private int _shipper;
  private int _consigner;
  private int _mat;
  private int _att_code;
  private double _tara;
  private double _tara_e;
  private int _zone_e;
  private int _cause_id;
  private double _carrying_e;
  private string _att_time;

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

  public int Car_id
  {
    get
    {
      return _car_id;
    }
    set
    {
      __isset.car_id = true;
      this._car_id = value;
    }
  }

  public string Num
  {
    get
    {
      return _num;
    }
    set
    {
      __isset.num = true;
      this._num = value;
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

  public int Att_code
  {
    get
    {
      return _att_code;
    }
    set
    {
      __isset.att_code = true;
      this._att_code = value;
    }
  }

  public double Tara
  {
    get
    {
      return _tara;
    }
    set
    {
      __isset.tara = true;
      this._tara = value;
    }
  }

  public double Tara_e
  {
    get
    {
      return _tara_e;
    }
    set
    {
      __isset.tara_e = true;
      this._tara_e = value;
    }
  }

  public int Zone_e
  {
    get
    {
      return _zone_e;
    }
    set
    {
      __isset.zone_e = true;
      this._zone_e = value;
    }
  }

  public int Cause_id
  {
    get
    {
      return _cause_id;
    }
    set
    {
      __isset.cause_id = true;
      this._cause_id = value;
    }
  }

  public double Carrying_e
  {
    get
    {
      return _carrying_e;
    }
    set
    {
      __isset.carrying_e = true;
      this._carrying_e = value;
    }
  }

  public string Att_time
  {
    get
    {
      return _att_time;
    }
    set
    {
      __isset.att_time = true;
      this._att_time = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool part_id;
    public bool car_id;
    public bool num;
    public bool shipper;
    public bool consigner;
    public bool mat;
    public bool att_code;
    public bool tara;
    public bool tara_e;
    public bool zone_e;
    public bool cause_id;
    public bool carrying_e;
    public bool att_time;
  }

  public car_t() {
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
            if (field.Type == TType.I32) {
              Car_id = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              Num = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              Shipper = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.I32) {
              Consigner = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.I32) {
              Mat = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.I32) {
              Att_code = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.Double) {
              Tara = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.Double) {
              Tara_e = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.I32) {
              Zone_e = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 11:
            if (field.Type == TType.I32) {
              Cause_id = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 12:
            if (field.Type == TType.Double) {
              Carrying_e = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 13:
            if (field.Type == TType.String) {
              Att_time = iprot.ReadString();
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
      TStruct struc = new TStruct("car_t");
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
      if (__isset.car_id) {
        field.Name = "car_id";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Car_id);
        oprot.WriteFieldEnd();
      }
      if (Num != null && __isset.num) {
        field.Name = "num";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Num);
        oprot.WriteFieldEnd();
      }
      if (__isset.shipper) {
        field.Name = "shipper";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Shipper);
        oprot.WriteFieldEnd();
      }
      if (__isset.consigner) {
        field.Name = "consigner";
        field.Type = TType.I32;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Consigner);
        oprot.WriteFieldEnd();
      }
      if (__isset.mat) {
        field.Name = "mat";
        field.Type = TType.I32;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Mat);
        oprot.WriteFieldEnd();
      }
      if (__isset.att_code) {
        field.Name = "att_code";
        field.Type = TType.I32;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Att_code);
        oprot.WriteFieldEnd();
      }
      if (__isset.tara) {
        field.Name = "tara";
        field.Type = TType.Double;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Tara);
        oprot.WriteFieldEnd();
      }
      if (__isset.tara_e) {
        field.Name = "tara_e";
        field.Type = TType.Double;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Tara_e);
        oprot.WriteFieldEnd();
      }
      if (__isset.zone_e) {
        field.Name = "zone_e";
        field.Type = TType.I32;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Zone_e);
        oprot.WriteFieldEnd();
      }
      if (__isset.cause_id) {
        field.Name = "cause_id";
        field.Type = TType.I32;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Cause_id);
        oprot.WriteFieldEnd();
      }
      if (__isset.carrying_e) {
        field.Name = "carrying_e";
        field.Type = TType.Double;
        field.ID = 12;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Carrying_e);
        oprot.WriteFieldEnd();
      }
      if (Att_time != null && __isset.att_time) {
        field.Name = "att_time";
        field.Type = TType.String;
        field.ID = 13;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Att_time);
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
    StringBuilder __sb = new StringBuilder("car_t(");
    bool __first = true;
    if (Part_id != null && __isset.part_id) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Part_id: ");
      __sb.Append(Part_id);
    }
    if (__isset.car_id) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Car_id: ");
      __sb.Append(Car_id);
    }
    if (Num != null && __isset.num) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Num: ");
      __sb.Append(Num);
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
    if (__isset.att_code) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Att_code: ");
      __sb.Append(Att_code);
    }
    if (__isset.tara) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Tara: ");
      __sb.Append(Tara);
    }
    if (__isset.tara_e) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Tara_e: ");
      __sb.Append(Tara_e);
    }
    if (__isset.zone_e) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Zone_e: ");
      __sb.Append(Zone_e);
    }
    if (__isset.cause_id) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Cause_id: ");
      __sb.Append(Cause_id);
    }
    if (__isset.carrying_e) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Carrying_e: ");
      __sb.Append(Carrying_e);
    }
    if (Att_time != null && __isset.att_time) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Att_time: ");
      __sb.Append(Att_time);
    }
    __sb.Append(")");
    return __sb.ToString();
  }

}


/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
package com.indas.att.thrift;

@SuppressWarnings({"cast", "rawtypes", "serial", "unchecked", "unused"})
@javax.annotation.Generated(value = "Autogenerated by Thrift Compiler (0.13.0)", date = "2021-05-14")
public class state_bits implements org.apache.thrift.TBase<state_bits, state_bits._Fields>, java.io.Serializable, Cloneable, Comparable<state_bits> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("state_bits");

  private static final org.apache.thrift.protocol.TField ATT_FIELD_DESC = new org.apache.thrift.protocol.TField("att", org.apache.thrift.protocol.TType.I32, (short)1);
  private static final org.apache.thrift.protocol.TField WEIGHT_FIELD_DESC = new org.apache.thrift.protocol.TField("weight", org.apache.thrift.protocol.TType.I32, (short)2);
  private static final org.apache.thrift.protocol.TField LOAD_FIELD_DESC = new org.apache.thrift.protocol.TField("load", org.apache.thrift.protocol.TType.I32, (short)3);

  private static final org.apache.thrift.scheme.SchemeFactory STANDARD_SCHEME_FACTORY = new state_bitsStandardSchemeFactory();
  private static final org.apache.thrift.scheme.SchemeFactory TUPLE_SCHEME_FACTORY = new state_bitsTupleSchemeFactory();

  public int att; // required
  public int weight; // required
  public int load; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    ATT((short)1, "att"),
    WEIGHT((short)2, "weight"),
    LOAD((short)3, "load");

    private static final java.util.Map<java.lang.String, _Fields> byName = new java.util.HashMap<java.lang.String, _Fields>();

    static {
      for (_Fields field : java.util.EnumSet.allOf(_Fields.class)) {
        byName.put(field.getFieldName(), field);
      }
    }

    /**
     * Find the _Fields constant that matches fieldId, or null if its not found.
     */
    @org.apache.thrift.annotation.Nullable
    public static _Fields findByThriftId(int fieldId) {
      switch(fieldId) {
        case 1: // ATT
          return ATT;
        case 2: // WEIGHT
          return WEIGHT;
        case 3: // LOAD
          return LOAD;
        default:
          return null;
      }
    }

    /**
     * Find the _Fields constant that matches fieldId, throwing an exception
     * if it is not found.
     */
    public static _Fields findByThriftIdOrThrow(int fieldId) {
      _Fields fields = findByThriftId(fieldId);
      if (fields == null) throw new java.lang.IllegalArgumentException("Field " + fieldId + " doesn't exist!");
      return fields;
    }

    /**
     * Find the _Fields constant that matches name, or null if its not found.
     */
    @org.apache.thrift.annotation.Nullable
    public static _Fields findByName(java.lang.String name) {
      return byName.get(name);
    }

    private final short _thriftId;
    private final java.lang.String _fieldName;

    _Fields(short thriftId, java.lang.String fieldName) {
      _thriftId = thriftId;
      _fieldName = fieldName;
    }

    public short getThriftFieldId() {
      return _thriftId;
    }

    public java.lang.String getFieldName() {
      return _fieldName;
    }
  }

  // isset id assignments
  private static final int __ATT_ISSET_ID = 0;
  private static final int __WEIGHT_ISSET_ID = 1;
  private static final int __LOAD_ISSET_ID = 2;
  private byte __isset_bitfield = 0;
  public static final java.util.Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    java.util.Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new java.util.EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.ATT, new org.apache.thrift.meta_data.FieldMetaData("att", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32        , "int")));
    tmpMap.put(_Fields.WEIGHT, new org.apache.thrift.meta_data.FieldMetaData("weight", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32        , "int")));
    tmpMap.put(_Fields.LOAD, new org.apache.thrift.meta_data.FieldMetaData("load", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32        , "int")));
    metaDataMap = java.util.Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(state_bits.class, metaDataMap);
  }

  public state_bits() {
  }

  public state_bits(
    int att,
    int weight,
    int load)
  {
    this();
    this.att = att;
    setAttIsSet(true);
    this.weight = weight;
    setWeightIsSet(true);
    this.load = load;
    setLoadIsSet(true);
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public state_bits(state_bits other) {
    __isset_bitfield = other.__isset_bitfield;
    this.att = other.att;
    this.weight = other.weight;
    this.load = other.load;
  }

  public state_bits deepCopy() {
    return new state_bits(this);
  }

  @Override
  public void clear() {
    setAttIsSet(false);
    this.att = 0;
    setWeightIsSet(false);
    this.weight = 0;
    setLoadIsSet(false);
    this.load = 0;
  }

  public int getAtt() {
    return this.att;
  }

  public state_bits setAtt(int att) {
    this.att = att;
    setAttIsSet(true);
    return this;
  }

  public void unsetAtt() {
    __isset_bitfield = org.apache.thrift.EncodingUtils.clearBit(__isset_bitfield, __ATT_ISSET_ID);
  }

  /** Returns true if field att is set (has been assigned a value) and false otherwise */
  public boolean isSetAtt() {
    return org.apache.thrift.EncodingUtils.testBit(__isset_bitfield, __ATT_ISSET_ID);
  }

  public void setAttIsSet(boolean value) {
    __isset_bitfield = org.apache.thrift.EncodingUtils.setBit(__isset_bitfield, __ATT_ISSET_ID, value);
  }

  public int getWeight() {
    return this.weight;
  }

  public state_bits setWeight(int weight) {
    this.weight = weight;
    setWeightIsSet(true);
    return this;
  }

  public void unsetWeight() {
    __isset_bitfield = org.apache.thrift.EncodingUtils.clearBit(__isset_bitfield, __WEIGHT_ISSET_ID);
  }

  /** Returns true if field weight is set (has been assigned a value) and false otherwise */
  public boolean isSetWeight() {
    return org.apache.thrift.EncodingUtils.testBit(__isset_bitfield, __WEIGHT_ISSET_ID);
  }

  public void setWeightIsSet(boolean value) {
    __isset_bitfield = org.apache.thrift.EncodingUtils.setBit(__isset_bitfield, __WEIGHT_ISSET_ID, value);
  }

  public int getLoad() {
    return this.load;
  }

  public state_bits setLoad(int load) {
    this.load = load;
    setLoadIsSet(true);
    return this;
  }

  public void unsetLoad() {
    __isset_bitfield = org.apache.thrift.EncodingUtils.clearBit(__isset_bitfield, __LOAD_ISSET_ID);
  }

  /** Returns true if field load is set (has been assigned a value) and false otherwise */
  public boolean isSetLoad() {
    return org.apache.thrift.EncodingUtils.testBit(__isset_bitfield, __LOAD_ISSET_ID);
  }

  public void setLoadIsSet(boolean value) {
    __isset_bitfield = org.apache.thrift.EncodingUtils.setBit(__isset_bitfield, __LOAD_ISSET_ID, value);
  }

  public void setFieldValue(_Fields field, @org.apache.thrift.annotation.Nullable java.lang.Object value) {
    switch (field) {
    case ATT:
      if (value == null) {
        unsetAtt();
      } else {
        setAtt((java.lang.Integer)value);
      }
      break;

    case WEIGHT:
      if (value == null) {
        unsetWeight();
      } else {
        setWeight((java.lang.Integer)value);
      }
      break;

    case LOAD:
      if (value == null) {
        unsetLoad();
      } else {
        setLoad((java.lang.Integer)value);
      }
      break;

    }
  }

  @org.apache.thrift.annotation.Nullable
  public java.lang.Object getFieldValue(_Fields field) {
    switch (field) {
    case ATT:
      return getAtt();

    case WEIGHT:
      return getWeight();

    case LOAD:
      return getLoad();

    }
    throw new java.lang.IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new java.lang.IllegalArgumentException();
    }

    switch (field) {
    case ATT:
      return isSetAtt();
    case WEIGHT:
      return isSetWeight();
    case LOAD:
      return isSetLoad();
    }
    throw new java.lang.IllegalStateException();
  }

  @Override
  public boolean equals(java.lang.Object that) {
    if (that == null)
      return false;
    if (that instanceof state_bits)
      return this.equals((state_bits)that);
    return false;
  }

  public boolean equals(state_bits that) {
    if (that == null)
      return false;
    if (this == that)
      return true;

    boolean this_present_att = true;
    boolean that_present_att = true;
    if (this_present_att || that_present_att) {
      if (!(this_present_att && that_present_att))
        return false;
      if (this.att != that.att)
        return false;
    }

    boolean this_present_weight = true;
    boolean that_present_weight = true;
    if (this_present_weight || that_present_weight) {
      if (!(this_present_weight && that_present_weight))
        return false;
      if (this.weight != that.weight)
        return false;
    }

    boolean this_present_load = true;
    boolean that_present_load = true;
    if (this_present_load || that_present_load) {
      if (!(this_present_load && that_present_load))
        return false;
      if (this.load != that.load)
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    int hashCode = 1;

    hashCode = hashCode * 8191 + att;

    hashCode = hashCode * 8191 + weight;

    hashCode = hashCode * 8191 + load;

    return hashCode;
  }

  @Override
  public int compareTo(state_bits other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = java.lang.Boolean.valueOf(isSetAtt()).compareTo(other.isSetAtt());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetAtt()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.att, other.att);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = java.lang.Boolean.valueOf(isSetWeight()).compareTo(other.isSetWeight());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetWeight()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.weight, other.weight);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = java.lang.Boolean.valueOf(isSetLoad()).compareTo(other.isSetLoad());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetLoad()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.load, other.load);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    return 0;
  }

  @org.apache.thrift.annotation.Nullable
  public _Fields fieldForId(int fieldId) {
    return _Fields.findByThriftId(fieldId);
  }

  public void read(org.apache.thrift.protocol.TProtocol iprot) throws org.apache.thrift.TException {
    scheme(iprot).read(iprot, this);
  }

  public void write(org.apache.thrift.protocol.TProtocol oprot) throws org.apache.thrift.TException {
    scheme(oprot).write(oprot, this);
  }

  @Override
  public java.lang.String toString() {
    java.lang.StringBuilder sb = new java.lang.StringBuilder("state_bits(");
    boolean first = true;

    sb.append("att:");
    sb.append(this.att);
    first = false;
    if (!first) sb.append(", ");
    sb.append("weight:");
    sb.append(this.weight);
    first = false;
    if (!first) sb.append(", ");
    sb.append("load:");
    sb.append(this.load);
    first = false;
    sb.append(")");
    return sb.toString();
  }

  public void validate() throws org.apache.thrift.TException {
    // check for required fields
    // check for sub-struct validity
  }

  private void writeObject(java.io.ObjectOutputStream out) throws java.io.IOException {
    try {
      write(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(out)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private void readObject(java.io.ObjectInputStream in) throws java.io.IOException, java.lang.ClassNotFoundException {
    try {
      // it doesn't seem like you should have to do this, but java serialization is wacky, and doesn't call the default constructor.
      __isset_bitfield = 0;
      read(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(in)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private static class state_bitsStandardSchemeFactory implements org.apache.thrift.scheme.SchemeFactory {
    public state_bitsStandardScheme getScheme() {
      return new state_bitsStandardScheme();
    }
  }

  private static class state_bitsStandardScheme extends org.apache.thrift.scheme.StandardScheme<state_bits> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, state_bits struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // ATT
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.att = iprot.readI32();
              struct.setAttIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 2: // WEIGHT
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.weight = iprot.readI32();
              struct.setWeightIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 3: // LOAD
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.load = iprot.readI32();
              struct.setLoadIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          default:
            org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
        }
        iprot.readFieldEnd();
      }
      iprot.readStructEnd();

      // check for required fields of primitive type, which can't be checked in the validate method
      struct.validate();
    }

    public void write(org.apache.thrift.protocol.TProtocol oprot, state_bits struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(ATT_FIELD_DESC);
      oprot.writeI32(struct.att);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(WEIGHT_FIELD_DESC);
      oprot.writeI32(struct.weight);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(LOAD_FIELD_DESC);
      oprot.writeI32(struct.load);
      oprot.writeFieldEnd();
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class state_bitsTupleSchemeFactory implements org.apache.thrift.scheme.SchemeFactory {
    public state_bitsTupleScheme getScheme() {
      return new state_bitsTupleScheme();
    }
  }

  private static class state_bitsTupleScheme extends org.apache.thrift.scheme.TupleScheme<state_bits> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, state_bits struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TTupleProtocol oprot = (org.apache.thrift.protocol.TTupleProtocol) prot;
      java.util.BitSet optionals = new java.util.BitSet();
      if (struct.isSetAtt()) {
        optionals.set(0);
      }
      if (struct.isSetWeight()) {
        optionals.set(1);
      }
      if (struct.isSetLoad()) {
        optionals.set(2);
      }
      oprot.writeBitSet(optionals, 3);
      if (struct.isSetAtt()) {
        oprot.writeI32(struct.att);
      }
      if (struct.isSetWeight()) {
        oprot.writeI32(struct.weight);
      }
      if (struct.isSetLoad()) {
        oprot.writeI32(struct.load);
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, state_bits struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TTupleProtocol iprot = (org.apache.thrift.protocol.TTupleProtocol) prot;
      java.util.BitSet incoming = iprot.readBitSet(3);
      if (incoming.get(0)) {
        struct.att = iprot.readI32();
        struct.setAttIsSet(true);
      }
      if (incoming.get(1)) {
        struct.weight = iprot.readI32();
        struct.setWeightIsSet(true);
      }
      if (incoming.get(2)) {
        struct.load = iprot.readI32();
        struct.setLoadIsSet(true);
      }
    }
  }

  private static <S extends org.apache.thrift.scheme.IScheme> S scheme(org.apache.thrift.protocol.TProtocol proto) {
    return (org.apache.thrift.scheme.StandardScheme.class.equals(proto.getScheme()) ? STANDARD_SCHEME_FACTORY : TUPLE_SCHEME_FACTORY).getScheme();
  }
}
